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
        private connection maconnexion;

        //premier constructeur permmettant de recuperer le nom des tables
        public requete(string DB0, connection maconnexion0)
        {
            this.DB = DB0;
            this.maconnexion = maconnexion0;
        }
        //second constructeur permettant de faire des requetes
        public requete(string DB0, string table0, connection maconnexion0)
        {
            this.DB = DB0;
            this.tables = table0;
            this.maconnexion = maconnexion0;
        }

        //getters
        public string get_DB() { return this.DB; }
        public string get_tables() { return this.tables; }
        public connection get_maconnexion() { return this.maconnexion; }

        //setters
        public void set_DB(string DB1) { this.DB = DB1; }
        public void set_tables(string tables1) { this.tables = tables1; }
        public void set_maconnexion(connection maconnexion1) { this.maconnexion = maconnexion1; }


        //recuperation des element de la table
        public List<string> colonne()
        {
            connection conn = get_maconnexion();
            conn.OPEN();
            //list des colonnes a renvoyer
            List<string> list_colonne = new List<string>();

            //recuperation de toutes les colonnes
            string req = " SELECT COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_NAME  ='" + get_tables() + "' and table_schema = '" + get_DB() + "' order by ordinal_position";
            MySqlCommand cmd = new MySqlCommand(req, conn.get_conn());
            using (MySqlDataReader reader_requete = cmd.ExecuteReader())
            {
                while (reader_requete.Read())
                {
                    string nom_colonne = reader_requete["COLUMN_NAME"].ToString();
                    list_colonne.Add(nom_colonne);
                }
            }
            conn.CLOSE();
            return list_colonne;
        }

        public List<string> elem_table()
        {
            //list des elements a renvoyer
            List<string> list_elem = new List<string>();
            connection conn = get_maconnexion();
            conn.OPEN();

            //recuperation de toutes les colonnes
            string req = " SELECT COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_NAME  ='" + get_tables() + "' and table_schema = '" + get_DB() + "'";
            MySqlCommand cmd = new MySqlCommand(req, conn.get_conn());
            using (MySqlDataReader reader_requete = cmd.ExecuteReader())
            {
                while (reader_requete.Read())
                {
                    string nom_elem = reader_requete["COLUMN_NAME"].ToString();
                    list_elem.Add(nom_elem);
                }
            }
            conn.CLOSE();
            return list_elem;
        }


        //retrieve
        public Dictionary<string, List<string>> SELECT()
        {
            List<string> colonnes = colonne();
            connection conn = get_maconnexion();
            conn.OPEN();
            string req = "SELECT * from " + get_tables();
            MySqlCommand cmd = new MySqlCommand(req, conn.get_conn());
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            //remplir le dictionnaire
            try
            {
                for (int i = 0; i < colonnes.Count; i++)
                {
                    //remplir la list d'element
                    string req_elem = "select " + colonnes[i] + " from " + get_tables();
                    MySqlCommand cmd_elem = new MySqlCommand(req_elem, conn.get_conn());
                    Thread.Sleep(6000);
                    using (MySqlDataReader reader_elem = cmd_elem.ExecuteReader())
                    {
                        List<string> elem = new List<string>();
                        while (reader_elem.Read())
                        {
                            string element = reader_elem[colonnes[i]].ToString();
                            elem.Add(element);
                        }
                        dict.Add(colonnes[i], elem);


                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            conn.CLOSE();
            return dict;
        }


        //alltable
        public List<string> allTable()
        {
            //ouvrir la connexion a la BDD
            connection conn = get_maconnexion();
            conn.OPEN();

            //creer une liste qui stockera les donnees
            List<string> list_tables = new List<string>();

            //recuprer les donnees de la table entierement
            string req = "show tables";

            //execute la commande et ajoute a la liste
            MySqlCommand stmt_requete_liste = new MySqlCommand(req, conn.get_conn());
            using (MySqlDataReader reader_requete = stmt_requete_liste.ExecuteReader())
            {
                while (reader_requete.Read())
                {

                    string nom_table = reader_requete["Tables_in_" + get_DB()].ToString();
                    list_tables.Add(nom_table);
                }
            }

            //fermeture de la connexion a la BDD
            conn.CLOSE();
            return list_tables;
        }


        //create
        public void InsertDatabase(List<string> values)
        {
            connection conn = get_maconnexion();
            conn.OPEN();
            string req = "INSERT INTO " + get_tables() + " VALUES ('"; //values
            for (int i = 0; i < values.Count; i++)
            {
                if (i < values.Count - 1)
                {
                    req += values[i] + "', '";
                }
                else
                {
                    req += values[i];
                }
            }
            req += "')";
            Console.WriteLine(req);
            MySqlCommand cmd = new MySqlCommand(req, conn.get_conn());
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            conn.CLOSE();
        }


        //delete
        public void DeleteDatabase()
        {
            connection conn = get_maconnexion();
            Console.WriteLine("quelle colonne sera le sujet de la condition de suppression ? (colonne)");
            string colonne_condition = Console.ReadLine();
            Console.WriteLine("quelle condition voulez vous appliquer ? (quel est la valeur que doit avoir " + colonne_condition + " dans la suppression ?)");
            string condition = Console.ReadLine();
            conn.OPEN();
            string req = "delete from " + get_tables() + " where " + colonne_condition + " = '" + condition + "'";
            Console.WriteLine(req);
            MySqlCommand cmd = new MySqlCommand(req, conn.get_conn());
            cmd.ExecuteNonQuery();
            conn.CLOSE();
        }


        //update
        public void UpdateDatabase(int ID)
        {
            connection conn = get_maconnexion();
            Console.WriteLine("quelle colonne voulez vous modifier ?");
            string val = Console.ReadLine();
            Console.WriteLine("par quoi voulez vous remplacez cette valeure ?");
            string new_val = Console.ReadLine();
            conn.OPEN();
            string req = "UPDATE " + get_tables() + " set " + val + " = '" + new_val + "' where id=" + ID;
            Console.WriteLine(req);
            MySqlCommand cmd = new MySqlCommand(req, conn.get_conn());
            cmd.ExecuteNonQuery();
            conn.CLOSE();
        }
    }
}
