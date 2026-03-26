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
        public void Ajouter(Eleve e) //MON GROS CACA
        {
            Database conn = new Database("3312","");
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "INSERT INTO ELEVE(nom, prenom, date_naissance, telephone) VALUES (@n,@p,@d,@t)";
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@n", e.Nom);
                cmd.Parameters.AddWithValue("@p", e.Prenom);
                cmd.Parameters.AddWithValue("@d", e.DateNaissance);
                cmd.Parameters.AddWithValue("@t", e.Tel);
                cmd.Parameters.AddWithValue("@t", e.Tel);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Eleve> GetAll()
        {
            Database conn = new Database("3312","");
            List<Eleve> liste = new List<Eleve>();
            //using (MySqlConnection cn = conn.GetConnection())
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ELEVE", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    liste.Add(new Eleve
                    {
                        CodeNEPH = dr.GetString("id_eleve"),
                        Nom = dr.GetString("nom"),
                        Prenom = dr.GetString("prenom"),
                        DateNaissance = dr.GetDateTime("date_naissance"),
                        Tel = dr.GetString("telephone")
                    });
                }
            }
            return liste;
        }

        public void Supprimer(int id)
        {
            Database conn = new Database("3312","");
            //using (MySqlConnection cn = conn.GetConnection())
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM ELEVE WHERE id_eleve=@id", cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}