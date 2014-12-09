namespace Binary_Digit_Blog.DAL.Data.Blogs
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using BinaryDigit.DataAccess;
    using BinaryDigit.DataAccess.Base;

    public sealed class Blogs_Technology : BD_DataObjectBase<Blogs_Technology>
    {
        #region Constructors and Destructors

        public Blogs_Technology()
        {
        }

        public Blogs_Technology(string whereClause, SqlParameter[] sqlParameters)
            : base(whereClause, sqlParameters)
        {
        }

        public Blogs_Technology(DataTable input)
            : base(input)
        {
        }

        public Blogs_Technology(SqlDataReader reader)
            : base(reader)
        {
        }

        public Blogs_Technology(Int32 technologyID)
        {
            this.ChangeTo("[TechnologyID] = @TechnologyID", new[]
                                                            {
                                                                new SqlParameter("@TechnologyID", technologyID)
                                                            });
        }

        #endregion

        #region Public Properties

        [BD_ColumnDefinition(2, "ArticleID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, false, false, false, false, false)]
        public Int32 ArticleID { get; set; }

        [BD_ColumnDefinition(13, "RecordInfo", "BD_TimeStamp", SqlDbType.Udt, "BD_TimeStamp", false, eColumnType.CLR, 65544, "BD_TimeStamp", "BD_TimeStamp, BinaryDigit.SQL, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", 51, 0, 0, false, false, false, false, false)]
        public BD_TimeStamp RecordInfo { get; set; }

        public override string TableName
        {
            get
            {
                return "Blogs_Technology";
            }
        }

        [BD_ColumnDefinition(3, "TechnologyDescription", "varchar", SqlDbType.VarChar, "System.String", false, eColumnType.Standard, 50, 0, 0, false, false, false, false, false)]
        public String TechnologyDescription { get; set; }

        [BD_ColumnDefinition(1, "TechnologyID", "int", SqlDbType.Int, "System.Int32", false, eColumnType.Standard, 4, 10, 0, true, true, false, false, false)]
        public Int32 TechnologyID { get; private set; }

        #endregion
    }
}