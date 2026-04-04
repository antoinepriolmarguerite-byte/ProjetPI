using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ProjetAutoEcoleS4.Data
{
public class Database
    {
        private string connectionString;

        public Database(string port, string password)
        {
            connectionString = $"server=localhost;port={port};database=AutoEcole;uid=root;pwd={password};";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        //Methode de test
        public string TestConnection()
        {
        try
        {

            var Connexion = new MySqlConnection(connectionString);
            Connexion.Open();
            return "bravo";
            //Console.WriteLine("connextion établie");
        }
        catch (MySqlException e)
        {switch (e.Number)
            {
                case 0:
                    return("Erreur de connexion au serveur");
                    break;
                case 1045:
                    return("Erreur uid/password");
                    break;
                default:
                    return(" ErreurConnexion : " + e.ToString());
                    break;
            }
            return "chepa";
        }
        }
    }
}
