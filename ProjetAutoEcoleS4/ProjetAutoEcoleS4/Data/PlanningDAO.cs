using MySql.Data.MySqlClient;
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

        public List<string> RecupererPlanningDAO()
        {
            List<string> liste = new List<string>();

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = @"SELECT p.DateHeureDebut, e.NomEleve, m.Nom as MoniteurNom 
                           FROM Planning p
                           JOIN Eleve e ON p.id_eleve = e.id_eleve
                           JOIN Moniteur m ON p.ID_Moniteur = m.ID_Moniteur order by p.DateHeureDebut";

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
    }
}
