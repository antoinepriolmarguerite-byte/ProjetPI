using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Facture
    {
        private int id_Facture;//Clé primaire
        private string destinataire;
        private string eleve; //Auto ou manuelle
        private int montant;
        private DateTime deadtimeReglement;
        private DateTime dateseance;
        private string typereglement;
        private string codeNEPH; //clef étrangère
    }
}
