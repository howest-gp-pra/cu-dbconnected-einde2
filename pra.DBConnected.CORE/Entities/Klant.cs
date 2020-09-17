using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.DBConnected.CORE.Entities
{
    public class Klant
    {
		private int klant_ID;
		private string klantnaam;
		private string plaats;

		public int Klant_ID
		{
			get { return klant_ID; }
			set { klant_ID = value; }
		}

		public string KlantNaam
		{
			get { return klantnaam; }
			set 
			{
				value = value.Trim();
				if (value.Length == 0)
					throw new Exception("Waarde klantnaam kan niet leeg zijn");
				if (value.Length > 30)
					value = value.Substring(0, 30);
				klantnaam = value; 
			}
		}

		public string Plaats
		{
			get { return plaats; }
			set
			{
				value = value.Trim(); 
				if (value.Length > 30)
					value = value.Substring(0, 30);
				plaats = value;
			}
		}
		public Klant(int klant_ID, string klantNaam, string plaats)
		{
			this.klant_ID = klant_ID;
			KlantNaam = klantNaam;
			Plaats = plaats;
		}
		public override string ToString()
		{
			return $"{klantnaam} - {plaats}";
		}
	}
}
