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

        public MoniteurDAO(string port, string password)
        {
            conn = new Database(port, password);
        }

        public List<Moniteur> GetAll()
        {
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
                        id_moniteur = dr.GetInt32("ID_Moniteur"), 
                        nom = dr.GetString("Nom"),
                        prenom = dr.GetString("Prenom"),
                    });
                }
            }
            return liste;
        }
        public int NbrheureMoniteur(int id, string port, string password)
        {
            int nbr = 0;
            Database conn = new Database(port, password);
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT Count(*) FROM Lecon WHERE id_moniteur =" + id;

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    nbr = dr.GetInt32("Count(*)");

                }
            }
            return nbr;
        }
    }
}
