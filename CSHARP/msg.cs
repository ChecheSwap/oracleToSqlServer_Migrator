using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxim.LOGIC.GENERICS
{
    public static class msg
    {
        public static void danger(string msg)
        {
            MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void showInfo(string msg)
        {
            MessageBox.Show(msg, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void error(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool yesno(string txt)
        {
            bool flag = false;

            DialogResult dr = MessageBox.Show(txt, "Confirmacion.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                flag = !flag;
            }
            return flag;
        }
    }
}
