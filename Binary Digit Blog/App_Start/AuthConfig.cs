namespace Binary_Digit_Blog
{
    internal static class AuthConfig
    {
        #region Public Methods and Operators

        public static void RegisterOpenAuth()
        {
            // See http://go.microsoft.com/fwlink/?LinkId=252803 for details on setting up this ASP.NET
            // application to support logging in via external services.

            OpenAuth.AuthenticationClients.AddTwitter(
                consumerKey: "QJtM0uuK3YJWeMlsGl6fmA",
                consumerSecret: "ESnbjTCdpMNrOHYr0bIxmPSq2xr9M6ymTjEvHCPEo30");

            OpenAuth.AuthenticationClients.AddFacebook(
                appId: "466360496735683",
                appSecret: "1cc8bfbf54ea1dfb96a56af48f328d64");

            OpenAuth.AuthenticationClients.AddMicrosoft(
                clientId: "000000004C0DD42A",
                clientSecret: "lZAcmgz5NaK5sxEamDlRjhFqXs-3PTrR");

            OpenAuth.AuthenticationClients.AddGoogle();
        }

        #endregion
    }
}