namespace BinaryDigit.IO
{
    using System;
    using System.IO;
    using System.Web;

    public class SmartFilesOutputHandler : IHttpHandler
    {
        #region Public Properties

        /// <summary>
        ///     You will need to configure this handler in the Web.config file of your
        ///     web and register it with IIS before being able to use it. For more information
        ///     see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        #region IHttpHandler Members
        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get
            {
                return true;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void ProcessRequest(HttpContext context)
        {
            int? id = null;
            Guid? rowGuid = null;
            if (!string.IsNullOrEmpty(context.Request.QueryString["i"]))
            {
                int temp = -1;
                if (int.TryParse(context.Request.QueryString["i"], out temp))
                {
                    id = temp;
                }
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["g"]))
            {
                Guid temp = Guid.Empty;
                if (Guid.TryParse(context.Request.QueryString["g"], out temp))
                {
                    rowGuid = temp;
                }
            }

            context.Response.Clear();
            context.Response.BufferOutput = false;
            string fileName = string.Empty;
            string mimeType = string.Empty;
            using (var ms = new MemoryStream(SmartFileHelper.RetrieveBytesFromDatabase(out fileName, out mimeType, id, rowGuid)))
            {
                context.Response.ContentType = mimeType;
                context.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                context.Response.AppendHeader("Content-Length", ms.Length.ToString());
                ms.WriteTo(context.Response.OutputStream);
            }
            context.Response.Flush();
            context.Response.Close();
        }

        #endregion

        #endregion
    }
}