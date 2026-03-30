using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Services;
//using ProjetAutoEcoleS4.Data;
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
        public int id_Lecon { get; set; } //Clé primaire
        public DateTime date { get; set; }
        public Eleve eleve { get; set; } //Auto ou manuelle
        public string moniteur { get; set; }
        public string vehicule { get; set; }
        public double montantFacture { get; set; }//ça fout quoi là?

        public Lecon(DateTime date, Eleve eleve, string moniteur, string vehicule, double montantFacture)
        {
            id_Lecon = idCounter;
            idCounter++;
            this.date = date;
            this.eleve = eleve;
            this.moniteur = moniteur;
            this.vehicule = vehicule;
            this.montantFacture = montantFacture;
        }
        public Lecon()
        {
            id_Lecon = idCounter;
            idCounter++;
            date = new DateTime();
            eleve = new Eleve();
            moniteur = "";
            vehicule = "";
            montantFacture = 0;
        }
    }
}