using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Words
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            var recordsForm = new frmRecords();
            recordsForm.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var reciteForm = new frmRecite();
            reciteForm.ShowDialog();
        }
    }
}
