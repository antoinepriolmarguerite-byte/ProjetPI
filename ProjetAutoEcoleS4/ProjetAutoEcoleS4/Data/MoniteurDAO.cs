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

        public List<string> RecupererPlanningDAO() //A supprimé, les moniteurs sont ajoutés manuellement et si on a le temps on ajoutera ptêtre l'option
        {
            List<string> liste = new List<string>();

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = @"SELECT p.DateHeureDebut, e.Nom, m.Nom as MoniteurNom 
                           FROM Planning p
                           JOIN Eleve e ON p.CodeNEPH = e.CodeNEPH
                           JOIN Moniteur m ON p.ID_Moniteur = m.ID_Moniteur";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        liste.Add($"{dr.GetDateTime("DateHeureDebut")} | {dr.GetString("Nom")} avec {dr.GetString("MoniteurNom")}");
                    }
                }
            }
            return liste;
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
                        id_Moniteur = dr.GetInt32("id_Moniteur"),//bawi faut changer guignol
                        nom = dr.GetString("Nom"),
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
