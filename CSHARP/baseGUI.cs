using Proxim.LOGIC.GENERICS;
using Proxim.LOGIC.GUI;
using Proxim.LOGIC.MSSQL_DATA_BASE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxim
{
    public partial class baseGUI : Form
    {       
        private central windowc= default(central);

        public baseGUI()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = false;            
        }

        private void baseGUI_Load(object sender, EventArgs e)
        {
            this.windowc = new central(this);
            this.windowc.ShowDialog();
        }
    }
}
