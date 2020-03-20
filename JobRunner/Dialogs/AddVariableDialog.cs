using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobRunner.ObjectModel;

namespace JobRunner.Dialogs
{
    public partial class AddVariableDialog : Form
    {
        public IVariableList Variables { get; set; }

        public AddVariableDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}