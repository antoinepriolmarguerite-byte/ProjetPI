using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysqlx.Expr;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
namespace ProjetAutoEcoleS4.Models
{
    internal class Eleve
    {
        private static int autoincr = 0;
        public int id_eleve { get; set; }
        public string codeNeph { get; set; } 
        public string nomEleve { get; set; }
        public string prenomEleve { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
        public string typeEleve { get; set; }
        public string adresse { get; set; }
        public string rib { get; set; }
        public DateTime dateNaissance { get; set; }
        public string permis { get; set; }
        public string estBoiteManuelle { get; set; } //true = Boite manuelle
        public int moniteurTitre { get; set; }
        public int nbHeuresAPayer { get; set; }
        public double montantReglementRestant { get; set; }
        public Eleve(string codeNEPH)
        {
            this.id_eleve = autoincr++;
            this.codeNeph = codeNEPH;
            nomEleve = "";
            prenomEleve = "";
            tel = "";
            mail = "";
            typeEleve = "";
            adresse = "";
            rib = "";
            dateNaissance = new DateTime();
            permis = "";
            estBoiteManuelle = "";
            moniteurTitre = 0;
            nbHeuresAPayer = 0;
            montantReglementRestant = 0;
        }
        public Eleve(IEleveService view,string port,string password)
        {
            string[] attributs = view.CreerEleve();
            this.codeNeph = attributs[0];
            this.nomEleve = attributs[1];
            this.prenomEleve = attributs[2];
            this.tel = attributs[3];
            this.mail = attributs[4];
            this.typeEleve = attributs[5];
            this.adresse = attributs[6];
            this.rib = attributs[7];
            this.dateNaissance = DateTime.Parse(attributs[8]);
            this.permis = attributs[9];
            this.estBoiteManuelle = attributs[10];
            this.moniteurTitre = int.Parse(attributs[11]);
            this.nbHeuresAPayer = 0;
        }
        public override string ToString()
        {
            return $"{id_eleve} | Code NEPH: {codeNeph} | Nom: {nomEleve} | Prénom: {prenomEleve}";
        }
        static public bool operator  ==(Eleve e1,Eleve e2)
        {
            return e1.codeNeph == e2.codeNeph;
        }
        static public bool operator  !=(Eleve e1,Eleve e2)
        {
            return e1.codeNeph != e2.codeNeph;
        }
        public Eleve()
        {
            codeNeph = "";
            nomEleve = "";
            prenomEleve = "";
            tel = "";
            mail = "";
            typeEleve = "";
            adresse = "";
            rib = "";
            dateNaissance = new DateTime();
            permis = "";
            estBoiteManuelle = "";
            moniteurTitre = 0;
            nbHeuresAPayer = 0;
            montantReglementRestant = 0;
        }
    }
}
