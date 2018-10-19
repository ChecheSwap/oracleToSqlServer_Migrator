using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxim.LOGIC.GENERICS
{
    public partial class form_base : Form
    {
        public form_base()
        {
            InitializeComponent();

           // this.ShowIcon = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;           
        }

        private void form_base_Load(object sender, EventArgs e)
        {            
        }
    }
}
