namespace BinaryDigit.DataAccess.Base
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Reflection;

    public abstract class BD_DataObjectBase<T>
    {
        #region Fields

        private readonly Dictionary<string, byte[]> _blogStorage = new Dictionary<string, byte[]>();

        private readonly Dictionary<string, int> _blogStorageSizes = new Dictionary<string, int>();

        #endregion

        #region Constructors and Destructors

        public BD_DataObjectBase()
        {
            this.Reset();
        }

        public BD_DataObjectBase(string whereClause, SqlParameter[] sqlParameters = null)
        {
            this.ChangeTo(whereClause, sqlParameters);
        }

        public BD_DataObjectBase(DataTable input)
        {
            ChangeTo(input);
        }

        public BD_DataObjectBase(SqlDataReader reader)
        {
            ChangeTo(reader);
        }

        private BD_DataObjectBase(DataRow dr)
        {
            ChangeTo(dr);
        }

        #endregion

        #region Public Properties

        public Dictionary<PropertyInfo, BD_ColumnDefinition> Columns
        {
            get
            {
                var result = new Dictionary<PropertyInfo, BD_ColumnDefinition>();
                foreach (PropertyInfo pi in this.GetType().GetProperties())
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(pi))
                    {
                        if (attr is BD_ColumnDefinition)
                        {
                            result.Add(pi, (BD_ColumnDefinition)attr);
                        }
                    }
                }
                return result;
            }
        }

        public bool IsLoaded { get; set; }

        public IEnumerable<KeyValuePair<PropertyInfo, BD_ColumnDefinition>> PrimaryKeyColumns
        {
            get
            {
                return (from o in this.Columns
                        where o.Value.Is_PrimaryKey
                        select o);
            }
        }

        public abstract string TableName { get; }

        #endregion

        #region Public Methods and Operators

        public void ChangeTo(string whereClause, SqlParameter[] sqlParameters = null)
        {
            this.ChangeTo(Sql.FetchDataTable(this.GetBasicSelectFrom() + " WHERE " + whereClause, sqlParameters));
        }

        public void ChangeTo(DataTable input)
        {
            this.Reset();
            foreach (DataRow dr in input.Rows)
            {
                ChangeTo(dr);
                break;
            }
        }

        public void ChangeTo(SqlDataReader reader)
        {
            this.Reset();
            foreach (string columnName in reader.GetColumnNames())
            {
                this.SetPropertyValue(columnName, reader[columnName]);
            }
            this.IsLoaded = true;
        }

        public void Delete()
        {
            if ((from o in this.Columns
                 where o.Value.SqlType == Configuration.DataAccess.Base.BD_TimeStamp_SqlType_Name
                 select o).Count() > 0)
            {
                KeyValuePair<PropertyInfo, BD_ColumnDefinition> column = (from o in this.Columns
                                                                          where o.Value.SqlType == Configuration.DataAccess.Base.BD_TimeStamp_SqlType_Name
                                                                          select o).FirstOrDefault();
                KeyValuePair<string, List<SqlParameter>> whereClause = this.GetWhereClauseByAllPrimaryKeys();
                if (whereClause.IsNullOrEmptyNot())
                {
                    Sql.ExecuteScalar("UPDATE [" + this.TableName + "] SET [" + column.Key.Name + "].MarkAsDeleted() WHERE " + whereClause.Key, whereClause.Value.ToArray());
                }
                else
                {
                    throw new ArgumentException("Delete method is only supported when primary keys are available.");
                }
            }
            else
            {
                this.DeleteForever();
            }
        }

        public void DeleteForever()
        {
            KeyValuePair<string, List<SqlParameter>> whereClause = this.GetWhereClauseByAllPrimaryKeys();
            if (whereClause.IsNullOrEmptyNot())
            {
                Sql.ExecuteNonQuery("DELETE FROM [" + this.TableName + "] WHERE " + whereClause.Key, whereClause.Value.ToArray());
            }
            else
            {
                throw new ArgumentException("Delete method is only supported when primary keys are available.");
            }
        }

        public List<T> GetAll(string orderBy = null)
        {
            return this.GetAll(orderBy, null);
        }

        public List<T> GetAll(string orderBy = null, string whereClause = null, SqlParameter[] sqlParameters = null, bool allowDeletedItems = false)
        {
            var results = new List<T>();

            if (!allowDeletedItems && (from o in this.Columns
                                       where o.Value.SqlType == Configuration.DataAccess.Base.BD_TimeStamp_SqlType_Name
                                       select o).Count() > 0)
            {
                KeyValuePair<PropertyInfo, BD_ColumnDefinition> column = (from o in this.Columns
                                                                          where o.Value.SqlType == Configuration.DataAccess.Base.BD_TimeStamp_SqlType_Name
                                                                          select o).FirstOrDefault();
                if (whereClause.IsNullOrEmptyNot())
                {
                    whereClause = "[" + column.Value.GetFieldName() + "].IsDeleted = 0 AND " + whereClause;
                }
                else
                {
                    whereClause = "[" + column.Value.GetFieldName() + "].IsDeleted = 0";
                }
            }

            Sql.ExecuteReader(reader => { results.Add((T)Activator.CreateInstance(typeof(T), reader)); }, this.GetBasicSelectFrom() + (whereClause.IsNullOrEmpty() ? string.Empty : " WHERE " + whereClause) + (orderBy.IsNullOrEmpty() ? string.Empty : " ORDER BY " + orderBy), sqlParameters);

            return results;
        }

        public void Save()
        {
            if (this.IsLoaded)
            {
                this.Update();
            }
            else
            {
                this.Create();
            }
        }

        #endregion

        #region Methods

        protected void Create()
        {
            string fullQuery = string.Empty;
            string readOnlyColumnsQuery = string.Empty;
            string columnsQuery = string.Empty;
            string ouputCLRColumnsQuery = string.Empty;
            string columnParamsQuery = string.Empty;
            var sqlParameters = new SqlParameter[this.Columns.Count];
            int index = -1;
            foreach (var item in this.Columns)
            {
                index++;
                if (!item.Value.ReadOnly)
                {
                    if (columnsQuery.Length > 0)
                    {
                        columnsQuery += ",";
                        columnParamsQuery += ",";
                    }
                    columnsQuery += "[" + item.Value.Name + "]";
                    if (item.Value.ColumnType == eColumnType.CLR)
                    {
                        columnParamsQuery += item.Value.SqlType + "::Parse(@" + item.Key.Name + ")";
                        sqlParameters[index] = this.GetClrSqlParameter(item, ParameterDirection.InputOutput);
                        readOnlyColumnsQuery = this.ConfigureAsReadonly(item, sqlParameters, index, readOnlyColumnsQuery);
                    }
                    else
                    {
                        columnParamsQuery += "@" + item.Key.Name;
                        sqlParameters[index] = sqlParameters[index] = this.GetStandardSqlParameter(item, ParameterDirection.Input);
                    }
                }
                else
                {
                    readOnlyColumnsQuery = this.ConfigureAsReadonly(item, sqlParameters, index, readOnlyColumnsQuery);
                }
            }

            this.ConfigureOutputAndExecute(readOnlyColumnsQuery, "INSERT INTO [" + this.TableName + "] (" + columnsQuery + ") VALUES (" + columnParamsQuery + ") ", sqlParameters);

            this.IsLoaded = true;
        }

        protected byte[] GetBlob(string columnName)
        {
            if (!this._blogStorage.ContainsKey(columnName))
            {
                KeyValuePair<string, List<SqlParameter>> whereClause = this.GetWhereClauseByAllPrimaryKeys();
                if (whereClause.IsNullOrEmptyNot())
                {
                    this.SetBlob(columnName, (byte[])Sql.ExecuteScalar("SELECT TOP 1 [" + columnName + "] FROM [" + this.TableName + "] WHERE " + whereClause.Key, whereClause.Value.ToArray()));
                }
                else
                {
                    throw new ArgumentException("Lazy Blob loading is only supported when Primary Keys are available.");
                }
            }

            return this._blogStorage[columnName];
        }

        protected int GetBlobSize(string columnName)
        {
            if (!this._blogStorageSizes.ContainsKey(columnName))
            {
                this.SetBlobSize(columnName, this.GetBlob(columnName).Length);
            }

            return this._blogStorageSizes[columnName];
        }

        protected void Reset()
        {
            foreach (var item in this.Columns)
            {
                if (item.Value.Is_Nullable)
                {
                    item.Key.SetValue(this, null);
                }
                else
                {
                    item.Key.SetValue(this, item.Value.DotNetType.GetDefault());
                }
            }
            this.IsLoaded = false;
        }

        protected void SetBlob(string columnName, byte[] blob)
        {
            if (this._blogStorage.ContainsKey(columnName))
            {
                this._blogStorage[columnName] = blob;
            }
            else
            {
                this._blogStorage.Add(columnName, blob);
            }
        }

        protected void SetBlobSize(string columnName, int length)
        {
            if (this._blogStorageSizes.ContainsKey(columnName))
            {
                this._blogStorageSizes[columnName] = length;
            }
            else
            {
                this._blogStorageSizes.Add(columnName, length);
            }
        }

        protected void Update()
        {
            string fullQuery = string.Empty;
            string readOnlyColumnsQuery = string.Empty;
            string columnsQuery = string.Empty;
            string ouputCLRColumnsQuery = string.Empty;
            var sqlParameters = new SqlParameter[this.Columns.Count];
            int index = -1;
            foreach (var item in this.Columns)
            {
                index++;
                if (!item.Value.ReadOnly)
                {
                    if (columnsQuery.Length > 0)
                    {
                        columnsQuery += ",";
                    }
                    columnsQuery += "[" + item.Value.Name + "] = ";
                    if (item.Value.ColumnType == eColumnType.CLR)
                    {
                        columnsQuery += item.Value.SqlType + "::Parse(@" + item.Key.Name + ")";
                        sqlParameters[index] = this.GetClrSqlParameter(item, ParameterDirection.InputOutput);
                        readOnlyColumnsQuery = this.ConfigureAsReadonly(item, sqlParameters, index, readOnlyColumnsQuery);
                    }
                    else
                    {
                        columnsQuery += "@" + item.Key.Name;
                        sqlParameters[index] = sqlParameters[index] = this.GetStandardSqlParameter(item, ParameterDirection.Input);
                    }
                }
                else
                {
                    readOnlyColumnsQuery = this.ConfigureAsReadonly(item, sqlParameters, index, readOnlyColumnsQuery);
                }
            }
            this.ConfigureOutputAndExecute(readOnlyColumnsQuery, "UPDATE [" + this.TableName + "] SET " + columnsQuery + " <{OUTPUT WHERE CLAUSE}>", sqlParameters);

            this.IsLoaded = true;
        }

        private void ChangeTo(DataRow dr)
        {
            this.Reset();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                this.SetPropertyValue(dc.ColumnName, dr[dc.ColumnName]);
            }
            this.IsLoaded = true;
        }

        private string ConfigureAsReadonly(KeyValuePair<PropertyInfo, BD_ColumnDefinition> item, SqlParameter[] sqlParameters, int index, string readOnlyColumnsQuery)
        {
            if (readOnlyColumnsQuery.Length > 0)
            {
                readOnlyColumnsQuery += ",";
            }

            if (item.Value.ColumnType == eColumnType.CLR)
            {
                readOnlyColumnsQuery += "@" + item.Key.Name + " = [" + item.Value.Name + "].ToString()";
                if (sqlParameters != null)
                {
                    sqlParameters[index] = this.GetClrSqlParameter(item, ParameterDirection.Input);
                }
            }
            else
            {
                readOnlyColumnsQuery += "@" + item.Key.Name + " = [" + item.Value.Name + "]";
                if (sqlParameters != null)
                {
                    sqlParameters[index] = this.GetStandardSqlParameter(item, ParameterDirection.InputOutput);
                }
            }
            return readOnlyColumnsQuery;
        }

        private void ConfigureOutputAndExecute(string readOnlyColumnsQuery, string executeQuery, SqlParameter[] sqlParameters)
        {
            int index = -1;
            string outputQuery = string.Empty;
            string preOutputQuery = string.Empty;
            string outputQueryWhereClause = string.Empty;

            if (this.PrimaryKeyColumns.Count() > 1 || executeQuery.StartsWith("UPDATE"))
            {
                outputQueryWhereClause = this.GetWhereClauseByAllPrimaryKeys().Key;
            }
            else if (this.PrimaryKeyColumns.Count() == 1)
            {
                foreach (var item in this.PrimaryKeyColumns)
                {
                    if (item.Value.Is_Identity)
                    {
                        preOutputQuery = "declare @scope_identity " + item.Value.SqlType + " = SCOPE_IDENTITY()" + Environment.NewLine;
                        //preOutputQuery = "declare @scope_identity " + item.Value.SqlType + " = @@IDENTITY" + Environment.NewLine;

                        preOutputQuery += "" + Environment.NewLine;

                        outputQueryWhereClause = "[" + item.Value.Name + "] = @scope_identity";
                    }
                    else
                    {
                        outputQueryWhereClause += "[" + item.Value.Name + "] = @" + item.Key.Name;
                    }

                    break;
                }
            }
            if (outputQueryWhereClause.IsNullOrEmptyNot())
            {
                if (readOnlyColumnsQuery.IsNullOrEmptyNot())
                {
                    outputQuery = preOutputQuery + "SELECT " + readOnlyColumnsQuery + " FROM " + this.TableName + " WHERE " + outputQueryWhereClause;
                }
                executeQuery = executeQuery.Replace("<{OUTPUT WHERE CLAUSE}>", " WHERE " + outputQueryWhereClause);
            }

            Sql.ExecuteScalar(executeQuery + Environment.NewLine + Environment.NewLine + outputQuery, sqlParameters, success: context =>
            {
                if (outputQuery.IsNullOrEmptyNot())
                {
                    index = -1;
                    foreach (SqlParameter sqlParameter in context.SqlParameters)
                    {
                        index++;
                        if (sqlParameter.Direction == ParameterDirection.Output ||
                            sqlParameter.Direction == ParameterDirection.InputOutput)
                        {
                            foreach (var item in this.Columns)
                            {
                                if (string.Compare(item.Key.Name, sqlParameter.ParameterName.TrimStart('@'), true) == 0)
                                {
                                    if (sqlParameter.Value == DBNull.Value)
                                    {
                                        item.Key.SetValue(this, null);
                                    }
                                        //else if (sqlParameter.ParameterName.TrimStart('@').StartsWith(clr_temp_))
                                    else if (item.Value.ColumnType == eColumnType.CLR)
                                    {
                                        item.Key.SetValue(this, item.Value.DotNetType.InvokeMember("Parse", BindingFlags.Static, Type.DefaultBinder, null, new[] { sqlParameter.Value }));
                                    }
                                    else
                                    {
                                        item.Key.SetValue(this, sqlParameter.Value);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            });
        }

        private string GetBasicSelectFrom()
        {
            string result = string.Empty;

            foreach (var item in this.Columns)
            {
                if (result.Length > 0)
                {
                    result += ",";
                }
                if (item.Value.ColumnType == eColumnType.CLR)
                {
                    result += "[" + item.Value.Name + "].ToString() as [" + item.Value.Name + "]";
                }
                else
                {
                    if (item.Value.SqlType == "System.Byte[]")
                    {
                        result += "DataLength([" + item.Value.Name + "]) as " + item.Value.GetFieldName() + "Length,";
                    }
                    result += "[" + item.Value.Name + "]";
                }
            }

            return "SELECT " + result + Environment.NewLine + "FROM " + this.TableName + Environment.NewLine;
        }

        private SqlParameter GetClrSqlParameter(KeyValuePair<PropertyInfo, BD_ColumnDefinition> item, ParameterDirection direction)
        {
            return new SqlParameter
                   {
                       Direction = direction,
                       ParameterName = "@" + item.Key.Name,
                       Value = item.Key.GetValue(this).ToStringSafe(null, null)
                       //,UdtTypeName = item.Value.ColumnType == eColumnType.CLR ? item.Value.SqlType : null
                   };
        }

        private SqlParameter GetStandardSqlParameter(KeyValuePair<PropertyInfo, BD_ColumnDefinition> item, ParameterDirection direction)
        {
            return new SqlParameter
                   {
                       Direction = direction,
                       IsNullable = item.Value.Is_Nullable,
                       ParameterName = "@" + item.Key.Name,
                       Precision = item.Value.Precision,
                       Scale = item.Value.Scale,
                       Size = item.Value.Max_Length,
                       SourceColumn = item.Value.Name,
                       SqlDbType = item.Value.SqlDbType,
                       Value = item.Key.GetValue(this)
                       //,UdtTypeName = item.Value.ColumnType == eColumnType.CLR ? "dbo." + item.Value.SqlType : null
                   };
        }

        private KeyValuePair<string, List<SqlParameter>> GetWhereClauseByAllPrimaryKeys()
        {
            string resultString = string.Empty;
            var resultSqlParameters = new List<SqlParameter>();

            foreach (var item in this.PrimaryKeyColumns)
            {
                if (resultString.IsNullOrEmptyNot())
                {
                    resultString += " AND ";
                }

                resultString += "[" + item.Value.Name + "] = @" + item.Key.Name;
                resultSqlParameters.Add(new SqlParameter("@" + item.Key.Name, item.Key.GetValue(this)));
            }
            return new KeyValuePair<string, List<SqlParameter>>(resultString, resultSqlParameters);
        }

        private void SetPropertyValue(string sqlColumnName, object value)
        {
            foreach (var item in this.Columns)
            {
                if (string.Compare(item.Value.Name, sqlColumnName, true) == 0)
                {
                    if (value == DBNull.Value)
                    {
                        item.Key.SetValue(this, null);
                    }
                    else if (item.Value.ColumnType == eColumnType.CLR)
                    {
                        item.Key.SetValue(this, item.Value.DotNetType.InvokeMember("Parse", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, Type.DefaultBinder, null, new object[] { new SqlString(value.ToStringSafe(null, null)) }));
                    }
                    else
                    {
                        item.Key.SetValue(this, value);
                    }
                    break;
                }
            }
        }

        #endregion
    }
}