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

        public List<string> RecupererPlanningDAO(DateTime date, int id)
        {
            List<string> liste = new List<string>();

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = @"SELECT HOUR(DateHeureDebut), MINUTE(DateHeureDebut),HOUR(DateHeureFin), MINUTE(DateHeureFin), e.PrenomEleve,e.NomEleve
                            FROM Planning p
                            LEFT JOIN Eleve e ON p.ID_Eleve = e.ID_Eleve
                            JOIN Moniteur m ON p.ID_Moniteur = m.ID_Moniteur 
                            WHERE MONTH(DateHeureDebut) =" + date.Month+ " AND YEAR(DateHeureDebut)=" + date.Year+ " AND DAY(DateHeureDebut)="+date.Day+ " AND m.ID_Moniteur="+id+ " order by DateHeureDebut";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    {
                        while (dr.Read())
                        {
                            liste.Add($"De {dr.GetInt32("HOUR(DateHeureDebut)")}:{dr.GetInt32("MINUTE(DateHeureDebut)")} à {dr.GetInt32("HOUR(DateHeureFin)")}:{dr.GetInt32("MINUTE(DateHeureFin)")} | Avec {dr.GetString("PrenomEleve")} {dr.GetString("NomEleve")}");
                        }
                    }
                }
                return liste;
            }
        }
    }
}
