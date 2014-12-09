namespace Binary_Digit_Blog.Account.Blogs
{
    using System;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;

    using Binary_Digit_Blog.DAL.Data.Blogs;

    public partial class Form : Page
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
            if (!this.IsPostBack)
            {
                this.LoadBlogArticle();
            }
        }

        protected void btnGetFromContent_Click(object sender, EventArgs e)
        {
            this.txtBlub.Text = HttpUtility.HtmlDecode(Regex.Replace(this.txtArticleContent.Text, @"<!--[\S\s]*?-->|<(?:"".*?""|'.*?'|[\S\s])*?>", "")).Trim().Replace("\n", ". ").Replace("\t", " ").Replace("\r", string.Empty);
            while (this.txtBlub.Text.Contains(". ."))
            {
                this.txtBlub.Text = this.txtBlub.Text.Replace(". .", ".");
            }
            while (this.txtBlub.Text.Contains("  "))
            {
                this.txtBlub.Text = this.txtBlub.Text.Replace("  ", " ");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime? publishDate = null;
            if (!string.IsNullOrEmpty(this.txtPublishDate.Text))
            {
                publishDate = DateTime.ParseExact(this.txtPublishDate.Text, "d MMMM yyyy", CultureInfo.InvariantCulture);

                publishDate = publishDate.Value.AddHours(this.txtDateHour.Text.ToInt32());
                publishDate = publishDate.Value.AddMinutes(this.txtDateMinute.Text.ToInt32());
            }

            BlogsExtended.SaveBlogEntry(this.ArticleGuid, this.txtTitle.Text, this.txtBlub.Text, this.txtArticleContent.Text, publishDate, HttpContext.Current.User.Identity.GetUserId(), this.txtKeywords.Text, this.txtTechnologies.Text);
            this.Response.Redirect("List");
        }

        private void LoadBlogArticle()
        {
            var obj = new Blogs_Articles(this.ArticleGuid);
            if (obj != null)
            {
                this.txtArticleContent.Text = obj.ArticleContent;
                this.txtTitle.Text = obj.PageTitle;
                this.txtBlub.Text = obj.AutoBlurb;
                if (obj.PublishDate.HasValue)
                {
                    this.txtPublishDate.Text = obj.PublishDate.Value.ToString("d MMMM yyyy");
                    this.txtDateHour.Text = obj.PublishDate.Value.Hour.ToString();
                    this.txtDateMinute.Text = obj.PublishDate.Value.Minute.ToString();
                }
                else
                {
                    this.txtPublishDate.Text = string.Empty;
                }
                this.txtKeywords.Text = string.Empty;
                foreach (Blogs_Keywords keyword in new Blogs_Keywords().GetAll(null, "ArticleID = @ArticleID", new[] { new SqlParameter("@ArticleID", obj.ArticleID) }))
                {
                    if (!string.IsNullOrEmpty(this.txtKeywords.Text))
                    {
                        this.txtKeywords.Text += ",";
                    }
                    this.txtKeywords.Text += keyword.KeywordDescription;
                }
                this.txtTechnologies.Text = string.Empty;
                foreach (Blogs_Technology technology in new Blogs_Technology().GetAll(null, "ArticleID = @ArticleID", new[] { new SqlParameter("@ArticleID", obj.ArticleID) }))
                {
                    if (!string.IsNullOrEmpty(this.txtTechnologies.Text))
                    {
                        this.txtTechnologies.Text += ",";
                    }
                    this.txtTechnologies.Text += technology.TechnologyDescription;
                }
                this.lnkViewOnSite.NavigateUrl = "/Blogs/0000/00/00/" + obj.PageName + "?i=" + obj.ArticleGuid + "&draft=1";
                this.lnkViewOnSite.Visible = true;
            }
        }

        #endregion
    }
}