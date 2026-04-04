using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Moniteur
    {
        private static int compteurId = 0;
        public int id_moniteur { get; set; }//Clé primaire
        public string nom { get; set; }
        public string prenom { get; set; } //Auto ou manuelle
        public string permisMoniteur { get; set; }
        public int salaireMoniteur { get; set; }

        public Moniteur(string nom)
        {
            
            this.id_moniteur = compteurId;
            compteurId ++;
            this.nom = nom;
            this.prenom = "";
            this.permisMoniteur = "";
            this.salaireMoniteur = 0;
        }
        public Moniteur()
        {
            id_moniteur = compteurId;
            compteurId++;
            nom = "";
            prenom = "";
            permisMoniteur = "";
            salaireMoniteur = 0;
        }
            static public bool operator  ==(Moniteur e1,Moniteur e2)
        {
            return e1.id_moniteur==e2.id_moniteur;
        }
        static public bool operator  !=(Moniteur e1,Moniteur e2)
        {
            return e1.id_moniteur!=e2.id_moniteur;
        }

        public override string ToString()
        {
            return $"{id_moniteur} | Nom: {nom} | Prénom: {prenom}";
        }
    }
}
