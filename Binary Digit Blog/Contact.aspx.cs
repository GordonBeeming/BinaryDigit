namespace Binary_Digit_Blog
{
    using System;
    using System.Configuration;
    using System.Web.UI;

    public partial class Contact : Page
    {
        #region Properties

        protected string FacebookName
        {
            get
            {
                return ConfigurationManager.AppSettings["facebook.name"];
            }
        }

        protected string FacebookUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["facebook.url"];
            }
        }

        protected string LinkedInName
        {
            get
            {
                return ConfigurationManager.AppSettings["linkedin.name"];
            }
        }

        protected string LinkedInUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["linkedin.url"];
            }
        }

        protected string TwitterName
        {
            get
            {
                return ConfigurationManager.AppSettings["twitter.name"];
            }
        }

        protected string TwitterUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["twitter.url"];
            }
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion
    }
}