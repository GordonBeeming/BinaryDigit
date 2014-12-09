namespace Binary_Digit_Blog
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;

    public partial class SiteMaster : MasterPage
    {
        #region Constants

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";

        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";

        #endregion

        #region Fields

        private string _antiXsrfTokenValue;

        #endregion

        #region Properties

        protected string CodeplexName
        {
            get
            {
                return ConfigurationManager.AppSettings["codeplex.name"];
            }
        }

        protected string CodeplexUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["codeplex.url"];
            }
        }

        protected string CodeprojectName
        {
            get
            {
                return ConfigurationManager.AppSettings["codeproject.name"];
            }
        }

        protected string CodeprojectUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["codeproject.url"];
            }
        }

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

        protected string MicrosoftAccountName
        {
            get
            {
                return ConfigurationManager.AppSettings["microsoftAccount.name"];
            }
        }

        protected string MicrosoftAccountUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["microsoftAccount.url"];
            }
        }

        protected string MsdnName
        {
            get
            {
                return ConfigurationManager.AppSettings["msdn.name"];
            }
        }

        protected string MsdnUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["msdn.url"];
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

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            HttpCookie requestCookie = this.Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                this._antiXsrfTokenValue = requestCookie.Value;
                this.Page.ViewStateUserKey = this._antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                this._antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                this.Page.ViewStateUserKey = this._antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                                     {
                                         HttpOnly = true,
                                         Value = this._antiXsrfTokenValue
                                     };
                if (FormsAuthentication.RequireSSL && this.Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                this.Response.Cookies.Set(responseCookie);
            }

            this.Page.PreLoad += this.master_Page_PreLoad;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.form1.Action = this.Request.RawUrl;
#if DEBUG
            this.ScriptManager1.ScriptMode = ScriptMode.Debug;
#endif
            if (string.IsNullOrEmpty(this.Page.MetaDescription))
            {
                this.Page.MetaDescription = "Binary Digit (B1n4ry D1g1t) blog is a place for me to air my views and share some of my knowledge";
            }
            if (string.IsNullOrEmpty(this.Page.MetaKeywords))
            {
                this.Page.MetaKeywords = string.Empty;
            }
            this.Page.MetaKeywords += "Binary Digit,B1n4ry D1g1t,Blog,C#,C-Sharp,MS SQL,Microsoft SQL,Microsoft SQL Server,ASP.net,T-SQL";
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Set Anti-XSRF token
                this.ViewState[AntiXsrfTokenKey] = this.Page.ViewStateUserKey;
                this.ViewState[AntiXsrfUserNameKey] = this.Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)this.ViewState[AntiXsrfTokenKey] != this._antiXsrfTokenValue
                    || (string)this.ViewState[AntiXsrfUserNameKey] != (this.Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        #endregion
    }
}