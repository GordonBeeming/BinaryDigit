namespace Binary_Digit_Blog.Services
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;

    using BinaryDigit;
    using BinaryDigit.IO;

    using Binary_Digit_Blog.DAL.Data.Blogs;

    using Microsoft.Win32;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SVC_Blogs" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SVC_Blogs.svc or SVC_Blogs.svc.cs at the Solution Explorer and start debugging.
    public class SVC_Blogs : ISVC_Blogs
    {
        #region Public Methods and Operators

        public Guid AddBlogEntry(string title, string keywords, string technology, string content, string smartFileHanderPlaceholder, string userId)
        {
            return BlogsExtended.SaveBlogEntry(Guid.Empty, title, HttpUtility.HtmlDecode(Regex.Replace(content.Replace(smartFileHanderPlaceholder, Configuration.IO.SmartFileOutputHandlerAshx + "?g="), @"<!--[\S\s]*?-->|<(?:"".*?""|'.*?'|[\S\s])*?>", "")), content.Replace(smartFileHanderPlaceholder, "/File$/"), null, Guid.Parse(userId), keywords, technology);
        }

        public Guid AddSmartFile(byte[] fileBytes, string fileName)
        {
            Guid result = Guid.Empty;

            var sf = new SmartFile();

            sf.FileBytes = fileBytes;
            sf.FileName = fileName;
            sf.FileRawUrl = null;
            sf.MimeType = this.GetContentType(fileName);

            sf.Save();

            result = sf.SmartGuid;

            sf = null;

            return result;
        }

        #endregion

        #region Methods

        private string GetContentType(string fileName)
        {
            string contentType = "application/octetstream";
            string ext = Path.GetExtension(fileName).ToLower();
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (registryKey != null && registryKey.GetValue("Content Type") != null)
            {
                contentType = registryKey.GetValue("Content Type").ToString();
            }
            return contentType;
        }

        #endregion
    }
}