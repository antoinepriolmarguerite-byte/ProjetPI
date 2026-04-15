using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Data
{
    internal class LeconDAO
    {
        public Database conn;
        public LeconDAO(string port, string password)
        {
            conn = new Database(port, password);
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : Lecon c
        // TRAITEMENT : Insère une leçon, récupère son ID auto-généré et met à jour le Planning et la Facturation
        // SORTIE     : aucune
        // ==========================================
        public void AjouterLecon_DAO(Lecon c)
        {
            PlanningDAO planningDAO = new PlanningDAO(conn);
            FactureDAO factureDAO = new FactureDAO(conn);

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();

                string insertTable = "INSERT INTO Lecon(Date_, ID_Eleve, ID_Moniteur, ID_Vehicule, MontantFacture) " +
                                     "VALUES (@date, @idEleve, @idMoniteur, @idVehicule, @montant); " +
                                     "SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(insertTable, cn))
                {
                    cmd.Parameters.AddWithValue("@date", c.dateLecon);
                    cmd.Parameters.AddWithValue("@idEleve", c.eleve.id_eleve);
                    cmd.Parameters.AddWithValue("@idMoniteur", c.id_moniteur);
                    cmd.Parameters.AddWithValue("@idVehicule", c.vehicule.id_vehicule);
                    cmd.Parameters.AddWithValue("@montant", c.montantFacture);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        c.id_lecon = Convert.ToInt32(result);
                    }
                }
                planningDAO.AjouterLeconDansPlanning(c);
                factureDAO.AjouterLeconDansFacture(c);
            }
        }
        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : string codeNEPH, DateTime dateLecon
        // TRAITEMENT : Vérifie si un élève a déjà une leçon programmée à une date précise
        // SORTIE     : bool (true si une leçon existe déjà)
        // ==========================================
        public bool VerifierLeconEleve(string codeNEPH, DateTime dateLecon)
        {
            bool leconExiste = false;

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();

                string sql = "SELECT 1 FROM Lecon " +
                             "JOIN Eleve ON Lecon.id_eleve = Eleve.id_eleve " +
                             "WHERE Eleve.CodeNEPH = @codeNEPH " +
                             "AND Lecon.Date_ = @dateLecon " +
                             "LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@codeNEPH", codeNEPH);
                cmd.Parameters.AddWithValue("@dateLecon", dateLecon);

                MySqlDataReader dr = cmd.ExecuteReader();

                leconExiste = dr.Read(); // true si une ligne existe
            }

            return leconExiste;
        }

        public bool VerifierLeconMoniteur(string moniteur, DateTime dateLecon)
        {
            bool leconExiste = false;

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();

                string sql = "SELECT id_vehicule FROM Lecon " +
                             "JOIN Moniteur ON Lecon.id_moniteur = Moniteur.id_moniteur " +
                             "WHERE Moniteur.Nom = @nom " +
                             "AND Lecon.Date_ = @dateLecon " +
                             "LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nom", moniteur);
                cmd.Parameters.AddWithValue("@dateLecon", dateLecon);

                MySqlDataReader dr = cmd.ExecuteReader();

                leconExiste = dr.Read(); // true si une ligne existe
            }

            return leconExiste;
        }


        public bool VerifierLeconVehicule(int vehicule, DateTime dateLecon)
        {
            bool leconExiste = false;

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();

                string sql = "SELECT Vehicule.id_vehicule FROM Lecon " +
                             "JOIN Vehicule ON Lecon.id_vehicule = Vehicule.id_vehicule " +
                             "WHERE Vehicule.immatriculation = @idvehicule " +
                             "AND Lecon.Date_ = @dateLecon " +
                             "LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idVehicule", vehicule);
                cmd.Parameters.AddWithValue("@dateLecon", dateLecon);

                MySqlDataReader dr = cmd.ExecuteReader();

                leconExiste = dr.Read(); // true si une ligne existe
            }

            return leconExiste;
        }
        public int Id_LeconFromDateAndEleve(string codeNEPH, DateTime dateLecon)
        {
            int id_lecon = 0;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT ID_Lecon FROM Lecon " +
                             "JOIN Eleve ON Lecon.Eleve = Eleve.CodeNEPH " +
                             "WHERE Eleve.CodeNEPH = @neph AND Lecon.Date_ = @date";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@neph", codeNEPH);
                cmd.Parameters.AddWithValue("@date", dateLecon);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    id_lecon = Convert.ToInt32(result);
                }
            }
            return id_lecon;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int idlecon
        // TRAITEMENT : Détache la leçon du planning puis supprime l'enregistrement de la table Lecon
        // SORTIE     : aucune
        // ==========================================
        public void SupprimerLecon_DAO(int idlecon)
        {
            //int idLecon = Id_LeconFromDateAndEleve(codeNEPH, dateLecon);

            if (idlecon <= 0)
            {
                Console.WriteLine("Erreur : ID de leçon invalide.");
                return;
            }

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sqlUpdate = "UPDATE Planning SET ID_Lecon = NULL WHERE ID_Lecon = @id";
                using (MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, cn))
                {
                    cmdUpdate.Parameters.AddWithValue("@id", idlecon);
                    cmdUpdate.ExecuteNonQuery();
                }
                string sql = "DELETE FROM Lecon WHERE id_Lecon = @id";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", idlecon);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Suppression réalisée avec succès.");
                else
                    Console.WriteLine("Aucune leçon trouvée avec cet ID.");
            }
        }
        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int anne, int mois
        // TRAITEMENT : Calcule la somme des montants facturés pour un mois et une année donnés
        // SORTIE     : double (chiffre d'affaires mensuel)
        // ==========================================
        public double Chiffremensuel(int anne, int mois)
        {
            double chiffre = 0;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT SUM(MontantFacture) FROM Lecon WHERE MONTH(Date_) = " + mois + " AND YEAR(Date_) = " + anne;
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    chiffre = Convert.ToDouble(result);
                }
            }
            return chiffre;
        }


        public List<Lecon> GetAll(string port, string password)
        {
            Database conn = new Database(port, password);
            List<Lecon> liste = new List<Lecon>();
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM LECON", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    liste.Add(new Lecon
                    {
                        id_lecon = dr.GetInt32("id_lecon"),
                        dateLecon = dr.GetDateTime("date_"),
                        id_moniteur = dr.GetInt32("ID_Moniteur"),
                    });
                }
            }
            return liste;
        }
        public List<string> GetAllsuppr(string port, string password)
        {
            Database conn = new Database(port, password);
            List<string> liste = new List<string>();
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM LECON left join eleve on lecon.id_eleve=eleve.id_eleve left join moniteur on moniteur.id_moniteur=lecon.id_moniteur order by id_lecon", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    liste.Add($"{dr.GetInt32("id_lecon")} | Nom de l'élève : {dr.GetString("nomeleve")} | Prénom de l'élève : {dr.GetString("prenomeleve")} | Nom du moniteur : {dr.GetString("Nom")} | Prénom du moniteur : {dr.GetString("prenom")} | Date : {dr.GetDateTime("date_")}");
                }
            }
            return liste;
        }
    }
}
