using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Services
{
    internal class FactureDAO
    {
        private Database conn;

        public FactureDAO(string port, string password)
        {
            conn = new Database(port, password);
        }
        public FactureDAO(Database conn)
        {
            this.conn = conn;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : Lecon lecon
        // TRAITEMENT : Ajoute une nouvelle facture dans la base de donnée
        // SORTIE     : aucune
        // ==========================================
        public void AjouterLeconDansFacture(Lecon lecon)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                int prochainNumero = 1;
                string sqlCount = "SELECT COUNT(*) FROM Facture";
                using (MySqlCommand cmdCount = new MySqlCommand(sqlCount, cn))
                {
                    prochainNumero = Convert.ToInt32(cmdCount.ExecuteScalar()) + 1;
                }

                string sql = "INSERT INTO Facture (ID_Facture, Destinataire, NomEleve, Montant, DateSeance, ID_Eleve) " +
                             "VALUES (@idFacture, @destinataire, @nomEleve, @montant, @dateSeance, @idEleve)";

                using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                {
                    string numFacture = $"FAC-{DateTime.Now.Year}-{prochainNumero:D3}";
                    cmd.Parameters.AddWithValue("@idFacture", numFacture);
                    cmd.Parameters.AddWithValue("@destinataire", lecon.eleve.nomEleve);
                    cmd.Parameters.AddWithValue("@nomEleve", lecon.eleve.nomEleve);
                    cmd.Parameters.AddWithValue("@montant", lecon.montantFacture);
                    cmd.Parameters.AddWithValue("@dateSeance", lecon.dateLecon);
                    cmd.Parameters.AddWithValue("@idEleve", lecon.eleve.id_eleve);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Facture {numFacture} générée avec succès !");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Erreur MySQL : " + ex.Message);
                    }
                }
            }
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : aucune
        // TRAITEMENT : Récupère toutes les factures avec gestion des valeurs NULL (IsDBNull)
        // SORTIE     : List<Facture> (liste complète des objets factures)
        // ==========================================

        public List<Facture> GetAll()
        {
            List<Facture> liste = new List<Facture>();

            using (MySqlConnection cn = conn.GetConnection())
            {
                try
                {
                    cn.Open();
                    string sql = "SELECT * FROM FACTURE";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Facture f = new Facture();

                                f.id_facture = dr.IsDBNull(dr.GetOrdinal("ID_Facture")) ? "" : dr.GetString("ID_Facture");
                                f.destinataire = dr.IsDBNull(dr.GetOrdinal("Destinataire")) ? "Inconnu" : dr.GetString("Destinataire");
                                f.nomEleve = dr.IsDBNull(dr.GetOrdinal("NomEleve")) ? "Non renseigné" : dr.GetString("NomEleve");
                                f.typeReglement = dr.IsDBNull(dr.GetOrdinal("TypeReglement")) ? "En attente" : dr.GetString("TypeReglement");
                                f.id_eleve = dr.IsDBNull(dr.GetOrdinal("ID_Eleve")) ? 0 : dr.GetInt32("ID_Eleve");
                                f.montant = dr.IsDBNull(dr.GetOrdinal("Montant")) ? 0 : dr.GetInt32("Montant");
                                f.deadlineReglement = dr.IsDBNull(dr.GetOrdinal("DeadlineReglement")) 
                                                    ? DateTime.Now.AddMonths(1) 
                                                    : dr.GetDateTime("DeadlineReglement");

                                f.dateSeance = dr.IsDBNull(dr.GetOrdinal("DateSeance")) 
                                            ? DateTime.Now 
                                            : dr.GetDateTime("DateSeance");

                                liste.Add(f);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur lors de la récupération des factures : " + ex.Message);
                }
            }

            return liste;
        }
    }
}
