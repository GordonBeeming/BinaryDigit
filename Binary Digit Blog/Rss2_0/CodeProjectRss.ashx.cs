namespace Binary_Digit_Blog.Rss2_0
{
    using System;
    using System.Data;
    using System.Web;

    using BinaryDigit.DataAccess;

    /// <summary>
    ///     Summary description for CodeProjectRss
    /// </summary>
    public class CodeProjectRss : IHttpHandler
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
            context.Response.ContentType = "text/plain";

            context.Response.Write(@"<?xml version='1.0' encoding='utf-8'?>
            <rss xmlns:a10='http://www.w3.org/2005/Atom' version='2.0'>
              <channel>
                <title>Binary Digit Blog</title>
                <link>http://binarydigit.co.za/</link>
                <description>A place for me to let go and share my knowledge and experiences</description>
                <language>EN-US</language>
                <copyright>Copyright " + DateTime.Now.Year + @"</copyright>
                <lastBuildDate>" + DateTime.Now.ToString("o") + @"</lastBuildDate>
                <category domain='categoryScheme'>CodeProject</category>
                <image>
                  <url>http://binarydigit.co.za/images/socialMedia/siteThumb.png</url>
                  <title>Binary Digit Blog</title>
                  <link>http://binarydigit.co.za/</link>
                </image>
                <a10:id>1</a10:id>");

            foreach (DataRow dr in Sql.FetchDataTable(@"
SELECT [ArticleGuid]
      ,[PageTitle]
      ,[PageName]
      ,[AutoBlurb]
      ,[PublishDate]
,[RecordInfo].UpdateDateTime as UpdateDateTime
FROM [dbo].[Blogs_Articles]
WHERE [IsPublished] = 1 AND [RecordInfo].IsDeleted = 0 AND [PublishDate] < getdate()
ORDER BY [PublishDate] DESC").Rows)
            {
                context.Response.Write(@"<item>
                  <guid isPermaLink='true'>" + dr["ArticleGuid"] + @"</guid>
                  <link>" + "http://binarydigit.co.za" + this.GetLinkUrl(dr["PublishDate"], dr["PageName"]) + @"</link>
                  <author>gordon@beeming.co.za</author>
                  <category domain='categoryScheme'>CodeProject</category>
                  <title>" + dr["PageTitle"] + @"</title>
                  <description>" + dr["AutoBlurb"] + @"</description>
                  <pubDate>" + ((DateTime)dr["PublishDate"]).ToString("o") + @"</pubDate>
                  <a10:updated>" + ((DateTime)dr["UpdateDateTime"]).ToString("o") + @"</a10:updated>
                  <a10:rights type='text'>Copyright " + DateTime.Now.Year + @"</a10:rights>
                  <a10:content type='text'>" + dr["AutoBlurb"] + @"</a10:content>
                  <a10:contributor>
                    <a10:name>Gordon Beeming</a10:name>
                    <a10:uri>http://binarydigit.co.za/About</a10:uri>
                    <a10:email>gordon@beeming.co.za</a10:email>
                  </a10:contributor>
                </item>");
            }
            context.Response.Write(@"</channel>
            </rss>");
        }

        #endregion

        #region Methods

        protected string GetLinkUrl(object date, object pageName)
        {
            var publishDate = (DateTime)date;
            return "/Blogs/" + publishDate.Year + "/" + publishDate.Month + "/" + publishDate.Day + "/" + pageName;
        }

        #endregion
    }
}