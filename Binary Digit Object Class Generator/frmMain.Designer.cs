namespace Binary_Digit_Object_Class_Generator
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnLoadDatabases = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxDatabases = new System.Windows.Forms.ComboBox();
            this.cbxTables = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodePreview = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lstAllTables = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection String :";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(216, 12);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(681, 64);
            this.txtConnectionString.TabIndex = 1;
            // 
            // btnLoadDatabases
            // 
            this.btnLoadDatabases.Location = new System.Drawing.Point(44, 46);
            this.btnLoadDatabases.Name = "btnLoadDatabases";
            this.btnLoadDatabases.Size = new System.Drawing.Size(166, 30);
            this.btnLoadDatabases.TabIndex = 2;
            this.btnLoadDatabases.Text = "Load Databases";
            this.btnLoadDatabases.UseVisualStyleBackColor = true;
            this.btnLoadDatabases.Click += new System.EventHandler(this.btnLoadDatabases_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Databases :";
            // 
            // cbxDatabases
            // 
            this.cbxDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDatabases.FormattingEnabled = true;
            this.cbxDatabases.Location = new System.Drawing.Point(216, 82);
            this.cbxDatabases.Name = "cbxDatabases";
            this.cbxDatabases.Size = new System.Drawing.Size(681, 26);
            this.cbxDatabases.TabIndex = 4;
            this.cbxDatabases.SelectedIndexChanged += new System.EventHandler(this.cbxDatabases_SelectedIndexChanged);
            // 
            // cbxTables
            // 
            this.cbxTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTables.FormattingEnabled = true;
            this.cbxTables.Location = new System.Drawing.Point(216, 114);
            this.cbxTables.Name = "cbxTables";
            this.cbxTables.Size = new System.Drawing.Size(681, 26);
            this.cbxTables.TabIndex = 7;
            this.cbxTables.SelectedIndexChanged += new System.EventHandler(this.cbxTables_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tables :";
            // 
            // txtCodePreview
            // 
            this.txtCodePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodePreview.Location = new System.Drawing.Point(15, 146);
            this.txtCodePreview.Multiline = true;
            this.txtCodePreview.Name = "txtCodePreview";
            this.txtCodePreview.ReadOnly = true;
            this.txtCodePreview.Size = new System.Drawing.Size(614, 439);
            this.txtCodePreview.TabIndex = 8;
            this.txtCodePreview.Click += new System.EventHandler(this.txtCodePreview_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(731, 555);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(166, 30);
            this.btnDownload.TabIndex = 9;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(635, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Export :";
            // 
            // lstAllTables
            // 
            this.lstAllTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAllTables.FormattingEnabled = true;
            this.lstAllTables.ItemHeight = 18;
            this.lstAllTables.Location = new System.Drawing.Point(635, 170);
            this.lstAllTables.Name = "lstAllTables";
            this.lstAllTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAllTables.Size = new System.Drawing.Size(262, 328);
            this.lstAllTables.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(635, 501);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "Namespace :";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNamespace.Location = new System.Drawing.Point(635, 522);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(262, 26);
            this.txtNamespace.TabIndex = 13;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 597);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lstAllTables);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.txtCodePreview);
            this.Controls.Add(this.cbxTables);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxDatabases);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadDatabases);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmMain";
            this.Text = "Binary Digit Object Class Generator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnLoadDatabases;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxDatabases;
        private System.Windows.Forms.ComboBox cbxTables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodePreview;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstAllTables;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamespace;
    }
}

