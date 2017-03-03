namespace Energeniaal
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.okButton = new System.Windows.Forms.Button();
            this.releaseNotesBox = new System.Windows.Forms.RichTextBox();
            this.releaseNotesPanel = new System.Windows.Forms.Panel();
            this.releaseNotesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.okButton.Location = new System.Drawing.Point(423, 471);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 26;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // releaseNotesBox
            // 
            this.releaseNotesBox.BackColor = System.Drawing.SystemColors.Window;
            this.releaseNotesBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.releaseNotesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.releaseNotesBox.Location = new System.Drawing.Point(10, 0);
            this.releaseNotesBox.Name = "releaseNotesBox";
            this.releaseNotesBox.ReadOnly = true;
            this.releaseNotesBox.Size = new System.Drawing.Size(472, 449);
            this.releaseNotesBox.TabIndex = 27;
            this.releaseNotesBox.Text = " ";
            // 
            // releaseNotesPanel
            // 
            this.releaseNotesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.releaseNotesPanel.BackColor = System.Drawing.SystemColors.Window;
            this.releaseNotesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.releaseNotesPanel.Controls.Add(this.releaseNotesBox);
            this.releaseNotesPanel.Location = new System.Drawing.Point(12, 12);
            this.releaseNotesPanel.Name = "releaseNotesPanel";
            this.releaseNotesPanel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.releaseNotesPanel.Size = new System.Drawing.Size(486, 453);
            this.releaseNotesPanel.TabIndex = 28;
            // 
            // HelpForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 506);
            this.Controls.Add(this.releaseNotesPanel);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpForm";
            this.Text = "Help";
            this.Load += new System.EventHandler(this.Help_Load);
            this.releaseNotesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.RichTextBox releaseNotesBox;
        private System.Windows.Forms.Panel releaseNotesPanel;
    }
}