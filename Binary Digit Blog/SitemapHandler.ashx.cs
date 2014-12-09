namespace Binary_Digit_Blog
{
    using System;
    using System.Data;
    using System.Web;

    using BinaryDigit.DataAccess;

    /// <summary>
    ///     Summary description for SitemapHandler
    /// </summary>
    public class SitemapHandler : IHttpHandler
    {
        #region Public Properties

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void ProcessRequest(HttpContext context)
        {
            string siteRoot = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            context.Response.ContentType = "text/xml";
            context.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            context.Response.Write("   <urlset");
            context.Response.Write("         xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            context.Response.Write("      xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            context.Response.Write("      xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9");
            context.Response.Write("            http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");
            context.Response.Write("");
            context.Response.Write("<url>");
            context.Response.Write("  <loc> " + siteRoot + "/</loc>");
            context.Response.Write("  <changefreq> daily </changefreq>");
            context.Response.Write("  <priority> 1.00 </priority>");
            context.Response.Write("</url>");
            context.Response.Write("<url>");
            context.Response.Write("  <loc> " + siteRoot + "/Blog</loc>");
            context.Response.Write("  <changefreq> daily </changefreq>");
            context.Response.Write("  <priority> 1 </priority>");
            context.Response.Write("</url>");

            //apps
            context.Response.Write("<url>");
            context.Response.Write("  <loc> " + siteRoot + "/Apps</loc>");
            context.Response.Write("  <changefreq> daily </changefreq>");
            context.Response.Write("  <priority> 0.80 </priority>");
            context.Response.Write("</url>");
            context.Response.Write("<url>");
            context.Response.Write("  <loc> " + siteRoot + "/Apps/Sudoku-Problem-Solver/Default</loc>");
            context.Response.Write("  <changefreq> monthly </changefreq>");
            context.Response.Write("  <priority> 0.80 </priority>");
            context.Response.Write("</url>");

            context.Response.Write("<url>");
            context.Response.Write("  <loc> " + siteRoot + "/About</loc>");
            context.Response.Write("  <changefreq> monthly </changefreq>");
            context.Response.Write("  <priority> 0.80 </priority>");
            context.Response.Write("</url>");
            context.Response.Write("<url>");
            context.Response.Write("  <loc> " + siteRoot + "/Contact</loc>");
            context.Response.Write("  <changefreq> monthly </changefreq>");
            context.Response.Write("  <priority> 0.80 </priority>");
            context.Response.Write("</url>");

            //blogs
            foreach (DataRow dr in Sql.FetchDataTable(@"
SELECT [ArticleGuid]
      ,[PageTitle]
      ,[PageName]
      ,[AutoBlurb]
      ,[PublishDate]
      ,[RecordInfo].UpdateDateTime as UpdateDateTime
FROM [dbo].[Blogs_Articles]
WHERE [IsPublished]=1 AND [RecordInfo].IsDeleted=0 AND [PublishDate] <getdate()
ORDER BY [PublishDate] DESC").Rows)
            {
                context.Response.Write("<url>");
                context.Response.Write("  <loc> " + siteRoot + this.GetLinkUrl(dr) + "</loc>");
                context.Response.Write("  <changefreq> monthly </changefreq>");
                context.Response.Write("  <lastmod> " + ((DateTime)dr["UpdateDateTime"]).ToString("dd MMMM yyyy HH:mm:ss") + @" </lastmod>");
                context.Response.Write("  <priority> 0.90 </priority>");
                context.Response.Write("</url>");
            }

            //keywords - tags
            foreach (DataRow dr in Sql.FetchDataTable(@"
SELECT [Description]
      , (
          SELECT TOP 1 COUNT(*) as Total
          FROM
		  (
				SELECT Blogs_Articles.ArticleID, LTRIM(RTRIM(Blogs_Technology.TechnologyDescription)) as [Description]
				FROM Blogs_Articles
			  INNER JOIN Blogs_Technology ON Blogs_Technology.ArticleID=Blogs_Articles.ArticleID
			  WHERE Blogs_Articles.[IsPublished]=1 AND Blogs_Articles.[PublishDate] <getdate() AND Blogs_Articles.RecordInfo.IsDeleted=0
			  UNION
				SELECT Blogs_Articles.ArticleID, LTRIM(RTRIM(Blogs_Keywords.KeywordDescription)) as [Description]
				FROM Blogs_Articles
			  INNER JOIN Blogs_Keywords ON Blogs_Keywords.ArticleID=Blogs_Articles.ArticleID
			  WHERE Blogs_Articles.[IsPublished]=1 AND Blogs_Articles.[PublishDate] <getdate() AND Blogs_Articles.RecordInfo.IsDeleted=0
		  ) as data
          WHERE (data.[Description]=Tags.[Description] OR
                data.[Description]=Tags.[Description]) 
          GROUP BY data.[Description]
        ) as Total
FROM Tags
ORDER BY newid()").Rows)
            {
                string tagText = dr[0].ToStringSafe();
                if (tagText.IsNullOrEmptyNot() && dr[1].IsNullOrEmptyNot())
                {
                    context.Response.Write("<url>");
                    context.Response.Write("  <loc> " + siteRoot + String.Format("/Search/{0}", HttpUtility.UrlEncode(tagText).Replace("+", " ")) + "</loc>");
                    context.Response.Write("  <changefreq> daily </changefreq>");
                    context.Response.Write("  <priority> 0.80 </priority>");
                    context.Response.Write("</url>");
                }
            }

            context.Response.Write("<url>");
            context.Response.Write("  <loc> " + siteRoot + "/Disclaimer</loc>");
            context.Response.Write("  <changefreq> monthly </changefreq>");
            context.Response.Write("  <priority> 0.80 </priority>");
            context.Response.Write("</url>");

            context.Response.Write("</urlset>");
        }

        #endregion

        #region Methods

        protected string GetLinkUrl(DataRow dr)
        {
            var publishDate = (DateTime)dr["PublishDate"];
            return "/Blogs/" + publishDate.Year + "/" + publishDate.Month + "/" + publishDate.Day + "/" + dr["PageName"];
        }

        #endregion
    }
}