using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Pra.DBConnected.CORE
{
    public class DBConnector
    {
        public static DataTable ExecuteSelect(string sqlInstructie)
        {
            string constring = Helper.GetConnectionString();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlInstructie, constring);
            try
            {
                da.Fill(ds);
            }
            catch (Exception fout)
            {
                string foutmelding = fout.Message;
                return null;
            }
            return ds.Tables[0];
        }
        public static bool ExecuteCommand(string sqlInstructie)
        {
            string constring = Helper.GetConnectionString();
            SqlConnection mijnVerbinding = new SqlConnection(constring);
            SqlCommand mijnOpdracht = new SqlCommand(sqlInstructie, mijnVerbinding);
            try
            {
                mijnOpdracht.Connection.Open();
                mijnOpdracht.ExecuteNonQuery();
                mijnVerbinding.Close();
                return true;
            }
            catch (Exception fout)
            {
                string foutmelding = fout.Message;
                return false;
            }
        }
        public static bool ExecuteSP(string SPNaam, SqlParameter[] parameters)
        {
            string constring = Helper.GetConnectionString();
            SqlConnection mijnVerbinding = new SqlConnection(constring);
            SqlCommand mijnOpdracht = new SqlCommand(SPNaam, mijnVerbinding);
            mijnOpdracht.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                    mijnOpdracht.Parameters.Add(p);
            }
            try
            {
                mijnOpdracht.Connection.Open();
                mijnOpdracht.ExecuteNonQuery();
                mijnVerbinding.Close();
                return true;

            }
            catch (Exception fout)
            {
                string foutmelding = fout.Message;
                return false;
            }

        }
        public static DataTable ExecuteSPWithDataTable(string SPNaam, SqlParameter[] parameters)
        {
            string constring = Helper.GetConnectionString();
            SqlConnection mijnVerbinding = new SqlConnection(constring);
            SqlCommand mijnOpdracht = new SqlCommand(SPNaam, mijnVerbinding);
            SqlDataReader rdr;
            mijnOpdracht.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                    mijnOpdracht.Parameters.Add(p);
            }
            try
            {
                mijnOpdracht.Connection.Open();
                rdr = mijnOpdracht.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                mijnVerbinding.Close();
                return dt;

            }
            catch (Exception fout)
            {
                string foutmelding = fout.Message;
                return null;
            }

        }
        public static string ExecuteScalaire(string sqlScalaireInstructie)
        {
            string constring = Helper.GetConnectionString();
            SqlConnection mijnVerbinding = new SqlConnection(constring);
            SqlCommand mijnOpdracht = new SqlCommand(sqlScalaireInstructie, mijnVerbinding);
            mijnVerbinding.Open();
            string retour = mijnOpdracht.ExecuteScalar().ToString();
            mijnVerbinding.Close();
            return retour;
        }
    }
}
