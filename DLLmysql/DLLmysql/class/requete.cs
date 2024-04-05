using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLmysql
{
    internal class requete
    {
        private string tables;
        private MySqlConnection maconnexion;

        public requete() { }

        public requete(string tables0, MySqlConnection maconnexion0)
        {
            this.tables = tables0;
            this.maconnexion = maconnexion0;
        }

        public string get_tables() { return this.tables; }
        public MySqlConnection get_maconnexion() { return this.maconnexion; }
        public void set_tables(string TABLES) { this.tables = TABLES; }
        public void set_maconnexion(MySqlConnection maconnexion1) { this.maconnexion = maconnexion1; }

        //create

        //read
        public void RETRIEVE(int NRevendeur)
        {
            MySqlConnection conn = get_maconnexion();
            conn.Open();
            string req = "SELECT * from revendeur WHERE NRevendeur = " + NRevendeur;
            MySqlCommand cmd = new MySqlCommand(req, BDD.maconnexion);

            MySqlDataReader reader_revendeur = cmd.ExecuteReader();

            reader_revendeur.Read();
            try
            {
                // this.set_raison_sociale(reader_revendeur["Raison_sociale"].ToString());

            }
            catch (Exception e)
            {
                Console.Write("oops\n" + e);
            }
            conn.Close();
        }

    }

    //update
    //delete
    //alltable
    public List<revendeur> FIND_ALL()
    {
        //ouvrir la connexion a la BDD
        BDD.maconnexion.Open();

        //creer une liste qui stockera les donnees
        List<revendeur> list_revendeur = new List<revendeur>();

        //recuprer les donnees de la table entierement
        string req = "select * from revendeur";

        //execute la commande et ajoute a la liste
        MySqlCommand stmt_revendeur_liste = new MySqlCommand(req, BDD.maconnexion);
        using (MySqlDataReader reader_revendeur = stmt_revendeur_liste.ExecuteReader())
        {
            while (reader_revendeur.Read())
            {
                revendeur Revendeur = new revendeur(reader_revendeur["raison_sociale"].ToString(), reader_revendeur["adresse"].ToString(), reader_revendeur["code_postal"].ToString(), reader_revendeur["ville"].ToString(), int.Parse(reader_revendeur["qtt_vendu_de_radiateur"].ToString()), int.Parse(reader_revendeur["Numero_telephone"].ToString()), int.Parse(reader_revendeur["numero_fax"].ToString()), reader_revendeur["adresse_mail"].ToString(), int.Parse(reader_revendeur["NRevendeur"].ToString()));
                list_revendeur.Add(Revendeur);
            }
        }

        //fermeture de la connexion a la BDD
        BDD.maconnexion.Close();
        return list_revendeur;
    }
}
}
