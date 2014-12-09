namespace Binary_Digit_Object_Class_Generator
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    using BinaryDigit.DataAccess;
    using BinaryDigit.IO;

    using Binary_Digit_App_Hub.Plugin;

    public partial class frmMain : BasePlugin
    {
        #region Constructors and Destructors

        public frmMain()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        private string _connectionString
        {
            get
            {
                string result = string.Empty;
                if (this.txtConnectionString.InvokeRequired)
                {
                    this.txtConnectionString.Invoke(new Action(() => { result = this.txtConnectionString.Text; }));
                }
                else
                {
                    result = this.txtConnectionString.Text;
                }
                return result;
            }
        }

        private string _databaseName
        {
            get
            {
                string result = string.Empty;
                if (this.cbxDatabases.InvokeRequired)
                {
                    this.cbxDatabases.Invoke(new Action(() => { result = this.cbxDatabases.Text; }));
                }
                else
                {
                    result = this.cbxDatabases.Text;
                }
                return result;
            }
        }

        private string _tableName
        {
            get
            {
                string result = string.Empty;
                if (this.cbxTables.InvokeRequired)
                {
                    this.cbxTables.Invoke(new Action(() => { result = this.cbxTables.Text; }));
                }
                else
                {
                    result = this.cbxTables.Text;
                }
                return result;
            }
        }

        #endregion

        #region Methods

        private void LoadTables()
        {
            this.cbxTables.DataSource = Sql.GetTables(this._databaseName, this._connectionString);
            this.lstAllTables.DataSource = this.cbxTables.DataSource;
            if (this.cbxTables.Items.Count > 0)
            {
                this.cbxTables.SelectedIndex = 0;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstAllTables.SelectedItems.Count > 0)
                {
                    string rootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\_objects\\";
                    if (!Directory.Exists(rootDirectory))
                    {
                        Directory.CreateDirectory(rootDirectory);
                    }

                    foreach (string table in this.lstAllTables.SelectedItems)
                    {
                        var code = new CodeGen(table, this._databaseName, this._connectionString);
                        File.WriteAllText(rootDirectory + table + ".cs", @"namespace " + this.txtNamespace.Text + @"
{
    " + code.GetClassObject() + @"
}");
                    }
                    MessageBox.Show("Complete", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnLoadDatabases_Click(object sender, EventArgs e)
        {
            if (this.txtConnectionString.Text.IsNullOrEmptyNot())
            {
                this.cbxDatabases.DataSource = Sql.GetDatabases(this._connectionString);
                if (this.cbxDatabases.Items.Count > 0)
                {
                    this.cbxDatabases.SelectedIndex = 0;
                }
            }
        }

        private void cbxDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadTables();
        }

        private void cbxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var code = new CodeGen(this._tableName, this._databaseName, this._connectionString);
            this.txtCodePreview.Text = code.GetClassObject();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var obj = new SmartFile();
        }

        private void txtCodePreview_Click(object sender, EventArgs e)
        {
            this.txtCodePreview.SelectAll();
        }

        #endregion
    }
}