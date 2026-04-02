using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAutoEcoleS4.Models;

namespace ProjetAutoEcoleS4.Data
{
    internal class MoniteurDAO
    {
        private Database conn;

        public MoniteurDAO(string port, string password) //Bah pourquoi elle marche pas ? Bah c'est le constructeur guignol
        {
            conn = new Database(port, password);
        }

         public List<Moniteur> GetAll(string port,string password)
        {
            Database conn = new Database(port,password); 
            List<Moniteur> liste = new List<Moniteur>();
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM MONITEUR", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    liste.Add(new Moniteur
                    {
                        id_Moniteur = dr.GetString("id_moniteur"),
                        nom = dr.GetString("nom"),
                        prenom = dr.GetString("prenom"),
                        //DateNaissance = dr.GetDateTime("date_naissance"),
                        //Tel = dr.GetString("telephone")
                    });
                }
            }
            return liste;
        }
    }
}
