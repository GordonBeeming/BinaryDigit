namespace Binary_Digit_Blog.DAL.Data.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Web;

    using BinaryDigit.DataAccess;

    public static class BlogsExtended
    {
        #region Public Methods and Operators

        public static Guid SaveBlogEntry(Guid articleGuid, string title, string articleBlurb, string articleContent, DateTime? publishDate, Guid userId, string keywords, string technologies)
        {
            var obj = new Blogs_Articles(articleGuid);
            obj.ArticleContent = articleContent;
            //obj.AutoBlurb = StripTagsCharArray(articleBlurb).Trim(997, true);
            obj.AutoBlurb = articleBlurb.Trim(997, true);
            obj.PageName = string.Empty;
            foreach (char ch in title)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    obj.PageName = obj.PageName + ch;
                }
                else if (ch == ' ')
                {
                    obj.PageName += "-";
                }
            }
            obj.PageName = obj.PageName.Trim();
            obj.PageTitle = title.Trim();
            obj.PublishDate = publishDate;
            obj.UserId = userId;

            obj.Save();

            AddMetaDataToArticle(obj, keywords, "Blogs_Keywords", "KeywordDescription");
            AddMetaDataToArticle(obj, technologies, "Blogs_Technology", "TechnologyDescription");

            HttpContext.Current.Application["TagCloudCacheGuid"] = Guid.NewGuid();

            return obj.ArticleGuid;
        }

        #endregion

        #region Methods

        private static void AddMetaDataToArticle(Blogs_Articles obj, string inputData, string tableName, string metaColumnName)
        {
            string keywordsQuery = "DELETE FROM " + tableName + @" WHERE [ArticleID] = " + obj.ArticleID + Environment.NewLine;
            var sqlParameters = new List<SqlParameter>();
            foreach (string keyword in inputData.Trim().Split(','))
            {
                if (keyword.Trim().Length > 0)
                {
                    sqlParameters.Add(new SqlParameter("@k" + sqlParameters.Count, keyword));
                    keywordsQuery += @" INSERT INTO [dbo].[" + tableName + @"]
             ([ArticleID]
             ,[" + metaColumnName + @"])
       VALUES
             (" + obj.ArticleID + @" ,@k" + (sqlParameters.Count - 1) + @")" + Environment.NewLine;
                }
            }

            Sql.ExecuteNonQuery(keywordsQuery, sqlParameters.ToArray(), success: context =>
            {
                if (context.CurrentSqlTransaction != null)
                {
                    context.CurrentSqlTransaction.Commit();
                }
            }, failed: context =>
            {
                if (context.CurrentSqlTransaction != null)
                {
                    context.CurrentSqlTransaction.Rollback();
                }
                return false;
            }, startNewTransaction: true);
        }

        /// <summary>
        ///     Remove HTML tags from string using char array.
        /// </summary>
        private static string StripTagsCharArray(string source)
        {
            var array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        #endregion
    }
}