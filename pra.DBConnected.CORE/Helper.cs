using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.DBConnected.CORE
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            return @"Data Source=(local)\SQLEXPRESS;Initial Catalog=Bibliotheek; Integrated security=true;";
        }
        public static string HandleQuotes(string waarde)
        {
            return waarde.Trim().Replace("'", "''");
        }
    }
}
