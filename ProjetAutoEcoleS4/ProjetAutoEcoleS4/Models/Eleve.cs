using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAutoEcoleS4.Data;
namespace ProjetAutoEcoleS4.Models
{
    internal class Eleve
    {
        public string CodeNEPH { get; set; } //idEleve
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string TypeEleve { get; set; }
        public string Adresse { get; set; }
        public string Rib { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Permis { get; set; }
        public bool EstBoiteManuelle { get; set; } //true => Boite manuelle
        public string MoniteurTitre { get; set; }
        public int NbHeureARegler { get; set; }
        public double MontantReglementRestant { get; set; }
        public Eleve(string CodeNEPH)
        {
            this.CodeNEPH = CodeNEPH;
            Nom = "";
            Prenom = "";
            Tel = "";
            Mail = "";
            TypeEleve = "";
            Adresse = "";
            Rib = "";
            DateNaissance = new DateTime();
            Permis = "";
            EstBoiteManuelle = false;
            MoniteurTitre = "";
            NbHeureARegler = 0;
            MontantReglementRestant = 0;
            //Console.WriteLine("Client construit avec succès ! NE PAS SORTIR DE SON CONTEXTE");
        }
        public Eleve()
        {
            CodeNEPH = "";
            Nom = "";
            Prenom = "";
            Tel = "";
            Mail = "";
            TypeEleve = "";
            Adresse = "";
            Rib = "";
            DateNaissance = new DateTime();
            Permis = "";
            EstBoiteManuelle = false;
            MoniteurTitre = "";
            NbHeureARegler = 0;
            MontantReglementRestant = 0;
            //Console.WriteLine("Client construit avec succès ! NE PAS SORTIR DE SON CONTEXTE"); 
        }

    }
}
