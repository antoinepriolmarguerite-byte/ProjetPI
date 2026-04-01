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
        public void AjouterLecon_DAO(Lecon c)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string insertTable = "insert into Lecon(id_Lecon,date,eleve,moniteur,vehicule,montantFacture) Values (" + c.id_Lecon + "," + c.date + "," + c.eleve + "," + c.moniteur + "," + c.vehicule + "," + c.montantFacture + ");";
                MySqlCommand con = new MySqlCommand(insertTable, cn);

                Console.WriteLine("Insertion réalisée");

                cn.Dispose();
            }
        }
        public bool VerifierLeconEleve(string codeNEPH, DateTime dateLecon)
        {
            bool leconExiste = false;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT * FROM Lecon " +
                             "JOIN Eleve ON Lecon.id_eleve = Eleve.id_eleve " +
                             "WHERE CodeNEPH = "+codeNEPH+" AND Date_ = "+dateLecon;
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                if(sql != null && sql != "")
                {
                    leconExiste = true;
                }
                else if(sql == null || sql == "")
                {
                    leconExiste = false;
                }
            }
            return leconExiste;
        }
        public bool VerifierLeconMoniteur(string moniteur, DateTime dateLecon)
        {
            bool leconExiste = false;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT * FROM Lecon " +
                             "JOIN Moniteur ON Lecon.id_moniteur = Moniteur.id_moniteur" +
                             "WHERE nom = " + moniteur + " AND Date_ = " + dateLecon;
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                if (sql != null && sql != "")
                {
                    leconExiste = true;
                }
                else if (sql == null || sql == "")
                {
                    leconExiste = false;
                }
            }
            return leconExiste;
        }
        public bool VerifierLeconVehicule(string vehicule, DateTime dateLecon)
        {
            bool leconExiste = false;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT * FROM Lecon " +
                             "JOIN Eleve ON Lecon.id_eleve = Eleve.id_eleve " +
                             "WHERE Immatriculation = " + vehicule + " AND Date_ = " + dateLecon;
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                if (sql != null && sql != "")
                {
                    leconExiste = true;
                }
                else if (sql == null || sql == "")
                {
                    leconExiste = false;
                }
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

        public void SupprimerLecon_DAO(string codeNEPH, DateTime dateLecon)
        {
            int idLecon = Id_LeconFromDateAndEleve(codeNEPH, dateLecon);

            if (idLecon <= 0)
            {
                Console.WriteLine("Erreur : ID de leçon invalide.");
                return;
            }

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "DELETE FROM Lecon WHERE id_Lecon = @id";

                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", idLecon);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Suppression réalisée avec succès.");
                else
                    Console.WriteLine("Aucune leçon trouvée avec cet ID.");
            }
        }
    }
}

