namespace Binary_Digit_Blog.Account
{
    using System;
    using System.Web.Security;
    using System.Web.UI;

    public partial class Register : Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RegisterUser.ContinueDestinationPageUrl = this.Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(this.RegisterUser.UserName, false);

            string continueUrl = this.RegisterUser.ContinueDestinationPageUrl;
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            this.Response.Redirect(continueUrl);
        }

        #endregion
    }
}