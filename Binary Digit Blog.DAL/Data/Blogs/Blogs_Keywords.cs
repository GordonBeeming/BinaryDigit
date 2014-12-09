namespace Binary_Digit_Blog.DAL.Data.Blogs
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using BinaryDigit.DataAccess;
    using BinaryDigit.DataAccess.Base;

    public sealed class Blogs_Keywords : BD_DataObjectBase<Blogs_Keywords>
    {
        #region Constructors and Destructors

        public Blogs_Keywords()
        {
        }

        public Blogs_Keywords(string whereClause, SqlParameter[] sqlParameters)
            : base(whereClause, sqlParameters)
        {
        }

        public Blogs_Keywords(DataTable input)
            : base(input)
        {
        }

        public Blogs_Keywords(SqlDataReader reader)
            : base(reader)
        {
        }

        public Blogs_Keywords(Int32 keywordID)
        {
            this.ChangeTo("[KeywordID] = @KeywordID", new[]
                                                      {
                                                          new SqlParameter("@KeywordID", keywordID)
                                                      });
        }

        #endregion

        #region Public Properties

        [BD_ColumnDefinition(2, "ArticleID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, false, false, false, false, false)]
        public Int32 ArticleID { get; set; }

        [BD_ColumnDefinition(3, "KeywordDescription", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, 50, 0, 0, false, false, false, false, false)]
        public String KeywordDescription { get; set; }

        [BD_ColumnDefinition(1, "KeywordID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, true, true, false, false, false)]
        public Int32 KeywordID { get; private set; }

        [BD_ColumnDefinition(15, "RecordInfo", "BD_TimeStamp", SqlDbType.Udt, "BD_TimeStamp", false, eColumnType.CLR, 65544, "BD_TimeStamp", "BD_TimeStamp, BinaryDigit.SQL, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", 51, 0, 0, false, false, false, false, false)]
        public BD_TimeStamp RecordInfo { get; set; }

        public override string TableName
        {
            get
            {
                return "Blogs_Keywords";
            }
        }

        #endregion
    }
}