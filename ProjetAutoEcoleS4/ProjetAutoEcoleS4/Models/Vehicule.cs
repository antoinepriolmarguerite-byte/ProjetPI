using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAutoEcoleS4.Data;
namespace ProjetAutoEcoleS4.Models
{
    internal class Vehicule
    {
        private static int autoincr = 0;
        public int id_vehicule { get; set; }
        public string immatriculation { get; set; } //Clé primaire
        public string typevehicule { get; set; }
        public bool boitevitesse { get; set; } //Auto ou manuelle
        public string historique { get; set; }
        public int coutAssurance { get; set; }
        public string marque { get; set; }
        public string modele { get; set; }
        public bool etat { get; set; }
        public Vehicule(string immatriculation, string typevehicule, bool boitevitesse, string historique, int coutAssurance, string marque, string modele)
        {
            this.id_vehicule = autoincr++;
            this.immatriculation = immatriculation;
            this.typevehicule = typevehicule;
            this.boitevitesse = boitevitesse;
            this.historique = historique;
            this.coutAssurance = coutAssurance;
            this.marque = marque;
            this.modele = modele;
        }
        public Vehicule(string immatriculation)
        {

            this.id_vehicule = autoincr++;
            this.immatriculation = "";
            this.typevehicule = "";
            this.boitevitesse = false;
            this.historique = "";
            this.coutAssurance = 0;
            this.marque = "";
            this.modele = "";
        }
        public Vehicule()
        {
            this.id_vehicule = autoincr++;
            this.immatriculation = "";
            this.typevehicule = "";
            this.boitevitesse = false;
            this.historique = "";
            this.coutAssurance = 0;
            this.marque = "";
            this.modele = "";
        }
        public override string ToString()
        {

            return id_vehicule + " | Marque : " + marque + " | Modèle : " + modele + " | Immatriculation : " + immatriculation;
        }
        public void afficherallvehicule(string port, string password)
        {
            VehiculeDAO vehiculeDao = new VehiculeDAO(port,password);
            List<Vehicule> liste = vehiculeDao.GetAll();
            foreach (Vehicule e in liste)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
