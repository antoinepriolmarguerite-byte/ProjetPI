using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Moniteur
    {
        public string id_Moniteur { get; set; }//Clé primaire
        public string nom { get; set; }
        public string prenom { get; set; } //Auto ou manuelle
        public string permis_moniteur { get; set; }
        public int salaire_Moniteur { get; set; }

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
