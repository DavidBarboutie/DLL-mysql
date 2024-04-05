using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DLLmysql
{
    class connection
    {
        private string server;
        private string identifiant;
        private string pwd;
        private string BDD;

        public connection() { }

        public connection(string server0, string identifiant0, string pwd0, string BDD0)
        {
            server = server0;
            identifiant = identifiant0;
            pwd = pwd0;
            BDD = BDD0;
        }

        public string get_server() { return server; }
        public string get_identifiant() { return identifiant; }
        public string get_pwd() { return pwd; }
        public string get_BDD() { return BDD; }

        public void set_server(string SERVER) { this.server = SERVER; }
        public void set_identifiant(string IDENTIFIANT) { this.identifiant = IDENTIFIANT; }
        public void set_pwd(string PWD) { this.pwd = PWD; }
        public void set_BDD(string BDD) { this.BDD = BDD; }

        //connexoin a la base de donnees 
        private MySqlConnection maconnexion()
        {
            string chaineConnexion = "Server=" + get_server() + ";uid=" + get_identifiant() + ";pwd=" + get_pwd() + ";Database=" + get_BDD();
            MySqlConnection maconnexion = new MySqlConnection(chaineConnexion);
            return maconnexion;
        }
        public void OPEN()
        {
            maconnexion().Open();
        }
        public void CLOSE()
        {
            maconnexion().Close();
        }
    }
}

