namespace Binary_Digit_App_Hub.Plugin
{
    using System.ComponentModel.Composition;
    using System.Windows.Forms;

    [InheritedExport(typeof(BasePlugin))]
    public class BasePlugin : Form
    {
        #region Public Properties

        public virtual string MenuItemText
        {
            get
            {
                return null;
            }
        }

        public virtual string MenuStructure
        {
            get
            {
                return null;
            }
        }

        public virtual bool ShowInMenu
        {
            get
            {
                return this.GetType() != typeof(BasePlugin);
            }
        }

        #endregion

        #region Properties

        internal new bool IsMdiContainer
        {
            get
            {
                return base.IsMdiContainer;
            }
            set
            {
                base.IsMdiContainer = value;
            }
        }

        internal new Form MdiParent
        {
            get
            {
                return base.MdiParent;
            }
            set
            {
                base.MdiParent = value;
            }
        }

        #endregion
    }
}