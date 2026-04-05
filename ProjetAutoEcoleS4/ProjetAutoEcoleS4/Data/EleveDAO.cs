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
        public void Ajouter(Eleve e, string port, string password) //MON GROS CACA respectez ce commentaire, c'est le 1er push de Bastien
        {
            Database conn = new Database(port, password);
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "INSERT INTO ELEVE (CodeNEPH, nomEleve, prenomEleve, DateNaissance, Tel, Mail, TypeEleve, Adresse, RIB, Permis, Boite, MoniteurTitre, NbHeuresAPayer) " +
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

        public List<Eleve> GetAll(string port, string password)
        {
            Database conn = new Database(port, password); //Ronan changera
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

        public void Supprimer(int id, string port, string password)
        {
            Database conn = new Database(port, password);
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM ELEVE WHERE CodeNEPH = @id", cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public int NbrheureEleve(int id, string port, string password)
        {
            int nbr = 0;
            Database conn = new Database(port, password);
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
        public double MontantTotalEleve(int id, string port, string password)
        {
            double montant = 0;
            Database conn = new Database(port, password);
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
