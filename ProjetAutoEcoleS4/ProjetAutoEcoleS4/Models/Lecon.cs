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
        public string id_Lecon  { get; set; }//Clé primaire
        public DateTime date_Lecon { get; set; }
        public Eleve eleve { get; set; } //
        public Moniteur moniteur { get; set; }
        public string vehicule { get; set; }
        public double montantFacture { get; set; }//ça fout quoi là?

        public Lecon(DateTime date, Eleve eleve, Moniteur moniteur, string vehicule, double montantFacture)
        {
            if(idCounter < 10) id_Lecon ="LEC00"+idCounter;
            if(idCounter < 100) id_Lecon ="LEC0"+idCounter;
            else id_Lecon = "LEC"+idCounter;
            idCounter++;
            this.date_Lecon = date;
            this.eleve = eleve;
            this.moniteur = moniteur;
            this.vehicule = vehicule;
            this.montantFacture = montantFacture;
        }   
        public Lecon()
        {
            if(idCounter < 10) id_Lecon ="MONIT00"+idCounter;
            if(idCounter < 100) id_Lecon ="MONIT0"+idCounter;
            else id_Lecon = "MONIT"+idCounter;
            idCounter++;
            date_Lecon = new DateTime();
            eleve = new Eleve();
            moniteur = new Moniteur();
            vehicule = "";
            montantFacture = 0; 
        }
    }
}