namespace BinaryDigit.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public static partial class Sql
    {
        #region Delegates

        public delegate void DelegateSqlDataReaderRowWork(SqlDataReader reader);

        public delegate bool DelegateSqlWorkFailed(SqlWorkContext context);

        public delegate void DelegateSqlWorkSuccess(SqlWorkContext context);

        private delegate object DelegateCommonWork(SqlCommand command, SqlDataReader reader);

        #endregion

        #region Public Methods and Operators

        public static int ExecuteNonQuery(string sqlQuery, SqlParameter[] sqlParameters = null, CommandType sqlCommandType = CommandType.Text, int sqlCommandTimeout = 30, SqlTransaction sqlTransaction = null, DelegateSqlWorkSuccess success = null, DelegateSqlWorkFailed failed = null, bool startNewTransaction = false, string connectionString = null, string databaseName = null)
        {
            int result = -1;

            CommonWork((command, reader) =>
            {
                result = command.ExecuteNonQuery();
                return result;
            }, sqlQuery, sqlParameters, sqlCommandType, sqlCommandTimeout, sqlTransaction, success, failed, startNewTransaction, connectionString, databaseName);

            return result;
        }

        public static void ExecuteReader(DelegateSqlDataReaderRowWork worker, string sqlQuery, SqlParameter[] sqlParameters = null, CommandType sqlCommandType = CommandType.Text, int sqlCommandTimeout = 30, SqlTransaction sqlTransaction = null, DelegateSqlWorkSuccess success = null, DelegateSqlWorkFailed failed = null, bool startNewTransaction = false, string connectionString = null, string databaseName = null)
        {
            CommonWork((command, reader) =>
            {
                reader = command.ExecuteReader();
                if (worker != null)
                {
                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            worker(reader);
                        }
                    }
                }
                return null;
            }, sqlQuery, sqlParameters, sqlCommandType, sqlCommandTimeout, sqlTransaction, success, failed, startNewTransaction, connectionString, databaseName);
        }

        public static object ExecuteScalar(string sqlQuery, SqlParameter[] sqlParameters = null, CommandType sqlCommandType = CommandType.Text, int sqlCommandTimeout = 30, SqlTransaction sqlTransaction = null, DelegateSqlWorkSuccess success = null, DelegateSqlWorkFailed failed = null, bool startNewTransaction = false, string connectionString = null, string databaseName = null)
        {
            object result = null;

            CommonWork((command, reader) =>
            {
                result = command.ExecuteScalar();
                return result;
            }, sqlQuery, sqlParameters, sqlCommandType, sqlCommandTimeout, sqlTransaction, success, failed, startNewTransaction, connectionString, databaseName);

            return result;
        }

        public static T ExecuteScalar<T>(string sqlQuery, SqlParameter[] sqlParameters = null, CommandType sqlCommandType = CommandType.Text, int sqlCommandTimeout = 30, SqlTransaction sqlTransaction = null, DelegateSqlWorkSuccess success = null, DelegateSqlWorkFailed failed = null, bool startNewTransaction = false, string connectionString = null, string databaseName = null)
        {
            object result = null;

            CommonWork((command, reader) =>
            {
                result = command.ExecuteScalar();
                return result;
            }, sqlQuery, sqlParameters, sqlCommandType, sqlCommandTimeout, sqlTransaction, success, failed, startNewTransaction, connectionString, databaseName);

            return (T)result;
        }

        public static DataSet FetchDataSet(string sqlQuery, SqlParameter[] sqlParameters = null, CommandType sqlCommandType = CommandType.Text, int sqlCommandTimeout = 30, SqlTransaction sqlTransaction = null, DelegateSqlWorkSuccess success = null, DelegateSqlWorkFailed failed = null, bool startNewTransaction = false, string connectionString = null, string databaseName = null)
        {
            var result = new DataSet();

            SqlConnection connec = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            try
            {
                connec = new SqlConnection(connectionString.IsNullOrEmpty() ? Configuration.DefaultConnectionString : connectionString);
                connec.Open();
                if (databaseName.IsNullOrEmptyNot())
                {
                    connec.ChangeDatabase(databaseName.TrimStart('[').TrimEnd(']'));
                }
                if (startNewTransaction)
                {
                    sqlTransaction = connec.BeginTransaction();
                }
                command = new SqlCommand(sqlQuery, connec);
                command.CommandTimeout = sqlCommandTimeout;
                command.CommandType = sqlCommandType;
                if (sqlTransaction != null)
                {
                    command.Transaction = sqlTransaction;
                }
                if (sqlParameters != null)
                {
                    foreach (SqlParameter sqlParameter in sqlParameters)
                    {
                        command.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.SqlDbType, sqlParameter.Size, sqlParameter.Direction, sqlParameter.Precision, sqlParameter.Scale, sqlParameter.SourceColumn, sqlParameter.SourceVersion, sqlParameter.SourceColumnNullMapping, sqlParameter.Value == null ? DBNull.Value : sqlParameter.Value, sqlParameter.XmlSchemaCollectionDatabase, sqlParameter.XmlSchemaCollectionOwningSchema, sqlParameter.XmlSchemaCollectionName));
                    }
                }

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(result);

                if (success != null)
                {
                    success(new SqlWorkContext(sqlTransaction, result, command.Parameters, null));
                }
            }
            catch (Exception ex)
            {
                if (failed != null)
                {
                    if (!failed(new SqlWorkContext(sqlTransaction, null, command.Parameters, ex)))
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
                if (connec != null)
                {
                    if (connec.State != ConnectionState.Closed)
                    {
                        connec.Close();
                    }
                    connec.Dispose();
                    connec = null;
                }
            }

            return result;
        }

        public static DataTable FetchDataTable(string sqlQuery, SqlParameter[] sqlParameters = null, CommandType sqlCommandType = CommandType.Text, int sqlCommandTimeout = 30, SqlTransaction sqlTransaction = null, DelegateSqlWorkSuccess success = null, DelegateSqlWorkFailed failed = null, bool startNewTransaction = false, string connectionString = null, string databaseName = null)
        {
            var result = new DataTable();

            CommonWork((command, reader) =>
            {
                reader = command.ExecuteReader();
                if (reader != null)
                {
                    //result.Load(reader, LoadOption.OverwriteChanges, new FillErrorEventHandler((obj, args) =>
                    //{
                    //    args.Continue = true;//This is done for when you are selecting * and a UDT is within the results.
                    //}));
                    result.Load(reader, LoadOption.OverwriteChanges);
                    //result.Load(reader);
                }
                return result;
            }, sqlQuery, sqlParameters, sqlCommandType, sqlCommandTimeout, sqlTransaction, success, failed, startNewTransaction, connectionString, databaseName);

            return result;
        }

        #endregion

        #region Methods

        private static void CommonWork(DelegateCommonWork work, string sqlQuery, SqlParameter[] sqlParameters, CommandType sqlCommandType, int sqlCommandTimeout, SqlTransaction sqlTransaction, DelegateSqlWorkSuccess success, DelegateSqlWorkFailed failed, bool startNewTransaction, string connectionString, string databaseName)
        {
            SqlConnection connec = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            try
            {
                connec = new SqlConnection(connectionString.IsNullOrEmpty() ? Configuration.DefaultConnectionString : connectionString);
                connec.Open();
                if (databaseName.IsNullOrEmptyNot())
                {
                    connec.ChangeDatabase(databaseName.TrimStart('[').TrimEnd(']'));
                }
                if (startNewTransaction)
                {
                    sqlTransaction = connec.BeginTransaction();
                }
                command = new SqlCommand(sqlQuery, connec);
                command.CommandTimeout = sqlCommandTimeout;
                command.CommandType = sqlCommandType;
                if (sqlTransaction != null)
                {
                    command.Transaction = sqlTransaction;
                }
                if (sqlParameters != null)
                {
                    foreach (SqlParameter sqlParameter in sqlParameters)
                    {
                        command.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.SqlDbType, sqlParameter.Size, sqlParameter.Direction, sqlParameter.Precision, sqlParameter.Scale, sqlParameter.SourceColumn, sqlParameter.SourceVersion, sqlParameter.SourceColumnNullMapping, sqlParameter.Value == null ? DBNull.Value : sqlParameter.Value, sqlParameter.XmlSchemaCollectionDatabase, sqlParameter.XmlSchemaCollectionOwningSchema, sqlParameter.XmlSchemaCollectionName));
                    }
                }

                if (work != null)
                {
                    if (success != null)
                    {
                        success(new SqlWorkContext(sqlTransaction, work(command, reader), command.Parameters, null));
                    }
                    else
                    {
                        work(command, reader);
                    }
                }
                else
                {
                    if (success != null)
                    {
                        success(new SqlWorkContext(sqlTransaction, null, command.Parameters, null));
                    }
                }
            }
            catch (Exception ex)
            {
                if (failed != null)
                {
                    if (!failed(new SqlWorkContext(sqlTransaction, null, command.Parameters, ex)))
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
                if (connec != null)
                {
                    if (connec.State != ConnectionState.Closed)
                    {
                        connec.Close();
                    }
                    connec.Dispose();
                    connec = null;
                }
            }
        }

        #endregion

        public struct SqlWorkContext
        {
            #region Fields

            public SqlTransaction CurrentSqlTransaction;

            public Exception Error;

            public object Result;

            public SqlParameterCollection SqlParameters;

            #endregion

            #region Constructors and Destructors

            public SqlWorkContext(SqlTransaction sqlTransaction, object result, SqlParameterCollection sqlParameters, Exception error)
            {
                this.CurrentSqlTransaction = sqlTransaction;
                this.Result = result;
                this.SqlParameters = sqlParameters;
                this.Error = error;
            }

            #endregion
        }
    }
}