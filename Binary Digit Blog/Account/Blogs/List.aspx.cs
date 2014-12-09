namespace Binary_Digit_Blog.Account.Blogs
{
    using System;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Binary_Digit_Blog.DAL.Data.Blogs;

    public partial class List : Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadData();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("Form?i=" + ((Button)sender).CommandArgument);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            var id = new Guid(((Button)sender).CommandArgument);
            var obj = new Blogs_Articles(id);
            if (obj != null)
            {
                obj.Delete();
            }

            //LoadData();
            this.Response.Redirect(this.Request.RawUrl);
        }

        private void LoadData()
        {
            this.gvData.DataSource = new Blogs_Articles().GetAll("Blogs_Articles.IsPublished, Blogs_Articles.PublishDate desc", "UserID = @UserID", new[] { new SqlParameter("@UserID", HttpContext.Current.User.Identity.GetUserId()) });
            this.gvData.DataBind();
        }

        #endregion
    }
}