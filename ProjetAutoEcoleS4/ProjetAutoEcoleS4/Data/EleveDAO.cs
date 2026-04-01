
﻿using MySql.Data.MySqlClient;
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
        public void Ajouter(Eleve e,string port, string password) //MON GROS CACA respectez ce commentaire, c'est le 1er push de Bastien
        {
            Database conn = new Database(port,password);
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string sql = "INSERT INTO ELEVE(CodeNEPH,Nom,Prenom, DateNaissance,Tel,Mail) VALUES ("+e.CodeNEPH+","+e.Nom+","+e.Prenom+","+e.DateNaissance+","+e.Tel+","+e.Mail+");";
                MySqlCommand cmd = new MySqlCommand(sql, cn);

                cn.Dispose();
            }
        }

        public List<Eleve> GetAll(string port,string password)
        {
            Database conn = new Database(port,password); //Ronan changera
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
                        Nom = dr.GetString("nom"),
                        Prenom = dr.GetString("prenom"),
                        //DateNaissance = dr.GetDateTime("date_naissance"),
                        //Tel = dr.GetString("telephone")
                    });
                }
            }
            return liste;
        }

        public void Supprimer(int id,string port,string password)
        {
            Database conn = new Database(port,password);
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM ELEVE WHERE CodeNEPH=@id", cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
