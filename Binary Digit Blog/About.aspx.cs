namespace Binary_Digit_Blog
{
    using System;
    using System.Web.UI;

    public partial class About : Page
    {
        #region Methods

        protected string GetAge()
        {
            return ((DateTime.Now - new DateTime(1989, 11, 22)).TotalDays / 365.25).ToString().Split('.')[0];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion
    }
}