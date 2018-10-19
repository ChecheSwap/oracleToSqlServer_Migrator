using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxim.LOGIC.GUI
{
    public partial class onExport : Form
    {
        public bool flag = false;
        public onExport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;

            this.FormClosing += (a, c) =>
            {
                if (!flag)
                {
                    c.Cancel = true;
                }
            };
        }

        private void onExport_Load(object sender, EventArgs e)
        {

        }
    }
}
