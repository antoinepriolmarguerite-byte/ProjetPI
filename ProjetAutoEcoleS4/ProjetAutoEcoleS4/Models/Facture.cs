using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Facture
    {
        private int id_facture; //Clé primaire
        private string destinataire;
        private string nomEleve;
        private int montant;
        private DateTime deadlineReglement;
        private DateTime dateseance;
        private string typeReglement;
        private string id_eleve; //Clé étrangère
    }
}
