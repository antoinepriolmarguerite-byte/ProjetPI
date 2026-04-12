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

        public List<string> RecupererPlanningDAOJournalier(DateTime date, int id)
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

        public string RecupererPlanningDAOHebdomadaire(DateTime date, int id)
        {
            string result = "";
            Dictionary<string, List<string>> planning = new Dictionary<string, List<string>>()
            {
                { "Lundi", new List<string>() },
                { "Mardi", new List<string>() },
                { "Mercredi", new List<string>() },
                { "Jeudi", new List<string>() },
                { "Vendredi", new List<string>() },
                { "Samedi", new List<string>() },
                { "Dimanche", new List<string>() },
            };
        
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();

                string sql = @"
        SELECT 
            DAYOFWEEK(DateHeureDebut) as Jour,
            HOUR(DateHeureDebut) as H1, 
            MINUTE(DateHeureDebut) as M1,
            HOUR(DateHeureFin) as H2, 
            MINUTE(DateHeureFin) as M2,
            e.PrenomEleve,
            e.NomEleve
        FROM Planning p
        LEFT JOIN Eleve e ON p.ID_Eleve = e.ID_Eleve
        JOIN Moniteur m ON p.ID_Moniteur = m.ID_Moniteur 
        WHERE WEEK(DateHeureDebut, 1) = WEEK(@date, 1) 
        AND m.ID_Moniteur = @id
        ORDER BY DateHeureDebut";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int jour = dr.GetInt32("Jour");

                        // MySQL : 1=Dimanche, 2=Lundi, ..., 7=Samedi
                        string jourNom = jour switch
                        {
                            2 => "Lundi",
                            3 => "Mardi",
                            4 => "Mercredi",
                            5 => "Jeudi",
                            6 => "Vendredi",
                            7 => "Samedi",
                            1 => "Dimanche",
                            _ => "Inconnu"
                        };

                        string cours = $"De {dr.GetInt32("H1")}:{dr.GetInt32("M1")} à {dr.GetInt32("H2")}:{dr.GetInt32("M2")} | Avec {dr.GetString("PrenomEleve")} {dr.GetString("NomEleve")}";

                        planning[jourNom].Add(cours);
                        for (int i = 0; i < planning[jourNom].Count; i++)
                        {
                            result = result + jourNom + " : \n";
                            if (planning[jourNom][i] == "")
                            {
                                result = result +"Aucun cours prévu";
                            }
                            else
                            { 
                                result = result +planning[jourNom][i]+ "\n" ;
                            }
                        }
                    }
                }

            }

            return result;
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
