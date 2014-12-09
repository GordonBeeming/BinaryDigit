namespace Binary_Digit_Blog.Controls
{
    using System;
    using System.Data;
    using System.Web.UI;

    using BinaryDigit.DataAccess;

    public partial class ArticleArchiveTree : UserControl
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadArticleArchiveTree();
            }
        }

        private void LoadArticleArchiveTree()
        {
            DataSet ds = Sql.FetchDataSet(@"
SELECT [PublishDateYear]
      ,[PublishDateMonth]
      ,[DisplayName]
FROM [dbo].[ArticleArchiveDates]
ORDER BY PublishDateYear DESC, PublishDateMonth DESC");

            string htmlContent = string.Empty;
            int lastYear = -1;
            int lastMonth = -1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (lastYear != (int)dr["PublishDateYear"])
                {
                    if (lastYear != -1)
                    {
                        htmlContent += "</ul>";
                    }
                    else
                    {
                        htmlContent += "<ul>";
                    }
                    htmlContent += "<li><strong><a href='/SearchDate/" + dr["PublishDateYear"] + "'>" + (int)dr["PublishDateYear"] + "</a></strong></li>";
                    htmlContent += "<ul>";

                    lastYear = (int)dr["PublishDateYear"];
                }
                htmlContent += "<li><a href='/SearchDate/" + ((string)dr["DisplayName"]).Split('(')[0].Trim() + "'>" + dr["DisplayName"] + "</a></li>";
            }
            if (htmlContent.IsNullOrEmptyNot())
            {
                htmlContent += "</ul></ul>";
            }
            this.litArticleArchiveTreeData.Text = htmlContent;
        }

        #endregion
    }
}