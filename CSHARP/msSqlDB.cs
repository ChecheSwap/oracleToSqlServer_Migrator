using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Proxim.LOGIC.GENERICS;
using System.Data;

namespace Proxim.LOGIC.MSSQL_DATA_BASE
{
    public class msSqlDB
    {
        private SqlConnection xconn = default(SqlConnection);  
        
        private string xconnString = "Server=CHECHESWAPPC;Integrated security=SSPI;database=master"; //FOR SYNCHRONIZE VALS

        private string interString = default(string);

        private bool conn(string connString)
        {
            bool flag = false;
            try
            {
                xconn = new SqlConnection(connString);
                xconn.Open();
                flag = !flag;
            }
            catch(Exception ex)
            {
                msg.error(ex.ToString());
            }

            return flag;
        }

        private bool disconn()
        {
            bool flag = false;
            try
            {
                if(xconn.State == ConnectionState.Open)
                {
                    xconn.Close();
                }
                flag = !flag;
            }
            catch(Exception ex)
            {
                msg.error(ex.ToString());
            }
            return flag;
        }

        public bool createSCHEMA(string schname)
        {
            bool flag = false;

            if (this.conn(this.xconnString))
            {
                string stmt = $"CREATE SCHEMA {schname}";
            }
            else
            {
                msg.error("Problema de conexion con Microsoft Sql Server at @master.");
            }
            return flag;
        }
        public bool createDB(string dbname)
        {
            bool flag = false;
            string xstr;

            this.dropDB(dbname);

            try
            {
                if (this.conn(this.xconnString))
                {
                    xstr = $"CREATE DATABASE {dbname}";

                    using (SqlCommand sc = new SqlCommand(xstr, xconn))
                    {                        
                        sc.ExecuteNonQuery();
                        this.interString = $"Server=CHECHESWAPPC;Integrated security=SSPI;database={dbname}"; //FOR INTERACTIONS
                    }
                    flag = !flag;
                }
            }
            catch(Exception ex)
            {
                msg.error(ex.Message);
            }
            finally
            {
                this.disconn();
            }
            return flag;
        }

        public bool dropDB(string dbname)
        {
            bool flag = false;
            bool interflag = false;
            string xstr;
            try
            {
                if (this.conn(this.xconnString))
                {
                    xstr = $"SELECT * FROM SYS.DATABASES WHERE NAME = '{dbname}'";

                    using (SqlCommand sc = new SqlCommand(xstr, xconn))
                    {
                        sc.CommandType = CommandType.Text;

                        using (SqlDataReader sread = sc.ExecuteReader())
                        {
                            if (sread.HasRows)
                            {
                                interflag = !interflag;
                            }
                        }

                        if (interflag)
                        {
                            using (SqlCommand secondary = new SqlCommand())
                            {
                                secondary.Connection = this.xconn;
                                secondary.CommandText = $"ALTER DATABASE {dbname} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                                secondary.ExecuteNonQuery();
                            }

                            sc.CommandText = $"DROP DATABASE {dbname}";
                            sc.ExecuteNonQuery();
                        }
                    }
                    
                    flag = !flag;
                }
            }
            catch (Exception ex)
            {
                msg.error(ex.Message);
            }
            finally
            {
                this.disconn();
            }

            return flag;
        }

        public bool insertDDL_DML(string ddl)
        {
            bool flag = false;

            try
            {
                if (this.conn(this.interString))
                {
                    using (SqlCommand sqc = new SqlCommand(ddl, this.xconn))
                    {
                        sqc.ExecuteNonQuery();
                    }
                    flag = !flag;
                }
            }
            catch(Exception ex)
            {
                msg.error(ex.Message);
            }
            finally
            {
                this.disconn();
            }
            return flag;
        }
    }
}
