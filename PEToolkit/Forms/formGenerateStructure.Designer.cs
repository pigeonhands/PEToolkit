namespace PEViewer.Forms
{
    partial class formGenerateStructure
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbStructure = new System.Windows.Forms.ComboBox();
            this.rbComplete = new System.Windows.Forms.RadioButton();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.clbStructureSelect = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Structure:";
            // 
            // cbStructure
            // 
            this.cbStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStructure.FormattingEnabled = true;
            this.cbStructure.Location = new System.Drawing.Point(81, 9);
            this.cbStructure.Name = "cbStructure";
            this.cbStructure.Size = new System.Drawing.Size(191, 21);
            this.cbStructure.TabIndex = 1;
            this.cbStructure.SelectedIndexChanged += new System.EventHandler(this.cbStructure_SelectedIndexChanged);
            // 
            // rbComplete
            // 
            this.rbComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbComplete.AutoSize = true;
            this.rbComplete.Checked = true;
            this.rbComplete.Location = new System.Drawing.Point(15, 45);
            this.rbComplete.Name = "rbComplete";
            this.rbComplete.Size = new System.Drawing.Size(115, 17);
            this.rbComplete.TabIndex = 2;
            this.rbComplete.TabStop = true;
            this.rbComplete.Text = "Complete Structure";
            this.rbComplete.UseVisualStyleBackColor = true;
            // 
            // rbCustom
            // 
            this.rbCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(136, 45);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(106, 17);
            this.rbCustom.TabIndex = 3;
            this.rbCustom.Text = "Custom Structure";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
            // 
            // clbStructureSelect
            // 
            this.clbStructureSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbStructureSelect.Enabled = false;
            this.clbStructureSelect.FormattingEnabled = true;
            this.clbStructureSelect.Location = new System.Drawing.Point(15, 68);
            this.clbStructureSelect.Name = "clbStructureSelect";
            this.clbStructureSelect.Size = new System.Drawing.Size(257, 154);
            this.clbStructureSelect.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(94, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // formGenerateStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.clbStructureSelect);
            this.Controls.Add(this.rbCustom);
            this.Controls.Add(this.rbComplete);
            this.Controls.Add(this.cbStructure);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "formGenerateStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Structures";
            this.Load += new System.EventHandler(this.formGenerateStructure_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStructure;
        private System.Windows.Forms.RadioButton rbComplete;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.CheckedListBox clbStructureSelect;
        private System.Windows.Forms.Button button1;
    }
}