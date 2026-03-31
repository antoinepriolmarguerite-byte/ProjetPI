using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Planning
    {
        private int idPlanning { get; }
        private DateTime dateHeureDebut { get; set; }
        private DateTime dateHeureFin { get; set; }
        private string formule; //C'est le type d'offre souscrite (35h boite auto, 28h boite manu ect...)
        private string immatriculation { get; set; }
        private int id_lecon { get; }
        private string id_moniteur { get; }
        private string codeNEPH { get; }


    }
}
