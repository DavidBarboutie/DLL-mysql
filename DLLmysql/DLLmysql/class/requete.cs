using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DLLmysql
{
    internal class requete
    {
        public string DB;
        private string tables;
        private MySqlConnection maconnexion;

        public requete() { }

        public requete(string DB0, string tables0, MySqlConnection maconnexion0)
        {
            this.DB = DB0;
            this.tables = tables0;
            this.maconnexion = maconnexion0;
        }

        //getters
        public string get_DB() {  return this.DB; }
        public string get_tables() { return this.tables; }
        public MySqlConnection get_maconnexion() { return this.maconnexion; }

        //setters
        public void set_DB(string DB1) {  this.DB = DB1; }
        public void set_tables(string tables1) { this.tables = tables1; }
        public void set_maconnexion(MySqlConnection maconnexion1) { this.maconnexion = maconnexion1; }

        //recuperation des element de la table
        public void colonne()
        {
            //nb colonne
            MySqlConnection info_s = new MySqlConnection("Server = localhost; uid = user; pwd = caribou; Database = " + get_DB());
            info_s.Open();
            string req_nb_col = "select count(*) from information_schema.columns where table_schema = " + get_DB() + " and table_name=" + get_tables();
            int id_max = 0;
            MySqlCommand cmd = new MySqlCommand(req_nb_col, info_s);
            MySqlDataReader reader_requete = cmd.ExecuteReader();
            info_s.Close();
        }
        //create
        public void CREATE()
        {
            MySqlConnection conn = get_maconnexion();
            conn.Open();
            string req = "INSERT INTO " + get_tables() +""; //values
            conn.Close();
        }
        //retrieve
        public void RETRIEVE(int Nrequete)
        {
            MySqlConnection conn = get_maconnexion();
            conn.Open();
            string req = "SELECT * from requete WHERE requete = " + Nrequete;
            MySqlCommand cmd = new MySqlCommand(req, conn);

            MySqlDataReader reader_requete = cmd.ExecuteReader();

            reader_requete.Read();
            try
            {
                // this.set_raison_sociale(reader_requete["Raison_sociale"].ToString());

            }
            catch (Exception e)
            {
                Console.Write("oops\n" + e);
            }
            conn.Close();
        }

        //update
        public void UPDATE(int ID) {
            MySqlConnection conn = get_maconnexion();
            conn.Open();
            string req = "UPDATE "+ get_tables() +" where id=" + ID;
            MySqlCommand cmd = new MySqlCommand(req, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //delete
        public void DELETE()
        {
            MySqlConnection conn = get_maconnexion();
            conn.Open();
            conn.Close();
        }
        //alltable
        public List<requete> FIND_ALL()
        {
            //ouvrir la connexion a la BDD
            MySqlConnection conn = get_maconnexion();
            conn.Open();

            //creer une liste qui stockera les donnees
            List<requete> list_requete = new List<requete>();

            //recuprer les donnees de la table entierement
            string req = "select * from requete";

            //execute la commande et ajoute a la liste
            MySqlCommand stmt_requete_liste = new MySqlCommand(req, conn);
            using (MySqlDataReader reader_requete = stmt_requete_liste.ExecuteReader())
            {
                while (reader_requete.Read())
                {
                    //requete requete = new requete(reader_requete["raison_sociale"].ToString(), reader_requete["adresse"].ToString(), reader_requete["code_postal"].ToString(), reader_requete["ville"].ToString(), int.Parse(reader_requete["qtt_vendu_de_radiateur"].ToString()), int.Parse(reader_requete["Numero_telephone"].ToString()), int.Parse(reader_requete["numero_fax"].ToString()), reader_requete["adresse_mail"].ToString(), int.Parse(reader_requete["Nrequete"].ToString()));
                    //list_requete.Add(requete);
                }
            }

            //fermeture de la connexion a la BDD
            conn.Close();
            return list_requete;
        }
    }
}
