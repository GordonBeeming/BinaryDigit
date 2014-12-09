namespace Binary_Digit_Blog.Controls
{
    using System;
    using System.Web.UI;

    public partial class DefaultAsideContent : UserControl
    {
        #region Fields

        private bool _showGoogleAdd = true;

        #endregion

        #region Public Properties

        public bool ShowGoogleAdd
        {
            get
            {
                return this._showGoogleAdd;
            }
            set
            {
                this._showGoogleAdd = value;
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