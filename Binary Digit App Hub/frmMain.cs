namespace Binary_Digit_App_Hub
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Windows.Forms;

    using Binary_Digit_App_Hub.Plugin;

    public partial class frmMain : Form, IPartImportsSatisfiedNotification
    {
        #region Fields

        [ImportMany(typeof(BasePlugin))]
        private readonly IEnumerable<BasePlugin> _plugins = null;

        #endregion

        #region Constructors and Destructors

        public frmMain()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods and Operators

        public void OnImportsSatisfied()
        {
            this.UseLoadedPlugins();
        }

        #endregion

        #region Methods

        private void AddToMenu(ToolStripItemCollection items, BasePlugin plugin, string menuStructureLeft, string menuItemText)
        {
            string lookingFor = menuStructureLeft.Split('/')[0].Trim();
            menuStructureLeft = (menuStructureLeft.TrimEnd('/') + '/').Remove(0, (menuStructureLeft.TrimEnd('/') + '/').IndexOf('/') + 1);
            bool found = false;
            foreach (ToolStripMenuItem tsmi in items)
            {
                if (string.Compare(tsmi.Text, lookingFor, true) == 0)
                {
                    if (menuStructureLeft.IsNullOrEmptyNot())
                    {
                        this.AddToMenu(tsmi.DropDownItems, plugin, menuStructureLeft, menuItemText);
                    }
                    else
                    {
                        var thisItem = new ToolStripMenuItem();
                        thisItem.Text = menuItemText;
                        items.Add(thisItem);
                        thisItem.Click += (sv, ev) =>
                        {
                            //look to clone the object
                            plugin.Show();
                        };
                    }
                    break;
                }
            }
            if (!found)
            {
                var thisItem = new ToolStripMenuItem();
                thisItem.Text = lookingFor;
                items.Add(thisItem);
                if (menuStructureLeft.IsNullOrEmptyNot())
                {
                    this.AddToMenu(thisItem.DropDownItems, plugin, menuStructureLeft, menuItemText);
                }
                else
                {
                    var thisClickItem = new ToolStripMenuItem();
                    thisClickItem.Text = menuItemText;
                    thisItem.DropDownItems.Add(thisClickItem);
                    thisClickItem.Click += (sv, ev) =>
                    {
                        //look to clone the object
                        plugin.Show();
                    };
                }
            }
        }

        private void LoadPlugins()
        {
            //AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            var catalog = new DirectoryCatalog(".\\", "*.dll");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        private void UseLoadedPlugins()
        {
            foreach (BasePlugin frm in (from o in this._plugins
                                        where o.ShowInMenu
                                        select o))
            {
                frm.MdiParent = this;
                frm.IsMdiContainer = false;
                frm.FormClosing += (sv, ev) =>
                {
                    ev.Cancel = true;
                    frm.Visible = false;
                    //frm = Activator.CreateInstance(frm.GetType()) as Binary_Digit_App_Hub.Plugin.BasePlugin;
                    //frm.MdiParent = this;
                    //frm.IsMdiContainer = false;
                };
                //frm.Show();

                this.AddToMenu(this.menuStrip1.Items, frm, frm.MenuStructure ?? "Tools", frm.MenuItemText ?? frm.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.LoadPlugins();
        }

        #endregion
    }
}