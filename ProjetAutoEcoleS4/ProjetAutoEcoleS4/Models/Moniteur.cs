using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Moniteur
    {
        static private int idCounter = 0;
        public string id_Moniteur { get; set; }//Clé primaire
        public string nom { get; set; }
        public string prenom { get; set; } //Auto ou manuelle
        public string permis_moniteur { get; set; }
        public int salaire_Moniteur { get; set; }

        public Moniteur(string nom, string prenom, string permis_moniteur, int salaire_Moniteur)
        {
            if(idCounter < 10) id_Moniteur ="MONIT00"+idCounter;
            if(idCounter < 100) id_Moniteur ="MONIT0"+idCounter;
            else id_Moniteur = "MONIT"+idCounter;
            idCounter++;
            this.nom = nom;
            this.prenom = prenom;
            this.permis_moniteur = permis_moniteur;
            this.salaire_Moniteur = salaire_Moniteur;
            //this.montantFacture = montantFacture;
        }   
        public Moniteur()
        {
            if(idCounter < 10) id_Moniteur ="MONIT00"+idCounter;
            if(idCounter < 100) id_Moniteur ="MONIT0"+idCounter;
            else id_Moniteur = "MONIT"+idCounter;
            idCounter++;
            nom = "";
            prenom = "";
            permis_moniteur = "";
            salaire_Moniteur = 0;
        }
        public override string ToString()
        {
            return $"{id_Moniteur} | Nom: {nom} | Prénom: {prenom}";
        }
        
        static public bool operator  ==(Moniteur e1,Moniteur e2)
        {
            return e1.id_Moniteur==e2.id_Moniteur;
        }
        static public bool operator  !=(Moniteur e1,Moniteur e2)
        {
            return e1.id_Moniteur!=e2.id_Moniteur;
        }
    }
}
