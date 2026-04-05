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

        public List<Facture> GetAll()
        {
            List<Facture> list_facture = new List<Facture>();

            return list_facture;
        }
    }
}
