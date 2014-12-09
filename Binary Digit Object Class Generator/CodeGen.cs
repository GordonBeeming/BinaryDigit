namespace Binary_Digit_Object_Class_Generator
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using BinaryDigit.DataAccess;

    public class CodeGen
    {
        #region Fields

        private readonly List<BD_ColumnDefinition> _columns;

        private readonly string _tableName;

        private string _connectionString;

        private string _databaseName;

        #endregion

        #region Constructors and Destructors

        public CodeGen(string tableName, string databaseName = null, string connectionString = null)
        {
            this._tableName = tableName;
            this._databaseName = databaseName;
            this._connectionString = connectionString;

            this._columns = new List<BD_ColumnDefinition>();
            DataSet dsColumns = Sql.GetColumnsBasic(this._tableName, this._databaseName, this._connectionString);
            foreach (DataRow dr in dsColumns.Tables[0].Rows) //Standard Columns
            {
                this._columns.Add(new BD_ColumnDefinition(
                    Convert.ToInt32(dr["column_id"].ToString()),
                    dr["columns_name"].ToString(),
                    dr["types_name"].ToString(),
                    this.GetSqlDbType(dr),
                    this.GetDotNetType(dr),
                    (bool)dr["is_nullable"],
                    eColumnType.Standard,
                    Convert.ToInt32(dr["max_length"].ToString()),
                    Convert.ToByte(dr["precision"].ToString()),
                    Convert.ToByte(dr["scale"].ToString()),
                    (bool)dr["IsPrimaryKey"],
                    (bool)dr["is_identity"],
                    (bool)dr["is_rowguidcol"],
                    (bool)dr["is_computed"],
                    (bool)dr["is_filestream"]));
            }
            foreach (DataRow dr in dsColumns.Tables[1].Rows) //CLR Columns
            {
                this._columns.Add(new BD_ColumnDefinition(
                    Convert.ToInt32(dr["column_id"].ToString()),
                    dr["columns_name"].ToString(),
                    dr["assembly_types_name"].ToString(),
                    SqlDbType.Udt,
                    dr["assembly_class"].ToString(),
                    (bool)dr["is_nullable"],
                    eColumnType.CLR,
                    Convert.ToInt32(dr["assembly_id"].ToString()),
                    dr["assembly_class"].ToString(),
                    dr["assembly_qualified_name"].ToString(),
                    Convert.ToInt32(dr["max_length"].ToString()),
                    Convert.ToByte(dr["precision"].ToString()),
                    Convert.ToByte(dr["scale"].ToString()),
                    (bool)dr["IsPrimaryKey"],
                    (bool)dr["is_identity"],
                    (bool)dr["is_rowguidcol"],
                    (bool)dr["is_computed"],
                    (bool)dr["is_filestream"]));
            }
        }

        #endregion

        #region Public Methods and Operators

        public string GetClassObject()
        {
            string result = string.Empty;

            result += @" public sealed class " + this._tableName.Singularize() + @" : BinaryDigit.DataAccess.Base.BD_DataObjectBase<" + this._tableName.Singularize() + @">
    {
        public override string TableName
        {
            get { return " + "\"" + this._tableName + "\"" + @"; }
        }";

            //get extra constructors
            result += this.GetConstructors();

            //get all properties
            result += this.GetProperties();

            //closing class braces
            result += @"
    }";

            return result;
        }

        #endregion

        #region Methods

        protected string GetConstructors()
        {
            string result = string.Empty;

            result += Environment.NewLine + Environment.NewLine + @" public " + this._tableName.Singularize() + @"() : base() { } public " + this._tableName.Singularize() + @"(string whereClause, System.Data.SqlClient.SqlParameter[] sqlParameters) : base(whereClause,sqlParameters) { } public " + this._tableName.Singularize() + @"(System.Data.DataTable input) : base(input) { } public " + this._tableName.Singularize() + @"(System.Data.SqlClient.SqlDataReader reader) : base(reader) { }

        ";
            IEnumerable<BD_ColumnDefinition> columnsForConstructor = (from o in this._columns
                                                                      where o.Is_PrimaryKey
                                                                      select o);
            if (columnsForConstructor.Count() > 0)
            {
                result += this.GenerateConstructorUsingColumns(columnsForConstructor);
            }

            columnsForConstructor = (from o in this._columns
                                     where o.Is_RowGuidCol && !o.Is_PrimaryKey
                                     select o);
            if (columnsForConstructor.Count() > 0)
            {
                result += this.GenerateConstructorUsingColumns(columnsForConstructor);
            }

            return result;
        }

        protected string GetProperties()
        {
            string result = string.Empty;

            result += Environment.NewLine + "#region Properties" + Environment.NewLine + Environment.NewLine;

            foreach (BD_ColumnDefinition column in this._columns)
            {
                if (column.DotNetTypeString == "System.Byte[]")
                {
                    result += "public int " + column.GetFieldName() + "Length { get { return GetBlobSize(\"" + column.Name + "\"); } set { SetBlobSize(\"" + column.Name + "\", value); } }" + Environment.NewLine + Environment.NewLine;
                }

                result += column.GetAsFeildAttribute() + Environment.NewLine;
                result += column.GetAsFeildDeclaration() + Environment.NewLine + Environment.NewLine;
            }

            result += Environment.NewLine + Environment.NewLine + "#endregion" + Environment.NewLine + Environment.NewLine;

            return result;
        }

        private string GenerateConstructorUsingColumns(IEnumerable<BD_ColumnDefinition> columnsForConstructor)
        {
            string result = string.Empty;

            string parametersPrimaryKey = string.Empty;
            string whereClausePrimaryKey = string.Empty;
            string sqlParametersPrimaryKey = string.Empty;

            foreach (BD_ColumnDefinition column in columnsForConstructor)
            {
                if (parametersPrimaryKey.Length > 0)
                {
                    parametersPrimaryKey += ",";
                    sqlParametersPrimaryKey += "," + Environment.NewLine;
                    whereClausePrimaryKey += " AND ";
                }
                parametersPrimaryKey += column.DotNetTypeString + " " + column.GetFieldName(true);
                whereClausePrimaryKey += "[" + column.Name + "] = @" + column.GetFieldName();
                sqlParametersPrimaryKey += "                                                                        new System.Data.SqlClient.SqlParameter(\"@" + column.GetFieldName() + "\"," + column.GetFieldName(true) + ")";
            }

            result += "public " + this._tableName.Singularize() + @"(" + parametersPrimaryKey + @")
          {
              ChangeTo(" + "\"" + whereClausePrimaryKey + "\"" + @", new System.Data.SqlClient.SqlParameter[]
                                                                      {
  " + sqlParametersPrimaryKey + @"
                                                                      });
          }" + Environment.NewLine + Environment.NewLine;

            return result;
        }

        private string GetDotNetType(DataRow dr)
        {
            string result = string.Empty;

            switch (dr["types_name"].ToString().ToLower())
            {
                case "bit":
                    result = "System.Boolean";
                    break;
                case "tinyint":
                    result = "System.Byte";
                    break;
                case "varbinary":
                    result = "System.Byte[]";
                    break;
                case "datetimeoffset":
                    result = "System.DateTimeOffset";
                    break;
                case "decimal":
                    result = "System.Decimal";
                    break;
                case "float":
                    result = "System.Double";
                    break;
                case "real":
                    result = "System.Single";
                    break;
                case "uniqueidentifier":
                    result = "System.Guid";
                    break;
                case "smallint":
                    result = "System.Int16";
                    break;
                case "int":
                    result = "System.Int32";
                    break;
                case "bigint":
                    result = "System.Int64";
                    break;
                case "variant":
                    result = "System.Object";
                    break;
                case "nvarchar":
                    result = "System.String";
                    break;
                case "time":
                    result = "System.TimeSpan";
                    break;
                case "varchar":
                    result = "System.String";
                    break;
                case "char":
                    result = "System.String";
                    break;
                case "money":
                    result = "System.Decimal";
                    break;
                case "date":
                case "datetime":
                    result = "System.DateTime";
                    break;
                case "nchar":
                    result = "System.String";
                    break;
                case "Bit":
                    result = "System.Boolean";
                    break;
                default:
                    result = "object";
                    break;
            }

            return result;
        }

        private SqlDbType GetSqlDbType(DataRow dr)
        {
            var result = SqlDbType.Variant;

            switch (dr["types_name"].ToString().ToLower())
            {
                case "bit":
                    result = SqlDbType.Bit;
                    break;
                case "tinyint":
                    result = SqlDbType.TinyInt;
                    break;
                case "varbinary":
                    result = SqlDbType.VarBinary;
                    break;
                case "datetimeoffset":
                    result = SqlDbType.DateTimeOffset;
                    break;
                case "decimal":
                    result = SqlDbType.Decimal;
                    break;
                case "float":
                    result = SqlDbType.Float;
                    break;
                case "real":
                    result = SqlDbType.Real;
                    break;
                case "uniqueidentifier":
                    result = SqlDbType.UniqueIdentifier;
                    break;
                case "smallint":
                    result = SqlDbType.SmallInt;
                    break;
                case "int":
                    result = SqlDbType.Int;
                    break;
                case "bigint":
                    result = SqlDbType.BigInt;
                    break;
                case "variant":
                    result = SqlDbType.Variant;
                    break;
                case "nvarchar":
                    result = SqlDbType.NVarChar;
                    break;
                case "time":
                    result = SqlDbType.Time;
                    break;
                case "varchar":
                    result = SqlDbType.VarChar;
                    break;
                case "char":
                    result = SqlDbType.Char;
                    break;
                case "money":
                    result = SqlDbType.Money;
                    break;
                case "date":
                case "datetime":
                    result = SqlDbType.DateTime;
                    break;
                case "nchar":
                    result = SqlDbType.NChar;
                    break;
                case "Bit":
                    result = SqlDbType.Bit;
                    break;
            }

            return result;
        }

        #endregion
    }
}