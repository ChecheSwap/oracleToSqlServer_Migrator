using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxim.LOGIC.ORACLE_DATA_BASE
{
    public static class SQLQ
    {
        public static string GET_USERS = "USR_EXPORT.SP_GET_USERNAME";
        public static string GET_TABLES_USER = "USR_EXPORT.SP_GET_TABLE_LIST";
        public static string GET_VIEWS_USER = "USR_EXPORT.SP_GET_VIEW_LIST";
        public static string EXEC_LOGIC = "USR_EXPORT.SP_FIRST";        
        public static string EXEC_PARSING = "USR_EXPORT.SP_SECOND";
        public static string GET_DDL = "USR_EXPORT.GET_DDL";
        public static string GET_DML = "USR_EXPORT.GET_DML";
        public static string GET_REFERENCIALS = "USR_EXPORT.GET_REFCONS";
        public static string GET_DDLVIEWS = "USR_EXPORT.GET_DDLVIEWS";
    }
}
