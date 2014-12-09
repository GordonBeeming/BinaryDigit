namespace BinaryDigit.IO
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using BinaryDigit.DataAccess;
    using BinaryDigit.DataAccess.Base;

    public sealed class SmartFile : BD_DataObjectBase<SmartFile>
    {
        #region Constructors and Destructors

        public SmartFile()
        {
        }

        public SmartFile(string whereClause, SqlParameter[] sqlParameters)
            : base(whereClause, sqlParameters)
        {
        }

        public SmartFile(DataTable input)
            : base(input)
        {
        }

        public SmartFile(SqlDataReader reader)
            : base(reader)
        {
        }

        public SmartFile(Int32 smartFileID)
        {
            this.ChangeTo("[SmartFileID] = @SmartFileID", new[]
                                                          {
                                                              new SqlParameter("@SmartFileID", smartFileID)
                                                          });
        }

        public SmartFile(Guid smartGuid)
        {
            this.ChangeTo("[SmartGuid] = @SmartGuid", new[]
                                                      {
                                                          new SqlParameter("@SmartGuid", smartGuid)
                                                      });
        }

        #endregion

        #region Public Properties

        [BD_ColumnDefinition(5, "CalcFileRawUrl", "varchar", SqlDbType.VarChar, "System.String", true, eColumnType.Standard, 255, 0, 0, false, false, false, true, false)]
        public String CalcFileRawUrl { get; private set; }

        [BD_ColumnDefinition(3, "FileBytes", "varbinary", SqlDbType.VarBinary, "System.Byte[]", true, eColumnType.Standard, -1, 0, 0, false, false, false, false, false)]
        public Byte[] FileBytes
        {
            get
            {
                return this.GetBlob("FileBytes");
            }
            set
            {
                this.SetBlob("FileBytes", value);
            }
        }

        public int FileBytesLength
        {
            get
            {
                return this.GetBlobSize("FileBytes");
            }
            set
            {
                this.SetBlobSize("FileBytes", value);
            }
        }

        [BD_ColumnDefinition(6, "FileName", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, 255, 0, 0, false, false, false, false, false)]
        public String FileName { get; set; }

        [BD_ColumnDefinition(4, "FileRawUrl", "varchar", SqlDbType.VarChar, "System.String", true, eColumnType.Standard, 255, 0, 0, false, false, false, false, false)]
        public String FileRawUrl { get; set; }

        [BD_ColumnDefinition(7, "MimeType", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, 255, 0, 0, false, false, false, false, false)]
        public String MimeType { get; set; }

        [BD_ColumnDefinition(15, "RecordInfo", "BD_TimeStamp", SqlDbType.Udt, "BD_TimeStamp", false, eColumnType.CLR, 65540, "BD_TimeStamp", "BD_TimeStamp, BinaryDigit.SQL, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", 51, 0, 0, false, false, false, false, false)]
        public BD_TimeStamp RecordInfo { get; set; }

        [BD_ColumnDefinition(1, "SmartFileID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, true, true, false, false, false)]
        public Int32 SmartFileID { get; private set; }

        [BD_ColumnDefinition(2, "SmartGuid", "uniqueidentifier", SqlDbType.UniqueIdentifier, "System.Guid", false, eColumnType.Standard, 16, 0, 0, false, false, true, false, false)]
        public Guid SmartGuid { get; private set; }

        public override string TableName
        {
            get
            {
                return "SmartFiles";
            }
        }

        #endregion
    }
}