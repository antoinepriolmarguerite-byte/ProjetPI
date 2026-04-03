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
        public List<Vehicule> GetAll(string port, string password)
        {
            Database conn = new Database(port, password);
            List<Vehicule> liste = new List<Vehicule>();
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM VEHICULE order by  id_vehicule", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    liste.Add(new Vehicule
                    {
                        id_vehicule = dr.GetInt32("id_vehicule"),
                        marque = dr.GetString("marque"),
                        modele = dr.GetString("modele"),
                        immatriculation = dr.GetString("immatriculation"),
                        etat = dr.GetString("etat")
                    });
                }
            }
            return liste;
        }
        public double Nbrkilometre(int idvehicule, string port, string password)
        {
            double nbr = 0;
            Database conn = new Database(port, password);
            List<Vehicule> liste = new List<Vehicule>();
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Nbkilometre FROM KilmometrageMois where id_vehicule=@idvehicule", cn);
                cmd.Parameters.AddWithValue("@idvehicule", idvehicule);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    nbr = Convert.ToDouble(result);
                }
            }
            return nbr;
        }
    }
}