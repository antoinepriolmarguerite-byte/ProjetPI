using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Data
{
    internal class PlanningDAO
    {
        private Database conn;

        public PlanningDAO(string port, string password)
        {
            conn = new Database(port, password);
        }

        public PlanningDAO(Database database)
        {
            conn = database;
        }

        public List<string> RecupererPlanningDAO()
        {
            List<string> liste = new List<string>();

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = @"SELECT p.DateHeureDebut, e.NomEleve, m.Nom as MoniteurNom 
                            FROM Planning p
                            LEFT JOIN Eleve e ON p.ID_Eleve = e.ID_Eleve
                            JOIN Moniteur m ON p.ID_Moniteur = m.ID_Moniteur 
                            ORDER BY p.DateHeureDebut";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    {
                        while (dr.Read())
                        {
                            liste.Add($"{dr.GetDateTime("DateHeureDebut")} | {dr.GetString("NomEleve")} avec {dr.GetString("MoniteurNom")}");
                        }
                    }
                }
                return liste;
            }
        }

        public void AjouterLeconDansPlanning(Lecon lecon)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "INSERT INTO Planning (DateHeureDebut, DateHeureFin, ID_Eleve, ID_Moniteur, ID_Vehicule, ID_Lecon) " +
                             "VALUES (@dateDebut, @dateFin, @idEleve, @idMoniteur, @idVehicule, @idLecon)";

                using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@dateDebut", lecon.dateLecon);
                    cmd.Parameters.AddWithValue("@dateFin", lecon.dateLecon.AddHours(1));
                    cmd.Parameters.AddWithValue("@idEleve", lecon.eleve.id_eleve);
                    cmd.Parameters.AddWithValue("@idMoniteur", lecon.id_moniteur);
                    cmd.Parameters.AddWithValue("@idVehicule", lecon.vehicule.id_vehicule);
                    cmd.Parameters.AddWithValue("@idLecon", lecon.id_lecon);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
