using System;
using System.Web;
using System.Web.UI.WebControls;

using BinaryDigit;
using BinaryDigit.IO;

namespace BinaryDigit.IO
{
    using System;

    public static class SmartFileHelper
    {
        #region Public Methods and Operators

        public static byte[] RetrieveBytesFromDatabase(out string fileName, out string mimeType, int? id = null, Guid? rowGuid = null)
        {
            byte[] result = null;

            SmartFile sf = null;

            if (sf == null && id.HasValue)
            {
                sf = new SmartFile(id.Value);
            }
            if (sf == null && rowGuid.HasValue)
            {
                sf = new SmartFile(rowGuid.Value);
            }

            if (sf == null)
            {
                sf = new SmartFile();
            }

            fileName = sf.FileName;
            mimeType = sf.MimeType;
            result = sf.FileBytes;

            return result;
        }

        public static string RetrieveDirectUrl(int? id = null, Guid? rowGuid = null)
        {
            string result = null;

            SmartFile sf = null;

            if (sf == null && id.HasValue)
            {
                sf = new SmartFile(id.Value);
            }
            if (sf == null && rowGuid.HasValue)
            {
                sf = new SmartFile(rowGuid.Value);
            }

            if (sf == null)
            {
                sf = new SmartFile();
            }

            result = sf.CalcFileRawUrl;

            sf = null;

            return result;
        }

        #endregion
    }
}

public static class SmartFileHelperExtensions
{
    #region Public Methods and Operators

    public static bool HandlerDatabaseFileRequest(this HttpContext context)
    {
        bool result = false;

        if (context.Request.RawUrl.ToLower().StartsWith("/file$/"))
        {
            string key = context.Request.RawUrl.Remove(0, 7).Split('/')[0];
            context.RewritePath(Configuration.IO.SmartFileOutputHandlerAshx + "?i=" + key + "&g=" + key);
        }

        return result;
    }

    public static int UploadToDatebase(this FileUpload fu, int? id = null, Guid? rowGuid = null)
    {
        int result = -1;

        if (fu.HasFile)
        {
            SmartFile sf = null;

            if (sf == null && id.HasValue)
            {
                sf = new SmartFile(id.Value);
            }
            if (sf == null && rowGuid.HasValue)
            {
                sf = new SmartFile(rowGuid.Value);
            }

            if (sf == null)
            {
                sf = new SmartFile();
            }

            sf.FileBytes = fu.FileBytes;
            sf.FileName = fu.FileName;
            sf.MimeType = fu.PostedFile.ContentType;

            sf.Save();
        }

        return result;
    }

    #endregion
}