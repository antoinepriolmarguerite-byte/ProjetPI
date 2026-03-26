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
    internal class Lecon_DAO
    {
        public Database conn;
        public Lecon_DAO() 
        {
            conn = new Database("3306", "Ayfa1712////");
        }
        public void AjouterLecon_DAO(Lecon c)
        {
            using (MySqlConnection cn = conn.GetConnection())
            {
                cn.Open();
                string insertTable = "insert into Lecon(id_Lecon,date,eleve,moniteur,vehicule,montantFacture) Values ("+c.id_Lecon+","+c.date+","+c.eleve+","+c.moniteur+","+c.vehicule+","+c.montantFacture+");";
                MySqlCommand con = new MySqlCommand(insertTable, cn);
                
                Console.WriteLine("Insertion réalisée");

                cn.Dispose();
            }
        }
    }
}
