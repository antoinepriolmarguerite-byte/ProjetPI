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
        public Eleve(string CodeNEPH,string Nom,string Prenom,string Tel,string Mail,string TypeEleve,string Adresse,string Rib,DateTime DateNaissance,string Permis, bool EstBoiteManuelle, string MoniteurTitre ,int NbHeureARegler)
        {
            this.CodeNEPH = CodeNEPH;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Tel = Tel;
            this.Mail = Mail;
            this.TypeEleve = TypeEleve;
            this.Adresse = Adresse;
            this.Rib = Rib;
            this.DateNaissance = DateNaissance;
            this.Permis = Permis;
            this.EstBoiteManuelle = EstBoiteManuelle;
            this.MoniteurTitre = MoniteurTitre;
            this.NbHeureARegler = NbHeureARegler;
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
