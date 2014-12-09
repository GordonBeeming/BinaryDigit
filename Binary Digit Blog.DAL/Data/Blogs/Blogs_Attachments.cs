namespace Binary_Digit_Blog.DAL.Data.Blogs
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using BinaryDigit.DataAccess;
    using BinaryDigit.DataAccess.Base;

    public sealed class Blogs_Attachments : BD_DataObjectBase<Blogs_Attachments>
    {
        #region Constructors and Destructors

        public Blogs_Attachments()
        {
        }

        public Blogs_Attachments(string whereClause, SqlParameter[] sqlParameters)
            : base(whereClause, sqlParameters)
        {
        }

        public Blogs_Attachments(DataTable input)
            : base(input)
        {
        }

        public Blogs_Attachments(SqlDataReader reader)
            : base(reader)
        {
        }

        public Blogs_Attachments(Int32 attachmentID)
        {
            this.ChangeTo("[AttachmentID] = @AttachmentID", new[]
                                                            {
                                                                new SqlParameter("@AttachmentID", attachmentID)
                                                            });
        }

        public Blogs_Attachments(Guid attachmentGuid)
        {
            this.ChangeTo("[AttachmentGuid] = @AttachmentGuid", new[]
                                                                {
                                                                    new SqlParameter("@AttachmentGuid", attachmentGuid)
                                                                });
        }

        #endregion

        #region Public Properties

        [BD_ColumnDefinition(4, "ArticleID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, false, false, false, false, false)]
        public Int32 ArticleID { get; set; }

        [BD_ColumnDefinition(2, "AttachmentGuid", "uniqueidentifier", SqlDbType.UniqueIdentifier, "System.Guid", false, eColumnType.Standard, 16, 0, 0, false, false, true, false, false)]
        public Guid AttachmentGuid { get; private set; }

        [BD_ColumnDefinition(1, "AttachmentID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, true, true, false, false, false)]
        public Int32 AttachmentID { get; private set; }

        [BD_ColumnDefinition(14, "RecordInfo", "BD_TimeStamp", SqlDbType.Udt, "BD_TimeStamp", false, eColumnType.CLR, 65544, "BD_TimeStamp", "BD_TimeStamp, BinaryDigit.SQL, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", 51, 0, 0, false, false, false, false, false)]
        public BD_TimeStamp RecordInfo { get; set; }

        [BD_ColumnDefinition(3, "SmartFileID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, false, false, false, false, false)]
        public Int32 SmartFileID { get; set; }

        public override string TableName
        {
            get
            {
                return "Blogs_Attachments";
            }
        }

        #endregion
    }
}