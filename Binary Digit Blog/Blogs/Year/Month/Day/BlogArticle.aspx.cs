namespace Binary_Digit_Blog.Blogs.Year.Month.Day
{
    using System;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.UI;

    using Binary_Digit_Blog.DAL.Data.Blogs;

    public partial class BlogArticle : Page
    {
        #region Properties

        protected Guid ArticleGuid
        {
            get
            {
                Guid result = Guid.Empty;
                if (Guid.TryParse(this.Request.QueryString["i"], out result))
                {
                    return result;
                }
                return Guid.Empty;
            }
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.ArticleGuid == Guid.Empty)
            {
                this.Response.RedirectPermanent("/Blog");
            }
            else
            {
                if (!this.IsPostBack)
                {
                    this.LoadBlogArticle();
                }
            }
        }

        private void LoadBlogArticle()
        {
            var obj = new Blogs_Articles(this.ArticleGuid);
            if (obj != null)
            {
                this.litTitle.Text = HttpUtility.HtmlEncode(obj.PageTitle);
                this.litContent.Text = obj.ArticleContent;
                if (obj.PublishDate.HasValue)
                {
                    this.litDateOfPublish.Text = obj.PublishDate.Value.ToString("dd MMMM yyyy");
                }
                else
                {
                    this.litDateOfPublish.Text = string.Empty;
                }
                if (this.Page.MetaKeywords == null)
                {
                    this.Page.MetaKeywords = string.Empty;
                }
                string tags = string.Empty;
                foreach (Blogs_Keywords item in new Blogs_Keywords().GetAll(null, "ArticleID = @ArticleID", new[] { new SqlParameter("@ArticleID", obj.ArticleID) }))
                {
                    this.Page.MetaKeywords += item.KeywordDescription + ",";
                    tags += String.Format("<a href='/Search/{1}'>{0}</a> ,", item.KeywordDescription, HttpUtility.UrlEncode(item.KeywordDescription).Replace("+", " "));
                }
                foreach (Blogs_Technology item in new Blogs_Technology().GetAll(null, "ArticleID = @ArticleID", new[] { new SqlParameter("@ArticleID", obj.ArticleID) }))
                {
                    this.Page.MetaKeywords += item.TechnologyDescription + ",";
                }
                this.Page.MetaKeywords = this.Page.MetaKeywords.Trim(',');
                this.Page.MetaDescription = obj.AutoBlurb;
                this.Page.Title = obj.PageTitle;
                if (!string.IsNullOrEmpty(tags))
                {
                    this.litTags.Text = "Tags : " + tags.Trim(',').Trim();
                }
            }
        }

        #endregion
    }
}