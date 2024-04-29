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
        private MySqlConnection conn;

        public connection() { }

        //connection avec mdp
        public connection(string server0, string identifiant0, string pwd0, string BDD0)
        {
            server = server0;
            identifiant = identifiant0;
            pwd = pwd0;
            BDD = BDD0;
            conn = new MySqlConnection("Server=" + server0 + ";uid=" + identifiant0 + ";pwd=" + pwd0 + ";Database=" + BDD0);
        }

        //getters (recuperation des donnees)
        public string get_server() { return server; }
        public string get_identifiant() { return identifiant; }
        public string get_pwd() { return pwd; }
        public string get_BDD() { return BDD; }
        public MySqlConnection get_conn() { return conn; }

        //setters (modification des donnees)
        public void set_server(string SERVER) { this.server = SERVER; }
        public void set_identifiant(string IDENTIFIANT) { this.identifiant = IDENTIFIANT; }
        public void set_pwd(string PWD) { this.pwd = PWD; }
        public void set_BDD(string BDD) { this.BDD = BDD; }

        //ouverture de la base de donnees
        public void OPEN()
        {
            get_conn().Open();
        }

        //fermeture de la base de donnees
        public void CLOSE()
        {
            get_conn().Close();
        }
    }
}

