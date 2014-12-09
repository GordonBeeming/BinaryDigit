namespace Binary_Digit_Blog.Account.Blogs
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using BinaryDigit.IO;

    public partial class FileBrowser : Page
    {
        #region Methods

        protected string GetFileSize()
        {
            string result = string.Empty;
            long totalBytes = Convert.ToInt64(this.Eval("FileBytesLength"));
            long remainder = 0;

            //if (totalBytes >= (long)Math.Pow(10, 24))
            //{
            //    result = Math.DivRem(totalBytes, (long)Math.Pow(10, 24), out remainder) + "." + remainder + "YB";
            //}
            //else if (totalBytes >= (long)Math.Pow(10, 21))
            //{
            //    result = Math.DivRem(totalBytes, (long)Math.Pow(10, 21), out remainder) + "." + remainder + "ZB";
            //}
            //else if (totalBytes >= (long)Math.Pow(10, 18))
            //{
            //    result = Math.DivRem(totalBytes, (long)Math.Pow(10, 18), out remainder) + "." + remainder + "EB";
            //}
            //else 
            if (totalBytes >= (long)Math.Pow(10, 15))
            {
                result = Math.DivRem(totalBytes, (long)Math.Pow(10, 15), out remainder) + "." + remainder + "PB";
            }
            else if (totalBytes >= (long)Math.Pow(10, 12))
            {
                result = Math.DivRem(totalBytes, (long)Math.Pow(10, 12), out remainder) + "." + remainder + "TB";
            }
            else if (totalBytes >= (long)Math.Pow(10, 9))
            {
                result = Math.DivRem(totalBytes, (long)Math.Pow(10, 9), out remainder) + "." + remainder + "GB";
            }
            else if (totalBytes >= (long)Math.Pow(10, 6))
            {
                result = Math.DivRem(totalBytes, (long)Math.Pow(10, 6), out remainder) + "." + remainder + "MB";
            }
            else if (totalBytes >= (long)Math.Pow(10, 3))
            {
                result = Math.DivRem(totalBytes, (long)Math.Pow(10, 3), out remainder) + "." + remainder + "kB";
            }
            else
            {
                result = totalBytes + "b";
            }
            return this.Eval("FileName") + " (" + result + ")";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadData();
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            var obj = new SmartFile(new Guid(((Button)sender).CommandArgument));
            obj.Delete();
            this.Response.Redirect(this.Request.RawUrl);
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            this.upFile.UploadToDatebase();
            this.Response.Redirect(this.Request.RawUrl);
        }

        private void LoadData()
        {
            this.gvData.DataSource = new SmartFile().GetAll("RecordInfo.CreateDateTime");
            this.gvData.DataBind();
        }

        #endregion
    }
}