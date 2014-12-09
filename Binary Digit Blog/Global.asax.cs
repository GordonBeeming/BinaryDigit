namespace Binary_Digit_Blog
{
    using System;
    using System.Data.SqlClient;
    using System.Web;

    using BinaryDigit;
    using BinaryDigit.DataAccess;

    public class Global : SystemGlobalAshx
    {
        #region Public Methods and Operators

        public override void BeginTheRequest(object sender, EventArgs e)
        {
            // add default code here to run on begin request
            if (HttpContext.Current.Request.Url.Segments.Length == 6 && HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), string.Empty).ToLower().StartsWith("/blogs/"))
            {
                int year = 00;
                int month = 00;
                int day = 00;
                string pageName = string.Empty;
                int.TryParse(HttpContext.Current.Request.Url.Segments[2].Trim('/'), out year);
                int.TryParse(HttpContext.Current.Request.Url.Segments[3].Trim('/'), out month);
                int.TryParse(HttpContext.Current.Request.Url.Segments[4].Trim('/'), out day);
                pageName = HttpContext.Current.Request.Url.Segments[5].Trim('/');

                Guid? articleGuid = null;
                if (year > 0)
                {
                    articleGuid = Sql.ExecuteScalar<Guid?>(@"
SELECT [ArticleGuid] 
FROM [dbo].[Blogs_Articles] 
WHERE YEAR([PublishDate]) = " + year + @" AND
        MONTH([PublishDate]) = " + month + @" AND
        DAY([PublishDate]) = " + day + @" AND
         [PublishDate] < getdate() AND 
        [PageName] = @PageName", new[] { new SqlParameter("PageName", pageName.Trim()) });
                }

                if (articleGuid.HasValue)
                {
                    HttpContext.Current.RewritePath("/Blogs/Year/Month/Day/BlogArticle.aspx?i=" + articleGuid);
                }
                else
                {
                    if (HttpContext.Current.Request.QueryString["i"].IsNullOrEmptyNot() && HttpContext.Current.Request.QueryString["draft"] == "1")
                    {
                        HttpContext.Current.RewritePath("/Blogs/Year/Month/Day/BlogArticle.aspx?i=" + HttpContext.Current.Request.QueryString["i"]);
                    }
                }
            }
            else if (HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), string.Empty).ToLower().StartsWith("/search/"))
            {
                string searchTerm = HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), string.Empty).ToLower().Replace("/search/", string.Empty).Split('/')[0];

                HttpContext.Current.RewritePath("/Search/SearchResults.aspx?q=" + HttpUtility.UrlEncode(searchTerm));
            }
            else if (HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), string.Empty).ToLower().StartsWith("/searchdate/"))
            {
                string searchTerm = HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), string.Empty).ToLower().Replace("/searchdate/", string.Empty).Split('/')[0];

                HttpContext.Current.RewritePath("/Search/SearchResults.aspx?q=" + HttpUtility.UrlEncode(searchTerm));
            }
            else if (HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), string.Empty).ToLower().StartsWith("/sitemap.xml"))
            {
                HttpContext.Current.RewritePath("/SitemapHandler.ashx");
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg == "TagCloudCache")
            {
                object o = HttpContext.Current.Application["TagCloudCacheGuid"];
                if (o == null)
                {
                    o = Guid.NewGuid();
                    HttpContext.Current.Application["TagCloudCacheGuid"] = o;
                }
                return o.ToString();
            }
            return base.GetVaryByCustomString(context, arg);
        }

        #endregion

        #region Methods

        private void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        private void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
        }

        #endregion
    }
}