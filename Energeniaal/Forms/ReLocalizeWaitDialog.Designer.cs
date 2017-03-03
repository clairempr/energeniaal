namespace Energeniaal
{
    partial class ReLocalizeWaitDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReLocalizeWaitDialog));
            this.WaitTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WaitTextLabel
            // 
            this.WaitTextLabel.AutoSize = true;
            this.WaitTextLabel.Location = new System.Drawing.Point(93, 17);
            this.WaitTextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WaitTextLabel.Name = "WaitTextLabel";
            this.WaitTextLabel.Size = new System.Drawing.Size(142, 17);
            this.WaitTextLabel.TabIndex = 0;
            this.WaitTextLabel.Text = "Switching language...";
            this.WaitTextLabel.UseWaitCursor = true;
            // 
            // ReLocalizeWaitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 49);
            this.Controls.Add(this.WaitTextLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReLocalizeWaitDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.UseWaitCursor = true;
            this.Activated += new System.EventHandler(this.ReLocalizeWaitDialog_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WaitTextLabel;
    }
}