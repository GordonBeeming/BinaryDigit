namespace Binary_Digit_Blog.Search
{
    using System;
    using System.Web.UI;

    public partial class SearchResults : Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BlogPostPreviews.SearchingDate = this.Request.RawUrl.ToLower().Contains("/searchdate/");
        }

        #endregion
    }
}