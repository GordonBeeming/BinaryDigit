namespace Binary_Digit_Blog.Controls
{
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;

    using BinaryDigit.DataAccess;

    public partial class BlogPostPreviews : UserControl
    {
        #region Public Properties

        public int ItemCount { get; set; }

        public string SearchTerm
        {
            get
            {
                return this.Request.QueryString["q"];
            }
        }

        public bool Searching { get; set; }

        public bool SearchingDate { get; set; }

        #endregion

        #region Methods

        protected string GetLinkUrl()
        {
            var publishDate = (DateTime)this.Eval("PublishDate");
            return "/Blogs/" + publishDate.Year + "/" + publishDate.Month + "/" + publishDate.Day + "/" + this.Eval("PageName");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Searching)
            {
                this.LoadSearchItems();
            }
            else
            {
                this.LoadBlogItems();
            }
        }

        private void LoadBlogItems()
        {
            this.rptBlogs.DataSource = Sql.FetchDataTable(@"
SELECT " + (this.ItemCount > 0 ? "TOP " + this.ItemCount : string.Empty) + @" [ArticleGuid]
      ,[PageTitle]
      ,[PageName]
      ,[AutoBlurb]
      ,[PublishDate]
FROM [dbo].[Blogs_Articles]
WHERE [IsPublished] = 1 AND [RecordInfo].IsDeleted = 0 AND [PublishDate] < getdate()
ORDER BY [PublishDate] DESC");
            this.rptBlogs.DataBind();
        }

        private void LoadSearchItems()
        {
            this.Page.Title = "Search - " + this.SearchTerm;
            this.Page.MetaKeywords = "Search," + this.SearchTerm;
            this.Page.MetaDescription = "Searching for" + this.SearchTerm;

            this.rptBlogs.DataSource = Sql.FetchDataTable(@"
SELECT *
FROM [dbo].[Blogs_Articles] as ba
WHERE ba.[IsPublished] = 1 AND ba.[RecordInfo].IsDeleted = 0 AND ba.[PublishDate] < getdate() AND
ba.ArticleID in 
(
  SELECT DISTINCT baInner.ArticleID
  FROM Blogs_Articles as baInner
  LEFT OUTER JOIN Blogs_Keywords ON Blogs_Keywords.ArticleID = baInner.ArticleID
  LEFT OUTER JOIN Blogs_Technology ON Blogs_Technology.ArticleID = baInner.ArticleID
  WHERE (
" + (this.SearchingDate ? @"
          LTRIM(RTRIM((DATENAME(m,[PublishDate]) + ' ' +CAST(YEAR([PublishDate]) AS VARCHAR)))) like '%' + @searchTerm + '%' 
          " : @"
          LTRIM(RTRIM(Blogs_Keywords.KeywordDescription)) like '%' + @searchTerm + '%' 
          OR
          LTRIM(RTRIM(Blogs_Technology.TechnologyDescription)) like '%' + @searchTerm + '%'
          OR
          LTRIM(RTRIM(baInner.PageTitle)) like '%' + @searchTerm + '%'
          OR
          LTRIM(RTRIM(baInner.PageName)) like '%' + @searchTerm + '%'") + @"
        ) AND 
        baInner.[RecordInfo].IsDeleted = 0
)
ORDER BY [PublishDate] DESC", new[] { new SqlParameter("searchTerm", this.SearchTerm) });
            this.rptBlogs.DataBind();
        }

        #endregion
    }
}