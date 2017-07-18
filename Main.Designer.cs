using System.Drawing;
using System.Windows.Forms;

namespace Powersaves
{
    partial class Main
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

        private Button B_Open;
        private Button B_Export;
        private TextBox TB_Path;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;

        private void InitializeComponent()
        {
            this.B_Open = new System.Windows.Forms.Button();
            this.B_Export = new System.Windows.Forms.Button();
            this.TB_Path = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.L_Edited = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // B_Open
            // 
            this.B_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Open.Location = new System.Drawing.Point(291, 10);
            this.B_Open.Name = "B_Open";
            this.B_Open.Size = new System.Drawing.Size(75, 23);
            this.B_Open.TabIndex = 0;
            this.B_Open.Text = "Browse...";
            this.B_Open.UseVisualStyleBackColor = true;
            this.B_Open.Click += new System.EventHandler(this.OpenBrowse);
            // 
            // B_Export
            // 
            this.B_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Export.Enabled = false;
            this.B_Export.Location = new System.Drawing.Point(250, 39);
            this.B_Export.Name = "B_Export";
            this.B_Export.Size = new System.Drawing.Size(114, 23);
            this.B_Export.TabIndex = 1;
            this.B_Export.Text = "Export";
            this.B_Export.UseVisualStyleBackColor = true;
            this.B_Export.Click += new System.EventHandler(this.ExportBackup);
            // 
            // TB_Path
            // 
            this.TB_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Path.Location = new System.Drawing.Point(12, 12);
            this.TB_Path.Name = "TB_Path";
            this.TB_Path.ReadOnly = true;
            this.TB_Path.Size = new System.Drawing.Size(270, 20);
            this.TB_Path.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // L_Edited
            // 
            this.L_Edited.AutoSize = true;
            this.L_Edited.Location = new System.Drawing.Point(12, 44);
            this.L_Edited.Name = "L_Edited";
            this.L_Edited.Size = new System.Drawing.Size(57, 13);
            this.L_Edited.TabIndex = 5;
            this.L_Edited.Text = "Edited: {0}";
            this.L_Edited.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 72);
            this.Controls.Add(this.L_Edited);
            this.Controls.Add(this.TB_Path);
            this.Controls.Add(this.B_Export);
            this.Controls.Add(this.B_Open);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Powersave Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label L_Edited;
    }
}