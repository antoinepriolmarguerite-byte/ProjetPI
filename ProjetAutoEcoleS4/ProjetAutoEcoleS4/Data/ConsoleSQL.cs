using MySql.Data.MySqlClient;


namespace ProjetAutoEcoleS4.Data
{
public class ConsoleSQL
    {
        private Database conn;

        // ==========================================
        // TYPE       : Constructeur d'INSTANCE
        // ENTRÉE     : string port, string password
        // TRAITEMENT : Initialise la chaîne de connexion MySQL avec les identifiants fournis
        // SORTIE     : aucune (initialise l'objet Database)
        // ==========================================
        public ConsoleSQL(string port, string password)
        {
            conn = new Database(port,password);
        }

        public string RecupererTypeCommande(string sql)
        {
            if(TesterCommande(sql)){
                string debut = sql.Trim().ToUpper();

                if (debut.StartsWith("SELECT") || debut.StartsWith("SHOW") || debut.StartsWith("DESCRIBE"))
                    return "LECTURE";
                
                if (debut.StartsWith("INSERT") || debut.StartsWith("UPDATE") || debut.StartsWith("DELETE"))
                    return "CHANGEMENT";

                if (debut.StartsWith("CREATE") || debut.StartsWith("DROP") || debut.StartsWith("ALTER"))
                    return "CREATION";

                return "INCONNU";
            }
            return "INCONNU";
        }

        public void Executer (string sql, string type)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                {
                    if (type == "LECTURE")
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            // Affichage des entêtes
                            for (int i = 0; i < dr.FieldCount; i++)
                                Console.Write(dr.GetName(i)+ " | ");
                            
                            Console.WriteLine("\n" + new string('-', dr.FieldCount * 15));

                            // Affichage des données
                            while (dr.Read())
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                    Console.Write(dr[i].ToString()!+ " | ");
                                Console.WriteLine();
                            }
                        }
                    }
                    if(type == "INCONNU") Console.WriteLine("La commande est invalide!");
                    else // ACTION ou STRUCTURE
                    {
                        int lignes = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Commande exécutée avec succès. ({lignes} ligne(s) impactée(s))");
                    }
                }
            }
        }

        public bool TesterCommande(string sql)
        {
            try
            {
                using (MySqlConnection cn = conn.GetConnection())
                {
                    cn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Prepare(); // Vérifie la syntaxe auprès du serveur
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"[Erreur de Syntaxe] : {ex.Message}");
                return false;
            }
        }
    }
}