using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Moniteur
    {
        public string id_Moniteur;//Clé primaire
        public string nom;
        public string prenom; //Auto ou manuelle
        public string permis_moniteur;
        public int salaire_Moniteur;
    }
}
