using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Pra.DBConnected.CORE
{
    public class Uitgever
    {
        public static DataTable GeefAlleUitgevers()
        {
            string sp = "pra_UitgeverSelectAllAsc";
            return DBConnector.ExecuteSPWithDataTable(sp, null);
        }
        public static string ZoekUitgever(int uitg_id)
        {
            string sp = "pra_UitgeverFind";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter();
            param[0].ParameterName = "@id";
            param[0].Value = uitg_id;
            DataTable dt = DBConnector.ExecuteSPWithDataTable(sp, param);
            if (dt != null)
                return dt.Rows[0][0].ToString();
            else
                return null;
        }
        public static bool VoegUitgeverToe(string nieuweUitgever)
        {
            nieuweUitgever = Helper.HandleQuotes(nieuweUitgever);
            if (nieuweUitgever.Length == 0)
                return false;
            if (nieuweUitgever.Length > 30)
                nieuweUitgever = nieuweUitgever.Substring(0, 30);

            string sql = "select max(uitg_id) from uitgever";
            int nieuweUitg_id = int.Parse(DBConnector.ExecuteScalaire(sql)) + 1;

            string sp = "pra_UitgeverNew";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter();
            param[1] = new SqlParameter();
            param[0].ParameterName = "@uitg_id";
            param[0].Value = nieuweUitg_id;
            param[1].ParameterName = "@uitgever";
            param[1].Value = nieuweUitgever;
            return DBConnector.ExecuteSP(sp, param);
        }
        public static bool WijzigUitgever(int uitg_id, string uitgever)
        {
            uitgever = Helper.HandleQuotes(uitgever);
            if (uitgever.Length == 0)
                return false;
            if (uitgever.Length > 30)
                uitgever = uitgever.Substring(0, 30);

            string sp = "pra_UitgeverEdit";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter();
            param[1] = new SqlParameter();
            param[0].ParameterName = "@uitg_id";
            param[0].Value = uitg_id;
            param[1].ParameterName = "@uitgever";
            param[1].Value = uitgever;
            return DBConnector.ExecuteSP(sp, param);
        }
        public static bool VerwijderUitgever(int uitg_id)
        {
            string sp = "pra_UitgeverDelete";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter();
            param[0].ParameterName = "@uitg_id";
            param[0].Value = uitg_id;
            return DBConnector.ExecuteSP(sp, param);

        }
    }
}
