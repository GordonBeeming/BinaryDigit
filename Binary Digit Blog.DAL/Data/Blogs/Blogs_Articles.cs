namespace Binary_Digit_Blog.DAL.Data.Blogs
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using BinaryDigit.DataAccess;
    using BinaryDigit.DataAccess.Base;

    public sealed class Blogs_Articles : BD_DataObjectBase<Blogs_Articles>
    {
        #region Constructors and Destructors

        public Blogs_Articles()
        {
        }

        public Blogs_Articles(string whereClause, SqlParameter[] sqlParameters)
            : base(whereClause, sqlParameters)
        {
        }

        public Blogs_Articles(DataTable input)
            : base(input)
        {
        }

        public Blogs_Articles(SqlDataReader reader)
            : base(reader)
        {
        }

        public Blogs_Articles(Int32 articleID)
        {
            this.ChangeTo("[ArticleID] = @ArticleID", new[]
                                                      {
                                                          new SqlParameter("@ArticleID", articleID)
                                                      });
        }

        public Blogs_Articles(Guid articleGuid)
        {
            this.ChangeTo("[ArticleGuid] = @ArticleGuid", new[]
                                                          {
                                                              new SqlParameter("@ArticleGuid", articleGuid)
                                                          });
        }

        #endregion

        #region Public Properties

        [BD_ColumnDefinition(7, "ArticleContent", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, -1, 0, 0, false, false, false, false, false)]
        public String ArticleContent { get; set; }

        [BD_ColumnDefinition(2, "ArticleGuid", "uniqueidentifier", SqlDbType.UniqueIdentifier, "System.Guid", false, eColumnType.Standard, 16, 0, 0, false, false, true, false, false)]
        public Guid ArticleGuid { get; private set; }

        [BD_ColumnDefinition(1, "ArticleID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, true, true, false, false, false)]
        public Int32 ArticleID { get; private set; }

        [BD_ColumnDefinition(6, "AutoBlurb", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, 1000, 0, 0, false, false, false, false, false)]
        public String AutoBlurb { get; set; }

        [BD_ColumnDefinition(9, "IsPublished", "bit", SqlDbType.Bit, "System.Boolean", true, eColumnType.Standard, 1, 1, 0, false, false, false, true, false)]
        public Boolean? IsPublished { get; private set; }

        [BD_ColumnDefinition(5, "PageName", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, 255, 0, 0, false, false, false, false, false)]
        public String PageName { get; set; }

        [BD_ColumnDefinition(4, "PageTitle", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, 255, 0, 0, false, false, false, false, false)]
        public String PageTitle { get; set; }

        [BD_ColumnDefinition(10, "PublishDate", "datetime", SqlDbType.DateTime, "System.DateTime", true, eColumnType.Standard, 8, 23, 3, false, false, false, false, false)]
        public DateTime? PublishDate { get; set; }

        [BD_ColumnDefinition(8, "RecordInfo", "BD_TimeStamp", SqlDbType.Udt, "BD_TimeStamp", false, eColumnType.CLR, 65544, "BD_TimeStamp", "BD_TimeStamp, BinaryDigit.SQL, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", 51, 0, 0, false, false, false, false, false)]
        public BD_TimeStamp RecordInfo { get; set; }

        public override string TableName
        {
            get
            {
                return "Blogs_Articles";
            }
        }

        [BD_ColumnDefinition(3, "UserId", "uniqueidentifier", SqlDbType.UniqueIdentifier, "System.Guid", false, eColumnType.Standard, 16, 0, 0, false, false, false, false, false)]
        public Guid UserId { get; set; }

        #endregion
    }
}