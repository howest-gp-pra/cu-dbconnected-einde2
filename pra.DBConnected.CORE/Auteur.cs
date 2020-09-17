using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Pra.DBConnected.CORE
{
    public class Auteur
    {
        public static DataTable GeefAlleAuteurs()
        {
            string sql;
            sql = "select * from auteur";
            return DBConnector.ExecuteSelect(sql);
        }
        public static DataTable GeefAlleAuteurs(string veldNaam, Enumeraties.SortOrder volgorde)
        {
            string sql;
            sql = "select * from auteur";
            sql += " order by " + veldNaam + " " + volgorde.ToString();
            return DBConnector.ExecuteSelect(sql);
        }
        public static string ZoekNaam(int auteur_id)
        {
            string sql;
            sql = "select * from auteur where auteur_id = " + auteur_id.ToString();
            DataTable dtAuteur = DBConnector.ExecuteSelect(sql);
            if (dtAuteur.Rows.Count > 0)
            {
                return dtAuteur.Rows[0]["naam"].ToString();
            }
            else
                return null;
        }
        public static bool VoegAuteurToe(string nieuweAuteur)
        {
            string sql;
            nieuweAuteur = Helper.HandleQuotes(nieuweAuteur);
            if (nieuweAuteur.Length == 0)
                return false;
            if (nieuweAuteur.Length > 30)
                nieuweAuteur = nieuweAuteur.Substring(0, 30);

            sql = "select max(auteur_id) from auteur";
            int nieuweAuteur_id = int.Parse(DBConnector.ExecuteSelect(sql).Rows[0][0].ToString()) + 1;

            sql = $"insert into auteur (auteur_id, naam) values ({nieuweAuteur_id},'{nieuweAuteur}')";
            return DBConnector.ExecuteCommand(sql);
        }
        public static bool WijzigAuteur(int auteur_id, string naam)
        {
            naam = Helper.HandleQuotes(naam);
            if (naam.Length == 0)
                return false;
            if (naam.Length > 30)
                naam = naam.Substring(0, 30);
            string sql = $"update auteur set naam = '{naam}' where auteur_id = {auteur_id}";
            return DBConnector.ExecuteCommand(sql);
        }
        public static bool VerwijderAuteur(int auteur_id)
        {
            string sql;
            sql = $"select count(*) from boeken where auteur_id = {auteur_id}";
            if (DBConnector.ExecuteSelect(sql).Rows[0][0].ToString() != "0")
                return false;

            sql = $"delete from auteur where auteur_id = {auteur_id}";
            return DBConnector.ExecuteCommand(sql);
        }
    }
}
