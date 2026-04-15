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

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : aucune
        // TRAITEMENT : Récupère tous les véhicules avec leur état et immatriculation
        // SORTIE     : List<Vehicule>
        // ==========================================
        public List<Vehicule> GetAll()
        {
            List<Vehicule> liste = new List<Vehicule>();

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "SELECT ID_Vehicule, Marque, Modele, Immatriculation, Etat FROM Vehicule ORDER BY ID_Vehicule";

                using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                {
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            liste.Add(new Vehicule
                            {
                                id_vehicule = dr.GetInt32("ID_Vehicule"),
                                marque = dr.IsDBNull(dr.GetOrdinal("Marque")) ? "" : dr.GetString("Marque"),
                                modele = dr.IsDBNull(dr.GetOrdinal("Modele")) ? "" : dr.GetString("Modele"),
                                immatriculation = dr.GetString("Immatriculation"),
                                etat = dr.GetBoolean("Etat")
                            });
                        }
                    }
                }
            }
            return liste;
        }


        public int FindVehicule(int idvehicule)
        {
            int id_vehicule = 0;
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id_vehicule FROM VEHICULE WHERE id_vehicule = @immat", cn);
                cmd.Parameters.AddWithValue("@immat", idvehicule);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    id_vehicule = Convert.ToInt32(result);
                }
            }
            return id_vehicule;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int idvehicule, int anne, int Mois
        // TRAITEMENT : Récupère le kilométrage enregistré pour un véhicule sur une période donnée
        // SORTIE     : double (nombre de kilomètres)
        // ==========================================
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

                cmd.ExecuteNonQuery();
                Console.WriteLine("Insertion réalisée");
                cn.Dispose();
            }
            Thread.Sleep(500);
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int id_vehicule
        // TRAITEMENT : Supprime les données liées (kilométrage, planning, leçons) avant de supprimer le véhicule
        // SORTIE     : aucune
        // ==========================================
        public void Supprimer(int id_vehicule)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                try
                {
                    cn.Open();
                    string sql = "DELETE FROM kilometragemois WHERE ID_Vehicule = @id_vehicule;";
                    string fe = "DELETE FROM Planning WHERE ID_Vehicule = @id_vehicule;";
                    MySqlCommand re = new MySqlCommand(fe, cn);
                    re.Parameters.AddWithValue("@id_vehicule", id_vehicule);
                    int h = re.ExecuteNonQuery();
                    string ak = "DELETE FROM Lecon WHERE ID_Vehicule = @id_vehicule;";
                    MySqlCommand ta = new MySqlCommand(ak, cn);
                    ta.Parameters.AddWithValue("@id_vehicule", id_vehicule);
                    int g = ta.ExecuteNonQuery();
                    string deleteQuery = "DELETE FROM Vehicule WHERE ID_Vehicule = @id_vehicule;";
                    MySqlCommand a = new MySqlCommand(sql, cn);
                    a.Parameters.AddWithValue("@id_vehicule", id_vehicule);
                    int f = a.ExecuteNonQuery();
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
        public bool VerifmodeleVehicule(int idvehicule, int ideleve)
        {
            bool verif = false;

            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT vehicule.boite, eleve.boite " +
                    "FROM eleve " +
                    "JOIN lecon ON eleve.id_eleve = lecon.id_eleve " +
                    "JOIN vehicule ON lecon.id_vehicule = vehicule.id_vehicule " +
                    "WHERE vehicule.id_vehicule = @idvehicule AND eleve.id_eleve = @ideleve", cn);

                cmd.Parameters.AddWithValue("@idvehicule", idvehicule);
                cmd.Parameters.AddWithValue("@ideleve", ideleve);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    bool boiteVehicule = dr.GetBoolean(0); // true = auto, false = manuelle
                    string boiteEleve = dr.GetString(1).ToLower();

                    if ((boiteVehicule && boiteEleve == "auto") ||
                        (!boiteVehicule && boiteEleve == "manuelle"))
                    {
                        verif = true;
                    }
                }
            }

            return verif;
        }

    }
}
