namespace BinaryDigit.DataAccess
{
    using System;
    using System.Data;

    public class BD_ColumnDefinition : Attribute
    {
        #region Fields

        /// <summary>
        ///     N/A in Standard Columns
        ///     assembly_class in CLR Columns
        /// </summary>
        /// <value>The assembly_ class.</value>
        public readonly string Assembly_Class;

        /// <summary>
        ///     N/A in Standard Columns
        ///     assembly_id in CLR Columns
        /// </summary>
        /// <value>The assembly_ ID.</value>
        public readonly int Assembly_ID;

        /// <summary>
        ///     N/A in Standard Columns
        ///     assembly_qualified_name in CLR Columns
        /// </summary>
        /// <value>The name of the assembly_ qualified_.</value>
        public readonly string Assembly_Qualified_Name;

        /// <summary>
        ///     eColumnType.Standard if from Standard Columns
        ///     eColumnType.CLR if from CLR Columns
        /// </summary>
        /// <value>The type of the column.</value>
        public readonly eColumnType ColumnType;

        /// <summary>
        ///     column_id in Standard Columns
        ///     column_id in CLR Columns
        /// </summary>
        /// <value>The column_ ID.</value>
        public readonly int Column_ID;

        /// <summary>
        ///     comments.
        /// </summary>
        /// <value>The comments.</value>
        public readonly string Comments;

        /// <summary>
        ///     Value is worked out based on the value of <paramref name="SqlType" />
        /// </summary>
        /// <value>The type of the SQL db.</value>
        public readonly string DotNetTypeString;

        /// <summary>
        ///     is_computed in Standard Columns
        ///     is_computed in CLR Columns
        /// </summary>
        /// <value>The is_ computed.</value>
        public readonly bool Is_Computed;

        /// <summary>
        ///     is_filestream in Standard Columns
        ///     is_filestream in CLR Columns
        /// </summary>
        /// <value>The is_ file stream.</value>
        public readonly bool Is_FileStream;

        /// <summary>
        ///     is_identity in Standard Columns
        ///     is_identity in CLR Columns
        /// </summary>
        /// <value>The is_ identity.</value>
        public readonly bool Is_Identity;

        /// <summary>
        ///     is_nullable in Standard Columns
        ///     is_nullable in CLR Columns
        /// </summary>
        /// <value>The is nullable.</value>
        public readonly bool Is_Nullable;

        /// <summary>
        ///     IsPrimaryKey in Standard Columns
        ///     IsPrimaryKey in CLR Columns
        /// </summary>
        /// <value>The is primary key.</value>
        public readonly bool Is_PrimaryKey;

        /// <summary>
        ///     is_rowguidcol in Standard Columns
        ///     is_rowguidcol in CLR Columns
        /// </summary>
        /// <value>The is_ row GUID col.</value>
        public readonly bool Is_RowGuidCol;

        /// <summary>
        ///     max_length in Standard Columns
        ///     max_length in CLR Columns
        /// </summary>
        /// <value>The length of the max_.</value>
        public readonly int Max_Length;

        /// <summary>
        ///     columns_name in Standard Columns
        ///     columns_name in CLR Columns
        /// </summary>
        /// <value>The name.</value>
        public readonly string Name;

        /// <summary>
        ///     precision in Standard Columns
        ///     precision in CLR Columns
        /// </summary>
        /// <value>The precision.</value>
        public readonly byte Precision;

        /// <summary>
        ///     scale in Standard Columns
        ///     scale in CLR Columns
        /// </summary>
        /// <value>The scale.</value>
        public readonly byte Scale;

        /// <summary>
        ///     Value is worked out based on the value of <paramref name="SqlType" />
        /// </summary>
        /// <value>The type of the SQL db.</value>
        public readonly SqlDbType SqlDbType;

        /// <summary>
        ///     types_name in Standard Columns
        ///     assembly_types_name in CLR Columns
        /// </summary>
        /// <value>The type of the SQL.</value>
        public readonly string SqlType;

        #endregion

        #region Constructors and Destructors

        public BD_ColumnDefinition(int column_ID, string name, string sqlType, SqlDbType sqlDbType, string dotNetTypeString, bool isNullable, eColumnType columnType)
            : this(column_ID, name, sqlType, sqlDbType, dotNetTypeString, isNullable, columnType, -1, null, null, -1, 0, 0, false, false, false, false, false)
        {
        }

        public BD_ColumnDefinition(int column_ID, string name, string sqlType, SqlDbType sqlDbType, string dotNetTypeString, bool isNullable, eColumnType columnType, int max_Length, byte precision, byte scale, bool is_PrimaryKey, bool is_Identity, bool is_RowGuidCol, bool is_Computed, bool is_FileStream)
            : this(column_ID, name, sqlType, sqlDbType, dotNetTypeString, isNullable, columnType, -1, null, null, max_Length, precision, scale, is_PrimaryKey, is_Identity, is_RowGuidCol, is_Computed, is_FileStream)
        {
        }

        public BD_ColumnDefinition(int column_ID, string name, string sqlType, SqlDbType sqlDbType, string dotNetTypeString, bool isNullable, eColumnType columnType, int assembly_ID, string assembly_Class, string assembly_Qualified_Name)
            : this(column_ID, name, sqlType, sqlDbType, dotNetTypeString, isNullable, columnType, assembly_ID, assembly_Class, assembly_Qualified_Name, -1, 0, 0, false, false, false, false, false)
        {
        }

        public BD_ColumnDefinition(int column_ID, string name, string sqlType, SqlDbType sqlDbType, string dotNetTypeString, bool isNullable, eColumnType columnType, int assembly_ID, string assembly_Class, string assembly_Qualified_Name, int max_Length, byte precision, byte scale, bool is_PrimaryKey, bool is_Identity, bool is_RowGuidCol, bool is_Computed, bool is_FileStream)
        {
            this.Column_ID = column_ID;
            this.Name = name;
            this.SqlType = sqlType;
            this.SqlDbType = sqlDbType;
            this.DotNetTypeString = dotNetTypeString;
            this.Is_Nullable = isNullable;
            this.ColumnType = columnType;
            this.Assembly_ID = assembly_ID;
            this.Assembly_Class = assembly_Class;
            this.Assembly_Qualified_Name = assembly_Qualified_Name;
            this.Max_Length = max_Length;
            this.Precision = precision;
            this.Scale = scale;
            this.Is_PrimaryKey = is_PrimaryKey;
            this.Is_Identity = is_Identity;
            this.Is_RowGuidCol = is_RowGuidCol;
            this.Is_Computed = is_Computed;
            this.Is_FileStream = is_FileStream;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the type of the dot net.
        /// </summary>
        /// <value>The type of the dot net.</value>
        public Type DotNetType
        {
            get
            {
                if (this.ColumnType == eColumnType.CLR)
                {
                    return Type.GetType(this.Assembly_Qualified_Name);
                }
                return Type.GetType(this.DotNetTypeString);
            }
        }

        /// <summary>
        ///     Gets the read only.
        /// </summary>
        /// <value>The read only.</value>
        public bool ReadOnly
        {
            get
            {
                return this.Is_RowGuidCol || this.Is_Computed || this.Is_Identity;
            }
        }

        #endregion

        #region Public Methods and Operators

        public string GetAsFeildAttribute()
        {
            string result = string.Empty;

            switch (this.ColumnType)
            {
                case eColumnType.Standard:
                    result = "[BinaryDigit.DataAccess.BD_ColumnDefinition(" + this.Column_ID + ", \"" + this.Name + "\", \"" + this.SqlType + "\", System.Data.SqlDbType." + this.SqlDbType + ", \"" + this.DotNetTypeString + "\", " + this.Is_Nullable.ToString().ToLower() + ", BinaryDigit.DataAccess.eColumnType.Standard, " + this.Max_Length + ", " + this.Precision + ", " + this.Scale + ", " + this.Is_PrimaryKey.ToString().ToLower() + ", " + this.Is_Identity.ToString().ToLower() + ", " + this.Is_RowGuidCol.ToString().ToLower() + ", " + this.Is_Computed.ToString().ToLower() + ", " + this.Is_FileStream.ToString().ToLower() + ")]";
                    break;
                case eColumnType.CLR:
                    result = "[BinaryDigit.DataAccess.BD_ColumnDefinition(" + this.Column_ID + ", \"" + this.Name + "\", \"" + this.SqlType + "\", System.Data.SqlDbType." + this.SqlDbType + ", \"" + this.DotNetTypeString + "\", " + this.Is_Nullable.ToString().ToLower() + ", BinaryDigit.DataAccess.eColumnType.CLR, " + this.Assembly_ID + ", \"" + this.Assembly_Class + "\", \"" + this.Assembly_Qualified_Name + "\", " + this.Max_Length + ", " + this.Precision + ", " + this.Scale + ", " + this.Is_PrimaryKey.ToString().ToLower() + ", " + this.Is_Identity.ToString().ToLower() + ", " + this.Is_RowGuidCol.ToString().ToLower() + ", " + this.Is_Computed.ToString().ToLower() + ", " + this.Is_FileStream.ToString().ToLower() + ")]";
                    break;
            }

            return result;
        }

        public string GetAsFeildDeclaration()
        {
            string result = string.Empty;

            switch (this.ColumnType)
            {
                case eColumnType.Standard:
                    if (this.DotNetTypeString == "System.Byte[]")
                    {
                        result = "public " + this.DotNetTypeString + (this.Is_Nullable && this.DotNetType != null && this.DotNetType.IsValueType ? "?" : string.Empty) + " " + this.GetFieldName() + " { get{ return GetBlob(\"" + this.Name + "\"); } " + (this.ReadOnly ? "private" : string.Empty) + " set { SetBlob(\"" + this.Name + "\", value); } }";
                    }
                    else
                    {
                        result = "public " + this.DotNetTypeString + (this.Is_Nullable && this.DotNetType != null && this.DotNetType.IsValueType ? "?" : string.Empty) + " " + this.GetFieldName() + " { get; " + (this.ReadOnly ? "private" : string.Empty) + " set; }";
                    }
                    break;
                case eColumnType.CLR:
                    result = "public " + this.Assembly_Class + " " + this.GetFieldName() + " { get; " + (this.ReadOnly ? "private" : string.Empty) + " set; }";
                    break;
            }

            return result;
        }

        public string GetFieldName(bool firstLower = false, bool requiresUnderscore = false)
        {
            if (this.Name.Contains(" "))
            {
                return (firstLower ? (requiresUnderscore ? "?" : string.Empty) + this.Name[0].ToString().ToLower() : this.Name[0].ToString().ToUpper()) + this.Name.ToTitleCase().Replace(" ", "_").Remove(0, 1);
            }
            return (firstLower ? (requiresUnderscore ? "?" : string.Empty) + this.Name[0].ToString().ToLower() : this.Name[0].ToString().ToUpper()) + this.Name.Remove(0, 1);
        }

        #endregion
    }
}