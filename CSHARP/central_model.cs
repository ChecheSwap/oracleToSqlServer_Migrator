using Proxim.LOGIC.GENERICS;
using Proxim.LOGIC.MSSQL_DATA_BASE;
using Proxim.LOGIC.ORACLE_DATA_BASE;
using Proxim.LOGIC.TYPES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxim.LOGIC.GUI
{    
    public class central_model
    {
        private central xbase = default(central);
        private oracleDB DB = default(oracleDB);
        private msSqlDB SDB = default(msSqlDB);       
        
        private List<userNames> usrnames = default(List<userNames>);

        private Thread xthread = default(Thread);

        private string oraUser = string.Empty;
        private string dbname = default(string);
    
        public central_model(central xbase)
        {
            this.xbase = xbase;            
            this.SDB = new msSqlDB();
            this.DB = new oracleDB(this.SDB);
            
        }

        public void configs()
        {
            this.xbase.Text = "Proxim Data Base Export Tool by @ChecheSwap";
            this.xbase.FormBorderStyle = FormBorderStyle.Fixed3D;
            
            this.xbase.FormClosing += (sender, args) => {
                if (!msg.yesno("Desea Salir del Programa?"))
                {
                    args.Cancel = true;
                }
                else
                {
                    this.xbase.mybase.Close();
                }
            };            
            this.xbase.Shown += (a, b) => {
                this.xbase.model.add_users();                
            };

            this.xbase.blisttables.Enabled = true;

            this.xbase.blistusrs.ItemCheck += (a, b) =>
            {

                for (int x = 0; x<this.xbase.blistusrs.Items.Count; ++x)
                { 
                    if(!(x == b.Index))
                    {
                        this.xbase.blistusrs.SetItemChecked(x,false);
                    }
                }

                if (b.NewValue == CheckState.Checked)
                {
                    this.oraUser = this.usrnames.ElementAt(b.Index).name;
                    this.fill_blist_tables(this.oraUser);
                    this.fill_blist_views(this.oraUser);

                    this.xbase.txtdbname.Text = this.oraUser;
                    this.xbase.cbdml.Checked = true;
                    this.xbase.cbref.Checked = true;
                }
                else
                {
                    this.oraUser = string.Empty;
                    this.clearblist(this.xbase.blisttables);
                    this.clearblist(this.xbase.blistvistas);

                    this.xbase.txtdbname.Text = "";
                    this.xbase.cbdml.Checked = false;
                    this.xbase.cbref.Checked = false;
                    this.xbase.cbviews.Checked = false;
                }
            };

            this.xbase.btnexport.Click += (sender, b) => {

                if (!(this.oraUser == string.Empty))
                {
                    this.dbname = this.xbase.txtdbname.Text.Trim();
                    this.onProcess(this.oraUser);
                }
                else
                {
                    msg.danger("Please, select a DB User.");
                }
            };
        }
        public void add_users()
        {
            this.usrnames = this.DB.getUsrnames();
            foreach(var x in this.usrnames)
            {
                this.xbase.blistusrs.Items.Add(x.name);
            }
        }

        public void fill_blist_tables(string usr)
        {
            foreach(var x in this.DB.getTablesofUser(usr))
            {
                this.xbase.blisttables.Items.Add(x.name);                
            }

            for(int y = 0; y < this.xbase.blisttables.Items.Count; ++y)
            {
                this.xbase.blisttables.SetItemChecked(y,true);
            }
        }

        public void fill_blist_views(string usr)
        {
            foreach (var x in this.DB.getViewsNames(usr))
            {
                this.xbase.blistvistas.Items.Add(x);
            }

            for (int y = 0; y < this.xbase.blistvistas.Items.Count; ++y)
            {
                this.xbase.blistvistas.SetItemChecked(y, true);
            }
        }

        private void clearblist(CheckedListBox cb)
        {
            cb.Items.Clear();
        }

        private void onProcess(string user)
        {
            this.xbase.Enabled = false;
            this.xthread = new Thread(new ThreadStart(() =>
            {               

                this.backgroundOpsDB(user, this.dbname, this.xbase.cbdml.Checked, this.xbase.cbref.Checked, this.xbase.cbviews.Checked);

            }));
            this.xthread.IsBackground = true;
            this.xthread.Start();            
            this.xbase.dialogExport = new onExport();
            this.xbase.dialogExport.ShowDialog();

        }
        private void backgroundOpsDB(string user, string dbname,  bool dml, bool refc, bool views)
        {
            bool Eflag = true;
            if (!(user == string.Empty))
            {
                if (!(dbname.Trim() == string.Empty))
                {
                    if (this.SDB.createDB(dbname))
                    {
                        if (this.DB.onInit(user))
                        {
                            if (this.DB.onSecond())
                            {
                                if (this.DB.insertDDL())
                                {
                                    if (dml)
                                    {
                                        if (!this.DB.insertDML())
                                        {
                                            msg.error("Proceso Inconsistente en Procedimiento de Migracion del DML");
                                            Eflag = false;
                                        }
                                    }

                                    if (refc)
                                    {
                                        if (!this.DB.insertRefConst())
                                        {
                                            msg.error("Proceso Inconsistente en Procedimiento de Migrado de las Restricciones Referenciales");
                                            Eflag = false;
                                        }
                                    }
                                    if (views)
                                    {
                                        if (!this.DB.insertViews())
                                        {
                                            msg.error("Proceso Inconsistente en procedimiento de migrado de Sql vistas.");
                                            Eflag = false;
                                        }
                                    }                                    
                                }
                                else
                                {
                                    msg.error("Proceso Inconsistente en Procedimiento de Migracion del DDL.");
                                    Eflag = false;
                                }
                            }
                            else
                            {
                                msg.error("Proceso Inconsistente en Procedimiento Secundario de Sintesis.");
                                Eflag = false;
                            }
                        }
                        else
                        {
                            msg.error("Proceso Inconsistente en Procedimiento Primario.");
                            Eflag = false;
                        }
                    }
                    else
                    {
                        msg.error("Error al crear Base de datos en Microsoft Sql Server.");
                        Eflag = false;
                    }

                    if (Eflag)
                    {
                        msg.showInfo("DB Export Succesfully!");
                    }
                    else
                    {
                        msg.danger("DB Export ended with warnings.");
                    }
                }
                else
                {
                    msg.danger("Nombre de base de datos no valido, ingrese un nombre para crear la nueva DB");
                    Eflag = false;                    
                }
            }
            else
            {
                msg.danger("Seleccion Un Esquema a Exportar.");
            }

            if (this.xbase.InvokeRequired)
            {
                this.xbase.Invoke(new MethodInvoker(delegate () {
                    this.xbase.dialogExport.flag = true;
                    this.xbase.dialogExport.Close();
                    this.xbase.Enabled = true;
                }));
            }                                                          
        }        
    }
}
