using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Data
{
    internal class EleveDAO
    {
        public Database conn;
        public EleveDAO(string port, string password)
        {
            conn = new Database(port, password);
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : Eleve e
        // TRAITEMENT : Exécute une requête INSERT pour sauvegarder un nouvel élève en base de données
        // SORTIE     : aucune (affiche un message de succès en console)
        // ==========================================
        public void Ajouter(Eleve e)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "INSERT INTO ELEVE (CodeNEPH, nomEleve, prenomEleve, DateNaissance, Tel, Mail, TypeEleve, Adresse, RIB, Permis, Boite, idMoniteurReferent, NbHeuresAPayer) " +
                            "VALUES (@code, @nom, @prenom, @date, @tel, @mail, @type, @adresse, @rib, @permis, @boite, @moniteur, @heures);";

                using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@code", e.codeNeph);
                    cmd.Parameters.AddWithValue("@nom", e.nomEleve);
                    cmd.Parameters.AddWithValue("@prenom", e.prenomEleve);
                    cmd.Parameters.AddWithValue("@date", e.dateNaissance);
                    cmd.Parameters.AddWithValue("@tel", e.tel);
                    cmd.Parameters.AddWithValue("@mail", e.mail);
                    cmd.Parameters.AddWithValue("@type", e.typeEleve);
                    cmd.Parameters.AddWithValue("@adresse", e.adresse);
                    cmd.Parameters.AddWithValue("@rib", e.rib);
                    cmd.Parameters.AddWithValue("@permis", e.permis);
                    cmd.Parameters.AddWithValue("@boite", e.estBoiteManuelle);
                    cmd.Parameters.AddWithValue("@moniteur", e.moniteurTitre);
                    cmd.Parameters.AddWithValue("@heures", e.nbHeuresAPayer);

                    cmd.ExecuteNonQuery();
                }
                
                Console.WriteLine($"L'élève a été ajouté avec succès dans la bdd !");
            }
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : aucune
        // TRAITEMENT : Récupère tous les enregistrements de la table ELEVE
        // SORTIE     : List<Eleve> (liste des objets élèves avec ID, Nom et Prénom)
        // ==========================================
        public List<Eleve> GetAll()
        {
            List<Eleve> liste = new List<Eleve>();
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ELEVE", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    liste.Add(new Eleve
                    {
                        id_eleve = dr.GetInt32("id_eleve"),
                        nomEleve = dr.GetString("NomEleve"),
                        prenomEleve = dr.GetString("PrenomEleve"),
                    });
                }
            }
            return liste;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int id
        // TRAITEMENT : Supprime en cascade les factures, plannings, leçons et l'élève via une transaction
        // SORTIE     : aucune (commit si succès, rollback en cas d'erreur)
        // ==========================================
        public void Supprimer(int id)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                using (MySqlTransaction transaction = cn.BeginTransaction())
                {
                    try
                    {
                        ExecuteSimpleQuery("DELETE FROM Facture WHERE ID_Eleve = @id", id, cn, transaction);
                        ExecuteSimpleQuery("UPDATE Planning SET ID_Lecon = NULL, ID_Eleve = NULL WHERE ID_Eleve = @id", id, cn, transaction);
                        ExecuteSimpleQuery("DELETE FROM Lecon WHERE ID_Eleve = @id", id, cn, transaction);
                        ExecuteSimpleQuery("DELETE FROM Eleve WHERE ID_Eleve = @id", id, cn, transaction);
                        transaction.Commit();
                        Console.WriteLine("Suppression réussie !");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Erreur lors de la suppression : " + ex.Message);
                    }
                }
            }
        }

        private void ExecuteSimpleQuery(string sql, int id, MySqlConnection cn, MySqlTransaction trans)
        {
            using (MySqlCommand cmd = new MySqlCommand(sql, cn, trans))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int id
        // TRAITEMENT : Compte le nombre d'entrées dans la table Lecon pour un élève spécifique
        // SORTIE     : int (nombre d'heures effectuées)
        // ==========================================
        public int NbrheureEleve(int id)
        {
            int nbr = 0;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT Count(*) FROM Lecon WHERE id_eleve =" + id;

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    nbr = dr.GetInt32("Count(*)");

                }
            }
            return nbr;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int id
        // TRAITEMENT : Récupère la valeur de la colonne MontantReglementRestant pour un élève
        // SORTIE     : double (montant financier restant dû)
        // ==========================================
        public double MontantTotalEleve(int id)
        {
            double montant = 0;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT MontantReglementRestant FROM Eleve WHERE id_eleve=" + id;
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    montant = dr.GetDouble("MontantReglementRestant");
                }
            }
            return montant;
        }
    }
}
