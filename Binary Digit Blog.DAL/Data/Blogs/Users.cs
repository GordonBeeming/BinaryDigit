namespace Binary_Digit_Blog.DAL.Data.Blogs
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using BinaryDigit.DataAccess;
    using BinaryDigit.DataAccess.Base;

    public sealed class User : BD_DataObjectBase<User>
    {
        #region Constructors and Destructors

        public User()
        {
        }

        public User(string whereClause, SqlParameter[] sqlParameters)
            : base(whereClause, sqlParameters)
        {
        }

        public User(DataTable input)
            : base(input)
        {
        }

        public User(SqlDataReader reader)
            : base(reader)
        {
        }

        public User(Guid userId)
        {
            this.ChangeTo("[UserId] = @UserId", new[]
                                                {
                                                    new SqlParameter("@UserId", userId)
                                                });
        }

        #endregion

        #region Public Properties

        [BD_ColumnDefinition(1, "ApplicationId", "uniqueidentifier", SqlDbType.UniqueIdentifier, "System.Guid", false, eColumnType.Standard, 16, 0, 0, false, false, false, false, false)]
        public Guid ApplicationId { get; set; }

        [BD_ColumnDefinition(4, "IsAnonymous", "bit", SqlDbType.Bit, "System.Boolean", false, eColumnType.Standard, 1, 1, 0, false, false, false, false, false)]
        public Boolean IsAnonymous { get; set; }

        [BD_ColumnDefinition(5, "LastActivityDate", "datetime", SqlDbType.Date, "System.DateTime", false, eColumnType.Standard, 8, 23, 3, false, false, false, false, false)]
        public DateTime LastActivityDate { get; set; }

        public override string TableName
        {
            get
            {
                return "Users";
            }
        }

        [BD_ColumnDefinition(2, "UserId", "uniqueidentifier", SqlDbType.UniqueIdentifier, "System.Guid", false, eColumnType.Standard, 16, 0, 0, true, false, false, false, false)]
        public Guid UserId { get; set; }

        [BD_ColumnDefinition(3, "UserName", "nvarchar", SqlDbType.NVarChar, "System.String", false, eColumnType.Standard, 100, 0, 0, false, false, false, false, false)]
        public String UserName { get; set; }

        #endregion
    }
}