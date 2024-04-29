using DLLmysql;

//init variables
string db;
string table;

//Etablir une connexion a la base de donnees
connection conn = connec();
//executer le programme pricipale
Main(conn);

//connection
connection connec()
{
    Console.WriteLine("server (localhost)");
    string server = Console.ReadLine();
    Console.WriteLine("identifiant mysql");
    string id = Console.ReadLine();
    Console.WriteLine("mdp de l'utilisateur " + id);
    string pass = Console.ReadLine();
    Console.WriteLine("database a utilisé");
    db = Console.ReadLine();
    connection conn = new connection(server, id, pass, db);
    return conn;
}
//faire le choix de la table et de laction, renvoie l objet requete creer
requete tables(connection conn)
{
    List<string> list_req = new List<string>();
    //afficher les tables
    requete req = new requete(db, conn);
    list_req = req.allTable();
    Console.Clear();
    Console.WriteLine("quelle tables souhaiter vous utilisez ?\n");
    for (int i = 0; i < list_req.Count; i++)
    {
        Console.WriteLine(list_req[i]);
    }

    //choix d'une table
    table = Console.ReadLine();
    requete req_table = new requete(db, table, conn);
    Console.Clear();
    Console.WriteLine("Vous avez choisi la table " + table);

    //affichage choix d'action
    Console.WriteLine("que voulez vous faire ?");
    Console.WriteLine("0 : Select\n1 : Insert\n2 : Update\n3 : Delete\n4 : back");
    return req_table;
}
//CRUD
void select(requete req_table)
{
    //a ameliorer
    Console.WriteLine("vous avez choisi l'option Select");
    //Thread.Sleep(1500);
    Dictionary<string, List<string>> dict = req_table.SELECT();
    foreach (KeyValuePair<string, List<string>> kvp in dict)
    {
        Console.Write(kvp.Key);
        foreach (string valeur in kvp.Value)
        {
            Console.Write("  " + $"{valeur}");
        }
        Console.Write("\n");
    }
}
void insert(requete req_table)
{
    //list des valeurs a ajouter 
    List<string> list_values = new List<string>();
    Console.WriteLine("vous avez choisi l'option Insert");
    Console.WriteLine("que voulez vous ajouter a la table " + table + " ?");
    //donner chaque colonne une par une
    List<string> list_colonnes = req_table.colonne();
    for (int i = 0; i < list_colonnes.Count; i++)
    {
        Console.WriteLine(list_colonnes[i]);
        list_values.Add(Console.ReadLine());
    }
    req_table.InsertDatabase(list_values);
}
void update(requete req_table)
{
    Console.WriteLine("vous avez choisi l'option Update");
    Thread.Sleep(1000);
    Console.WriteLine("quel element souhaiter vous modifier ? (donner son id)");
    int ID = int.Parse(Console.ReadLine());
    req_table.UpdateDatabase(ID);
}
void delete(requete req_table)
{
    Console.WriteLine("vous avez choisi l'option Delete");
    Thread.Sleep(1000);
    req_table.DeleteDatabase();
}
//choix de l action
void choice(requete req_table)
{
    int choix = int.Parse(Console.ReadLine());
    switch (choix)
    {
        case 0:
            select(req_table);
            break;

        case 1:
            insert(req_table);
            break;

        case 2:
            update(req_table);
            break;

        case 3:
            delete(req_table);
            break;

        case 4:
            Console.WriteLine("vous avez choisi l'option back");
            Thread.Sleep(1000);
            Main(conn);
            break;

        default:
            break;
    }
}

//main
void Main(connection conn)
{
    //etablir une connexion
    requete req_table = tables(conn);
    //agir en fonction du choix
    choice(req_table);
    Console.WriteLine("ok.");
}




