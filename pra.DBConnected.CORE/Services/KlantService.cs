using Pra.DBConnected.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Pra.DBConnected.CORE;
using System.Linq;

namespace Pra.DBConnected.CORE.Services
{
    public class KlantService
    {
		private List<Klant> klanten;

		public List<Klant> Klanten
		{
			get { return klanten; }
		}

		public KlantService()
		{
			ReadAllRecords();
		}

		private void ReadAllRecords()
		{
			klanten = new List<Klant>();

			string sql;
			sql = "select * from klant order by klantnaam";
			DataTable dtKlant = DBConnector.ExecuteSelect(sql);
			foreach(DataRow rw in dtKlant.Rows)
			{
				klanten.Add(new Klant(int.Parse(rw[0].ToString()), rw[1].ToString(), rw[2].ToString()));
			}
		}

		public bool AddKlant(Klant klant)
		{
			string sql;
			sql = "insert into klant(klant_id, klantnaam, plaats) values (";
			sql += GetNextID().ToString() + " , ";
			sql += "'" + Helper.HandleQuotes(klant.KlantNaam) + "' , ";
			sql += "'" + Helper.HandleQuotes(klant.Plaats) + "' )";
			if (DBConnector.ExecuteCommand(sql))
			{
				// nieuwe klant aan de List toevoegen
				klanten.Add(klant);
				// sorteer de List opnieuw op klantnaam
				klanten = klanten.OrderBy(sorteerklant => sorteerklant.KlantNaam).ToList();

				// alternatief voor 2 instructies hierboven : methode ReadAllRecords aanroepen
				return true;
			}
			else
			{
				return false;
			}
		}
		private int GetNextID()
		{
			string sql = "select max(klant_id) from klant";
			return int.Parse(DBConnector.ExecuteScalaire(sql)) + 1;
		}

		public bool EditKlant(Klant klant)
		{
			string sql;
			sql = "update klant ";
			sql += " set klantnaam = '" + Helper.HandleQuotes(klant.KlantNaam) + "' , ";
			sql += " plaats = '" + Helper.HandleQuotes(klant.Plaats) + "' ";
			sql += " where klant_id = " + klant.Klant_ID.ToString();
			if (DBConnector.ExecuteCommand(sql))
			{

				// sorteer de List opnieuw op klantnaam
				klanten = klanten.OrderBy(sorteerklant => sorteerklant.KlantNaam).ToList();

				// alternatief voor code hierboven : methode ReadAllRecords aanroepen
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeleteKlant(Klant klant)
		{
			string sql;
			sql = "select count(*) from uitleen where klant_id = " + klant.Klant_ID;
			if (int.Parse(DBConnector.ExecuteScalaire(sql)) != 0)
				return false;
			sql = "delete from klant where klant_id = " + klant.Klant_ID;
			if (DBConnector.ExecuteCommand(sql))
			{
				// klant ook uit list verwijderen
				klanten.Remove(klant);
				// List opnieuw sorteren hoeft hier niet
				// alternatief : methode ReadAllRecords aanroepen
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
