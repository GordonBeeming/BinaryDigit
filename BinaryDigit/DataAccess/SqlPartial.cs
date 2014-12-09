namespace BinaryDigit.DataAccess
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public static partial class Sql
    {
        #region Public Methods and Operators

        public static DataSet GetColumns(string standardColumnsToReturn, string clrColumnsToReturn, string tableName, string databaseName = null, string connectionString = null)
        {
            return FetchDataSet(@" select " + standardColumnsToReturn + @"
      ,CAST((SELECT ISNULL((SELECT TOP 1 1
                    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                    WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1
                            and column_name = sys.columns.name
                            AND table_name = @tableName),0)) as BIT ) as IsPrimaryKey
from sys.columns
inner join sys.types on sys.types.system_type_id = sys.columns.system_type_id and sys.types.system_type_id <> 240
where object_name(object_id) = @tableName AND sys.types.name <> 'sysname'
order by column_id

select " + clrColumnsToReturn + @"
      ,CAST((SELECT ISNULL((SELECT TOP 1 1
                    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                    WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1
                            and column_name = sys.columns.name
                            AND table_name = @tableName),0)) as BIT ) as IsPrimaryKey
from sys.columns
inner join sys.assembly_types on sys.assembly_types.system_type_id = sys.columns.system_type_id
                                    and sys.assembly_types.user_type_id = sys.columns.user_type_id
where object_name(object_id) = @tableName AND sys.assembly_types.name <> 'sysname'
order by column_id", new[] { new SqlParameter("tableName", tableName) }, connectionString: connectionString, databaseName: databaseName);
        }

        public static DataSet GetColumnsBasic(string tableName, string databaseName = null, string connectionString = null)
        {
            return GetColumns(@"sys.columns.name as columns_name
      , sys.types.name as types_name 
      ,column_id
      ,sys.columns.is_nullable
      ,sys.columns.max_length
      ,sys.columns.precision
      ,sys.columns.scale
      ,sys.columns.is_identity
      ,sys.columns.is_rowguidcol
      ,sys.columns.is_computed
      ,sys.columns.is_filestream", @"sys.columns.name as columns_name
      , sys.assembly_types.name as assembly_types_name 
      ,column_id
      ,sys.assembly_types.assembly_id
      ,assembly_class
      ,assembly_qualified_name
      ,sys.columns.is_nullable
      ,sys.columns.max_length
      ,sys.columns.precision
      ,sys.columns.scale
      ,sys.columns.is_identity
      ,sys.columns.is_rowguidcol
      ,sys.columns.is_computed
      ,sys.columns.is_filestream", tableName, databaseName, connectionString);
        }

        public static List<string> GetDatabases(string connectionString = null)
        {
            var result = new List<string>();

            foreach (DataRow dr in FetchDataTable(@"
DECLARE @DBuser_sql VARCHAR(4000) 
DECLARE @DBuser_table TABLE (DBName VARCHAR(200))--, UserName VARCHAR(250), LoginType VARCHAR(500), AssociatedRole VARCHAR(200)) 
SET @DBuser_sql='SELECT ''[?]'' AS DBName --,a.name AS Name,a.type_desc AS LoginType,USER_NAME(b.role_principal_id) AS AssociatedRole 
FROM [?].sys.database_principals a 
LEFT OUTER JOIN [?].sys.database_role_members b ON a.principal_id=b.member_principal_id 
WHERE a.sid NOT IN (0x01,0x00) AND a.sid IS NOT NULL AND a.type NOT IN (''C'') AND a.is_fixed_role <> 1 AND a.name NOT LIKE ''##%'' AND ''?'' NOT IN (''master'',''msdb'',''model'',''tempdb'') ORDER BY Name'
INSERT @DBuser_table 
EXEC sp_MSforeachdb @command1=@dbuser_sql 
SELECT DISTINCT * FROM @DBuser_table 
ORDER BY DBName", connectionString: connectionString).Rows)
            {
                result.Add(dr[0].ToString());
            }

            return result;
        }

        public static List<string> GetTables(string databaseName = null, string connectionString = null, string columnsToReturn = "*")
        {
            var result = new List<string>();

            foreach (DataRow dr in FetchDataTable(@" select " + columnsToReturn + @" 
from sys.tables
where is_ms_shipped = 0 and name <> 'sysdiagrams'
order by name", connectionString: connectionString, databaseName: databaseName).Rows)
            {
                result.Add(dr[0].ToString());
            }

            return result;
        }

        #endregion
    }
}