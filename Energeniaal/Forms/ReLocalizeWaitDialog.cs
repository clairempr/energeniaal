using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Energeniaal
{
    public partial class ReLocalizeWaitDialog : Form
    {
        public ReLocalizeWaitDialog()
        {
            InitializeComponent();
        }

        private void ReLocalizeWaitDialog_Activated(object sender, EventArgs e)
        {
            this.WaitTextLabel.Text = MyStrings.LanguageChangeWaitText;
            this.Refresh(); // otherwise label doesn't show up
            foreach (Form f in Application.OpenForms)
            {
                if (f is IReLocalizable)
                {
                    ((IReLocalizable)f).ReLocalize();
                }
            }
            this.Close();
        }

    }
}
