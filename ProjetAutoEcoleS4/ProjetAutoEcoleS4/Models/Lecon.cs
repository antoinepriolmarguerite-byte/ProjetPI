using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProjetAutoEcoleS4.Models
{
    internal class Lecon
    {
        private static int idCounter = 1; // Compteur pour générer des ID uniques
        public int id_Lecon  { get; set; }//Clé primaire
        public DateTime date_Lecon { get; set; }
        public Eleve eleve { get; set; } //Auto ou manuelle
        public int id_moniteur { get; set; }
        public Vehicule vehicule { get; set; }
        public double montantFacture { get; set; }//ça fout quoi là?

        public Lecon(DateTime date, Eleve eleve, int id_moniteur, Vehicule vehicule, double montantFacture)
        {
            //id_Lecon = idCounter;
            //idCounter++;
            this.date_Lecon = date;
            this.eleve = eleve;
            this.id_moniteur = id_moniteur;
            this.vehicule = vehicule;
            this.montantFacture = montantFacture;
        }   
        public Lecon()
        {
            //id_Lecon = idCounter;
            //idCounter++;
            date_Lecon = new DateTime();
            eleve = new Eleve();
            id_moniteur = 0;
            vehicule = new Vehicule();
            montantFacture = 0; 
        }
    }
}