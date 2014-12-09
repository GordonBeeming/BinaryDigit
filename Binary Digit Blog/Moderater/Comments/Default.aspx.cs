namespace Binary_Digit_Blog.Moderater.Comments
{
    using System;
    using System.Web.UI;

    public partial class Default : Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Redirect("http://b1n4ryd1g1t.disqus.com/admin/moderate/#/" + this.Request.QueryString["q"]);
        }

        #endregion
    }
}