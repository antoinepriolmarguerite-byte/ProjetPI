using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Moniteur
    {
        private static int compteurId=0;
        public int id_Moniteur { get; set; }//Clé primaire
        public string nom { get; set; }
        public string prenom { get; set; } //Auto ou manuelle
        public string permis_moniteur { get; set; }
        public int salaire_Moniteur { get; set; }

        public Moniteur(string nom)
        {
            
            this.id_Moniteur = compteurId;
            compteurId ++;
            this.nom = nom;
            this.prenom = "";
            this.permis_moniteur = "";
            this.salaire_Moniteur = 0;
        }
        public Moniteur()
        {
            id_Moniteur = compteurId;
            compteurId++;
            nom = "";
            prenom = "";
            permis_moniteur = "";
            salaire_Moniteur = 0;
        }
            static public bool operator  ==(Moniteur e1,Moniteur e2)
        {
            return e1.id_Moniteur==e2.id_Moniteur;
        }
        static public bool operator  !=(Moniteur e1,Moniteur e2)
        {
            return e1.id_Moniteur!=e2.id_Moniteur;
        }

        public override string ToString()
        {
            return $"{id_Moniteur} | Nom: {nom} | Prénom: {prenom}";
        }
    }
}
