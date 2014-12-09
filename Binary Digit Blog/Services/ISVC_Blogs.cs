namespace Binary_Digit_Blog.Services
{
    using System;
    using System.ServiceModel;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISVC_Blogs" in both code and config file together.
    [ServiceContract]
    public interface ISVC_Blogs
    {
        #region Public Methods and Operators

        [OperationContract]
        Guid AddBlogEntry(string title, string keywords, string technology, string content, string smartFileHanderPlaceholder, string userId);

        [OperationContract]
        Guid AddSmartFile(byte[] file, string fileName);

        #endregion
    }
}