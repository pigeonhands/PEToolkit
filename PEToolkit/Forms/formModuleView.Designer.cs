namespace PEToolkit.Forms
{
    partial class formModuleView
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
            this.components = new System.ComponentModel.Container();
            this.lvModules = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmModules = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpSelectedModulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadSelectedModulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvModules
            // 
            this.lvModules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvModules.ContextMenuStrip = this.cmModules;
            this.lvModules.FullRowSelect = true;
            this.lvModules.GridLines = true;
            this.lvModules.Location = new System.Drawing.Point(12, 12);
            this.lvModules.Name = "lvModules";
            this.lvModules.Size = new System.Drawing.Size(659, 349);
            this.lvModules.TabIndex = 1;
            this.lvModules.UseCompatibleStateImageBehavior = false;
            this.lvModules.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 167;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Handle";
            this.columnHeader2.Width = 106;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Path";
            this.columnHeader4.Width = 138;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Module Size";
            this.columnHeader3.Width = 138;
            // 
            // cmModules
            // 
            this.cmModules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.dumpSelectedModulesToolStripMenuItem,
            this.unloadSelectedModulesToolStripMenuItem});
            this.cmModules.Name = "cmModules";
            this.cmModules.Size = new System.Drawing.Size(209, 92);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // dumpSelectedModulesToolStripMenuItem
            // 
            this.dumpSelectedModulesToolStripMenuItem.Name = "dumpSelectedModulesToolStripMenuItem";
            this.dumpSelectedModulesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.dumpSelectedModulesToolStripMenuItem.Text = "Dump Selected Modules";
            this.dumpSelectedModulesToolStripMenuItem.Click += new System.EventHandler(this.dumpSelectedModulesToolStripMenuItem_Click);
            // 
            // unloadSelectedModulesToolStripMenuItem
            // 
            this.unloadSelectedModulesToolStripMenuItem.Name = "unloadSelectedModulesToolStripMenuItem";
            this.unloadSelectedModulesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.unloadSelectedModulesToolStripMenuItem.Text = "Unload Selected Modules";
            this.unloadSelectedModulesToolStripMenuItem.Click += new System.EventHandler(this.unloadSelectedModulesToolStripMenuItem_Click);
            // 
            // formModuleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 373);
            this.Controls.Add(this.lvModules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "formModuleView";
            this.Text = "Modules";
            this.Load += new System.EventHandler(this.formModuleView_Load);
            this.cmModules.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvModules;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip cmModules;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpSelectedModulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadSelectedModulesToolStripMenuItem;
    }
}