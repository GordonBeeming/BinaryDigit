namespace BinaryDigit
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;

    public abstract class SystemGlobalAshx : HttpApplication
    {
        #region Public Methods and Operators

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            bool mustRunDetaultCode = true;
            string currentRawUrlTolower = HttpContext.Current.Request.RawUrl.ToLower();
            foreach (string rawUrlTolower in Configuration.BeginRequest_IgnoreRawUrlsToLower)
            {
                if (currentRawUrlTolower.StartsWith(rawUrlTolower))
                {
                    mustRunDetaultCode = false;
                    break;
                }
            }
            if (mustRunDetaultCode)
            {
                if (!this.Request.RawUrl.Split('?')[0].Contains(".aspx"))
                {
                    if (File.Exists(this.Request.PhysicalPath + ".aspx"))
                    {
                        string pathToRewrite = this.Request.RawUrl;
                        if (pathToRewrite.Contains('?'))
                        {
                            pathToRewrite = pathToRewrite.Insert(pathToRewrite.IndexOf("?"), ".aspx");
                        }
                        else
                        {
                            pathToRewrite += ".aspx";
                        }

                        mustRunDetaultCode = false;
                        HttpContext.Current.RewritePath(pathToRewrite);
                    }
                }
                else
                {
                    mustRunDetaultCode = false;
                    HttpContext.Current.Response.RedirectPermanent(this.Request.RawUrl.Remove(this.Request.RawUrl.ToLower().IndexOf(".aspx"), 5));
                }

                if (mustRunDetaultCode)
                {
                    if (HttpContext.Current.HandlerDatabaseFileRequest())
                    {
                        mustRunDetaultCode = false;
                    }

                    if (mustRunDetaultCode)
                    {
                        this.BeginTheRequest(sender, e);
                    }
                }
            }
        }

        public abstract void BeginTheRequest(object sender, EventArgs e);

        #endregion
    }
}