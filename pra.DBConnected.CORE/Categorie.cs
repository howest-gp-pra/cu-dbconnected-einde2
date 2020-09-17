using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Pra.DBConnected.CORE
{
    public class Categorie
    {
        public static string ZoekCategorie(int cat_id)
        {
            string sql;
            sql = "select * from categorie where cat_id = " + cat_id.ToString();
            DataTable dtCategorie = DBConnector.ExecuteSelect(sql);
            if (dtCategorie.Rows.Count > 0)
                return dtCategorie.Rows[0]["categorie"].ToString();
            else
                return null;
        }
        public static bool VoegCategorieToe(string nieuweCategorie)
        {
            string sql;
            nieuweCategorie = Helper.HandleQuotes(nieuweCategorie);
            if (nieuweCategorie.Length == 0)
                return false;
            if (nieuweCategorie.Length > 30)
                nieuweCategorie = nieuweCategorie.Substring(0, 30);

            sql = "select max(cat_id) from categorie";
            int nieuwecat_id = int.Parse(DBConnector.ExecuteScalaire(sql)) + 1;

            sql = $"insert into categorie (cat_id, categorie) values ({nieuwecat_id},'{nieuweCategorie}')";
            return DBConnector.ExecuteCommand(sql);
        }
        public static bool WijzigCategorie(int cat_id, string categorie)
        {
            categorie = Helper.HandleQuotes(categorie);
            if (categorie.Length == 0)
                return false;
            if (categorie.Length > 30)
                categorie = categorie.Substring(0, 30);
            string sql = $"update categorie set categorie = '{categorie}' where cat_id = {cat_id}";
            return DBConnector.ExecuteCommand(sql);
        }
        public static bool VerwijderCategorie(int cat_id)
        {
            string sql;
            sql = $"select count(*) from boeken where cat_id = {cat_id}";
            if (DBConnector.ExecuteScalaire(sql) != "0")
                return false;

            sql = $"delete from categorie where cat_id = {cat_id}";
            return DBConnector.ExecuteCommand(sql);
        }
    }
}
