using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
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
            //Console.WriteLine("Client construit avec succès ! NE PAS SORTIR DE SON CONTEXTE");
        }
        public Eleve(IEleveService view)
        {
            string[] attributs = view.AjouterEleve();
            this.CodeNEPH = attributs[0];
            this.Nom = attributs[1];
            this.Prenom = attributs[2];
            this.Tel = attributs[3];
            this.Mail = attributs[4];
            this.TypeEleve = attributs[5];
            this.Adresse = attributs[6];
            this.Rib = attributs[7];
            this.DateNaissance = DateTime.Parse(attributs[8]);
            this.Permis = attributs[9];
            this.EstBoiteManuelle = bool.Parse(attributs[9]);
            this.MoniteurTitre = attributs[10];
            this.NbHeureARegler = 0;
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
            //Console.WriteLine("Client construit avec succès ! NE PAS SORTIR DE SON CONTEXTE"); 
        }
        public static bool operator ==(Eleve e1, Eleve e2)
        {
            return e1.CodeNEPH==e2.CodeNEPH;
        }
        public static bool operator !=(Eleve e1, Eleve e2)
        {
            return e1.CodeNEPH!=e2.CodeNEPH;
        }
    }
}
