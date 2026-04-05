using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Facture
    {
        public string id_facture { get; set; } //Clé primaire
        public string destinataire { get; set; }
        public string nomEleve { get; set; }
        public int montant { get; set; }
        public DateTime deadlineReglement { get; set; }
        public DateTime dateSeance { get; set; }
        public string typeReglement { get; set; }
        public int id_eleve { get; set; } //Clé étrangère

        public Facture(int id_eleve, DateTime date)
        {
            string dateFormat = "yyyyMMddHHmmss";
            this.id_facture = "FAC-" + DateTime.Now.ToString(dateFormat);

            this.destinataire = "";
            this.nomEleve = "";
            this.montant = 0;
            this.deadlineReglement = DateTime.Now.AddMonths(1);
            this.dateSeance = date;
            this.typeReglement = "En attente";
            this.id_eleve = id_eleve;
        }
    }
}
