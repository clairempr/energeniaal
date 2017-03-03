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
    public partial class HelpForm : Form, IReLocalizable
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            this.SetText();
        }

        private void SetText()
        {
            this.Text = MyStrings.Help;
            this.releaseNotesBox.Rtf = MyStrings.ReleaseNotesRtf;
        }

        void IReLocalizable.ReLocalize()
        {
            this.SetText();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
