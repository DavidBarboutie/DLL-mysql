using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DLLmysql
{
    internal class BDD
    {
        //connexoin a la base de donnees infrarad
        private static string chaineConnexion = "Server=localhost;uid=user;pwd=caribou;Database=infrarad";
        public static MySqlConnection maconnexion = new MySqlConnection(chaineConnexion);
    }
}
