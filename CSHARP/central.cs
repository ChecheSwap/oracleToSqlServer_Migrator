using Proxim.LOGIC.GENERICS;
using Proxim.LOGIC.ORACLE_DATA_BASE;
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
    public partial class central : form_base
    {
        public baseGUI mybase = default(baseGUI);
        public central_model model = default(central_model);
        public onExport dialogExport = default(onExport);

        public central(baseGUI mybase){
            InitializeComponent();
            this.mybase = mybase;
            this.model = new central_model(this);
            this.model.configs();
        }

        public central()
        {
            InitializeComponent();                        
        }

        private void central_Load(object sender, EventArgs e)
        {}

        private void controlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msg.showInfo("PROXIM Database Export Tool Beta Edition. by @ChecheSwap");
        }
    }
}
