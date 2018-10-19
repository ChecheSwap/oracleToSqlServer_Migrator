using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Proxim.LOGIC.GENERICS;
using System.Data.Common;
using Proxim.LOGIC.TYPES;
using System.Data;
using Proxim.LOGIC.MSSQL_DATA_BASE;

namespace Proxim.LOGIC.ORACLE_DATA_BASE
{
    public class oracleDB
    {
        private msSqlDB sqlserverDB = default(msSqlDB);
        public oracleDB(msSqlDB sqlserverDB)
        {
            this.sqlserverDB = sqlserverDB;
        }
        private static string HOST = "localhost";
        private static int PORT = 1521;
        private static string SID = "xe";
        private static string USER = regOra.CR_USR;
        private static string PASSWORD = regOra.CR_PASSWORD;

        private static OracleConnection xconn = default(OracleConnection);
        private static string myString = default(string);

        private static OracleConnection getODBConnection(string host, int port, String sid, String user, String password)
        {
            myString = $"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {host})(PORT = {port}))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = {sid})));Password={password};User ID={user}";

            xconn = new OracleConnection();

            xconn.ConnectionString = myString;

            return xconn;
        }

        private bool connect()
        {
            bool flag = false;

            try
            {
                xconn = getODBConnection(HOST, PORT, SID, USER, PASSWORD);
                xconn.Open();
                flag = !flag;
            }
            catch (Exception ex)
            {
                msg.error(ex.Message);
            }

            return flag;
        }

        private bool disconn()
        {
            bool flag = false;

            if (xconn.State == ConnectionState.Open)
            {
                try
                {
                    xconn.Close();
                }
                catch (Exception ex)
                {
                    msg.error(ex.Message);
                }
            }

            return flag;
        }

        //-----------------------------------------------------------------------------------------------------------

        public bool insertViews()
        {
            bool flag = false;

            bool innerflag = false;

            if (this.connect())
            {
                try
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.CommandText = SQLQ.GET_DDLVIEWS;
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.Connection = xconn;

                        using (OracleParameter op = new OracleParameter())
                        {
                            op.OracleDbType = OracleDbType.RefCursor;
                            op.Direction = ParameterDirection.Output;
                            oc.Parameters.Add(op);

                            using (OracleDataReader xreader = oc.ExecuteReader())
                            {
                                if (xreader.HasRows)
                                {
                                    while (xreader.Read())
                                    {
                                        if (!this.sqlserverDB.insertDDL_DML(xreader.GetValue(0).ToString()))
                                        {
                                            msg.danger("Inconsistencia en Sql Vistas, se cancela insercion  de vistas a priori.");
                                            innerflag = true;
                                            break;
                                        }
                                    }

                                    if (!innerflag)
                                    {
                                        flag = !flag;
                                    }
                                }
                            }

                        }
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
            }

            return flag;
        }
        public bool insertRefConst()
        {
            bool flag = false;
            bool innerflag = false;
            if (this.connect())
            {
                try
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.Connection = xconn;
                        oc.CommandText = SQLQ.GET_REFERENCIALS;
                        oc.CommandType = CommandType.StoredProcedure;

                        using (OracleParameter op = new OracleParameter())
                        {
                            op.Direction = ParameterDirection.Output;
                            op.OracleDbType = OracleDbType.RefCursor;
                            oc.Parameters.Add(op);

                            using (OracleDataAdapter oda = new OracleDataAdapter(oc))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    oda.Fill(ds);

                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        if(!this.sqlserverDB.insertDDL_DML(dr[0].ToString()))
                                        {
                                            msg.danger("Inconsistencia en Restricciones referenciales, se cancela insercion a priori.");
                                            innerflag = true;
                                            break;
                                        }
                                    }
                                    if (!innerflag)
                                    {
                                        flag = !flag;
                                    }
                                }
                            }
                        }
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
            }

            return flag;
        }
        public bool insertDML()
        {
            bool flag = false;
            bool innerflag = false;

            if (this.connect())
            {
                try
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.Connection = xconn;
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.CommandText = SQLQ.GET_DML;

                        OracleParameter op = new OracleParameter();
                        op.OracleDbType = OracleDbType.RefCursor;
                        op.Direction = ParameterDirection.Output;
                        oc.Parameters.Add(op);

                        using (OracleDataReader xreader = oc.ExecuteReader())
                        {
                            if (xreader.HasRows)
                            {                                
                                while (xreader.Read())
                                {
                                    if (!this.sqlserverDB.insertDDL_DML(xreader.GetValue(0).ToString()))
                                    {
                                        innerflag = !innerflag;
                                        msg.danger("Inconsistencia en DML, se procede a cancelar Operacion a priori");
                                        break;
                                    }
                                }
                            }
                            if (!innerflag)
                            {
                                flag = !flag;
                            }
                        }

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
            }
            return flag;
        }
        public bool insertDDL()
        {
            bool flag = false;
            bool innerflag = false;

            if (this.connect())
            {
                try
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.Connection = xconn;
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.CommandText = SQLQ.GET_DDL;

                        OracleParameter op = new OracleParameter();
                        op.OracleDbType = OracleDbType.RefCursor;
                        op.Direction = ParameterDirection.Output;
                        oc.Parameters.Add(op);

                        using (OracleDataReader xreader = oc.ExecuteReader())
                        {
                            if (xreader.HasRows)
                            {
                                while (xreader.Read())
                                {
                                    if (!this.sqlserverDB.insertDDL_DML(xreader.GetValue(0).ToString().Trim()))
                                    {
                                        msg.danger("Inconsistencia en DDL, se procede a cancelar operacion a priori");
                                        innerflag = !innerflag;
                                        break;
                                    }
                                }
                                if (!innerflag)
                                {
                                    flag = !flag;
                                }
                            }
                        }
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
            }
            return flag;
        }

        public bool onInit(string usr)
        {            
            bool flag = false;
            try
            {
                if (this.connect())
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.Connection = xconn;
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.CommandText = SQLQ.EXEC_LOGIC;

                        OracleParameter oraParam = new OracleParameter();
                        oraParam.OracleDbType = OracleDbType.Varchar2;
                        oraParam.Direction = ParameterDirection.Input;
                        oraParam.Value = usr;
                        oc.Parameters.Add(oraParam);
                        
                        oc.ExecuteNonQuery();
                        flag = !flag;
                    }
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

        public bool onSecond()
        {
            bool flag = false;


            try
            {
                if (this.connect())
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.Connection = xconn;
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.CommandText = SQLQ.EXEC_PARSING;

                        oc.ExecuteNonQuery();
                        flag = !flag;
                    }
                }
            }
            catch(Exception ex)
            {
                msg.error(ex.Message);
            }
            finally{
                this.disconn();
            }

            return flag;
        }
        
        public List<userNames> getUsrnames()
        {
            List<userNames> xlist = new List<userNames>() { };
            try
            {
                if (this.connect())
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.Connection = xconn;
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.CommandText = SQLQ.GET_USERS;

                        OracleParameter oraParam = new OracleParameter();
                        oraParam.OracleDbType = OracleDbType.RefCursor;
                        oraParam.Direction = ParameterDirection.Output;

                        oc.Parameters.Add(oraParam);

                        using (OracleDataReader xreader = oc.ExecuteReader())
                        {
                            if (xreader.HasRows)
                            {
                                while (xreader.Read())
                                {
                                    for (int x = 0; x < xreader.FieldCount; ++x)
                                    {
                                        xlist.Add(new userNames(xreader.GetValue(x).ToString()));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg.error(ex.Message);
            }
            finally
            {
                disconn();
            }

            return xlist;
        }

        public List<userNames> getTablesofUser(string usr)
        {
            List<userNames> xlist = new List<userNames>();

            try
            {
                this.connect();

                using (OracleCommand oc = new OracleCommand())
                {
                    oc.Connection = xconn;
                    oc.CommandType = CommandType.StoredProcedure;
                    oc.CommandText = SQLQ.GET_TABLES_USER;

                    OracleParameter oraParam = new OracleParameter();
                    oraParam.OracleDbType = OracleDbType.RefCursor;
                    oraParam.Direction = ParameterDirection.Output;

                    OracleParameter oraParam2 = new OracleParameter();
                    oraParam2.OracleDbType = OracleDbType.Varchar2;
                    oraParam2.Direction = ParameterDirection.Input;
                    oraParam2.Value = usr;
                    oraParam2.ParameterName = "USRNAME";

                    oc.Parameters.Add(oraParam);
                    oc.Parameters.Add(oraParam2);

                    using (OracleDataAdapter oda = new OracleDataAdapter(oc))
                    {
                        using (DataSet ds = new DataSet())
                        {

                            oda.Fill(ds);

                            foreach (DataRow t in ds.Tables[0].Rows)
                            {
                                foreach (DataColumn dc in ds.Tables[0].Columns)
                                {
                                    xlist.Add(new userNames(t[dc].ToString()));
                                }
                            }
                        }
                    }
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

            return xlist;
        }

        public List<string> getViewsNames(string usr)
        {
            List<string> xlist = new List<string>();

            if (this.connect())
            {
                try
                {
                    using (OracleCommand oc = new OracleCommand())
                    {
                        oc.Connection = xconn;
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.CommandText = SQLQ.GET_VIEWS_USER;

                        OracleParameter oraParam = new OracleParameter();
                        oraParam.OracleDbType = OracleDbType.RefCursor;
                        oraParam.Direction = ParameterDirection.Output;

                        OracleParameter oraParam2 = new OracleParameter();
                        oraParam2.OracleDbType = OracleDbType.Varchar2;
                        oraParam2.Direction = ParameterDirection.Input;
                        oraParam2.Value = usr;
                        oraParam2.ParameterName = "USRNAME";

                        oc.Parameters.Add(oraParam);
                        oc.Parameters.Add(oraParam2);

                        using (OracleDataAdapter oda = new OracleDataAdapter(oc))
                        {
                            using (DataSet ds = new DataSet())
                            {

                                oda.Fill(ds);

                                foreach (DataRow t in ds.Tables[0].Rows)
                                {
                                    foreach (DataColumn dc in ds.Tables[0].Columns)
                                    {
                                        xlist.Add(t[dc].ToString());
                                    }
                                }
                            }
                        }
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
            }

            return xlist;
        }
    }

}