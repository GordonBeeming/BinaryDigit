namespace Binary_Digit_Blog.Controls
{
    using System;
    using System.Web;
    using System.Web.UI;

    public partial class SearchBox : UserControl
    {
        #region Properties

        protected string SearchTerm
        {
            get
            {
                return this.Request.QueryString["q"];
            }
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtSearchTerm.Text = this.SearchTerm;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Search/" + HttpUtility.UrlEncode(this.txtSearchTerm.Text.Trim()).Replace("+", " "));
        }

        #endregion
    }
}