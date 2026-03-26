using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Models
{
    internal class Vehicule
    {
        private string immatriculation { get; set; } //Clé primaire
        private string typevehicule { get; set; }
        private bool boitevitesse { get; set; } //Auto (1) ou manuelle (0)
        private string historique { get; set; }
        private int coutAssurance { get; set; }
        private string marque { get; set; }
        private string modele { get; set; }
    }
}
