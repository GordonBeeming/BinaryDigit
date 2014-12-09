namespace Binary_Digit_Blog.Controls
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;

    using BinaryDigit.DataAccess;

    public partial class TagCloud : UserControl
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadTagCloud();
            }
        }

        private string GetCssClass(int tagCount, int postCount)
        {
            int result = (tagCount * 100) / postCount;
            if (result <= 20)
            {
                return "TagSize1";
            }
            if (result <= 40)
            {
                return "TagSize2";
            }
            if (result <= 60)
            {
                return "TagSize3";
            }
            if (result <= 80)
            {
                return "TagSize4";
            }
            if (result <= 100)
            {
                return "TagSize5";
            }
            return "";
        }

        private void LoadTagCloud()
        {
            DataSet ds = Sql.FetchDataSet(@"
SELECT [Description]
      , (
          (
		  SELECT TOP 1 COUNT(*) as Total
          FROM
		  (
				SELECT Blogs_Articles.ArticleID, LTRIM(RTRIM(Blogs_Technology.TechnologyDescription)) as [Description]
				FROM Blogs_Articles
			  INNER JOIN Blogs_Technology ON Blogs_Technology.ArticleID = Blogs_Articles.ArticleID
			  WHERE Blogs_Articles.[IsPublished] = 1 AND Blogs_Articles.[PublishDate] < getdate() AND Blogs_Articles.RecordInfo.IsDeleted = 0
			  UNION
				SELECT Blogs_Articles.ArticleID, LTRIM(RTRIM(Blogs_Keywords.KeywordDescription)) as [Description]
				FROM Blogs_Articles
			  INNER JOIN Blogs_Keywords ON Blogs_Keywords.ArticleID = Blogs_Articles.ArticleID
			  WHERE Blogs_Articles.[IsPublished] = 1 AND Blogs_Articles.[PublishDate] < getdate() AND Blogs_Articles.RecordInfo.IsDeleted = 0
		  ) as data
          WHERE (data.[Description] = Tags.[Description] OR
                data.[Description] = Tags.[Description]) 
          GROUP BY data.[Description]
        )
		*
		(
			SELECT TOP 1 MAX(bk.Multiplier)
			FROM Blogs_Keywords as bk
			WHERE bk.KeywordDescription = Tags.Description
		)
		) as Total
FROM Tags
ORDER BY newid()

SELECT COUNT(*) as Total
FROM Blogs_Articles
INNER JOIN Blogs_Keywords ON Blogs_Keywords.ArticleID = Blogs_Articles.ArticleID
WHERE Blogs_Articles.[IsPublished] = 1 AND Blogs_Articles.[PublishDate] < getdate() AND Blogs_Articles.RecordInfo.IsDeleted = 0");

            string htmlContent = string.Empty;
            int totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tagText = dr[0].ToStringSafe();
                if (tagText.IsNullOrEmptyNot() && dr[1].IsNullOrEmptyNot())
                {
                    int tagCount = Convert.ToInt32(dr[1]);
                    htmlContent += String.Format("<a href='/Search/{1}' class='{2}'>{0}</a> ", tagText, HttpUtility.UrlEncode(tagText).Replace("+", " "), this.GetCssClass(tagCount, totalCount));
                }
            }
            this.litTagCloud.Text = htmlContent;
        }

        #endregion
    }
}