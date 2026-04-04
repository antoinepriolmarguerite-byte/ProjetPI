using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Data
{
    internal class VehiculeDAO
    {
        public Database conn;
        public VehiculeDAO(string port, string password)
        {
            conn = new Database(port, password);
        }
        public List<Vehicule> GetAll()
        {
            List<Vehicule> liste = new List<Vehicule>();
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM VEHICULE order by id_vehicule", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    liste.Add(new Vehicule
                    {
                        id_vehicule = dr.GetInt32("id_vehicule"),
                        marque = dr.GetString("marque"),
                        modele = dr.GetString("modele"),
                        immatriculation = dr.GetString("immatriculation"),
                        etat = dr.GetBoolean("etat")
                    });
                }
            }
            return liste;
        }

        public int FindVehicule(string immatriculation)
        {
            int id_vehicule = 0;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id_vehicule FROM VEHICULE WHERE immatriculation = @immat", cn);
                cmd.Parameters.AddWithValue("@immat", immatriculation);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    id_vehicule = Convert.ToInt32(result);
                }
            }
            return id_vehicule;
        }

        public double Nbrkilometre(int idvehicule, int anne, int Mois)
        {
            double nbr = 0;
            string phrase = anne + "" + Mois;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Nbkilometre FROM KilometrageMois where ID_Vehicule=@idvehicule and Annee_mois=@annemois", cn);
                cmd.Parameters.AddWithValue("@idvehicule", idvehicule);
                cmd.Parameters.AddWithValue("@annemois", phrase);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    nbr = Convert.ToDouble(result);
                }
            }
            return nbr;
        }
        public void Ajouter(Vehicule v)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string insertTable = "insert into VEHICULE(Immatriculation,TypeVehicule,Boite,Marque,Modele) Values (@immatriculation,@typevehicule,@boitevitesse,@marque,@modele);";
                MySqlCommand cmd = new MySqlCommand(insertTable, cn);
                cmd.Parameters.AddWithValue("@immatriculation", v.immatriculation);
                cmd.Parameters.AddWithValue("@typevehicule", v.typevehicule);
                cmd.Parameters.AddWithValue("@boitevitesse", v.boitevitesse);
                cmd.Parameters.AddWithValue("@marque", v.marque);
                cmd.Parameters.AddWithValue("@modele", v.modele);
                Console.WriteLine("Insertion réalisée");
                cn.Dispose();
            }
            Thread.Sleep(1000);
        }
        public void Supprimer(int id_vehicule)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                try
                {
                    cn.Open();
                    string deleteQuery = "DELETE FROM Vehicule WHERE ID_Vehicule = @id_vehicule;";

                    MySqlCommand cmd = new MySqlCommand(deleteQuery, cn);
                    cmd.Parameters.AddWithValue("@id_vehicule", id_vehicule);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Le véhicule {id_vehicule} a été supprimé avec succès.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun véhicule trouvé avec cet id_vehicule.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression : " + ex.Message);
                }
            }
        }

    }
}
