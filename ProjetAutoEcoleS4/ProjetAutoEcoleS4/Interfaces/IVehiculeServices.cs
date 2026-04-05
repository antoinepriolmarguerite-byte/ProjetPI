using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Services
{
    internal class IVehiculeServices
    {
        string port;
        string password;
        VehiculeDAO bdd_vehicule;
        List<Vehicule> list_vehicule;

        public IVehiculeServices(string port, string password)
        {
            this.bdd_vehicule = new VehiculeDAO(port, password);
            this.list_vehicule = bdd_vehicule.GetAll();
            this.port = port;
            this.password = password;
        }

        public void AfficherAllVehicule()
        {
            Console.Clear();
            Console.WriteLine("Voici la liste de tous les véhicules : ");
            foreach (Vehicule v in list_vehicule)
            {
                Console.WriteLine("ID : " + v.id_vehicule + " | Immatriculation : " + v.immatriculation + " | Type : " + v.typevehicule + " | Boite de vitesse : " + (v.boitevitesse ? "Automatique" : "Manuelle") + " | Marque : " + v.marque + " | Modèle : " + v.modele);
            }
        }

        public void AfficherAjoutSuppVehicule()
        {
            VehiculeDAO dao = new VehiculeDAO(port, password);
            Vehicule v = new Vehicule();
            VehiculeServices vehiculeService = new VehiculeServices(port, password);
            Console.WriteLine("Voulez-vous ajouter ou supprimer un véhicule ?");
            Console.WriteLine("1 - Ajouter");
            Console.WriteLine("2 - Supprimer");
            string choix;
            do
            {
                choix = Console.ReadLine();
                if (choix != "1" && choix != "2")
                {
                    Console.Write("Veuillez entrer un choix valide : ");
                }
            } while (choix != "1" && choix != "2");
            if (choix == "1")
            {
                VehiculeServices ajvehicule = new VehiculeServices(port, password);
                ajvehicule.AjouterVehicule(new Vehicule());
            }
            else if (choix == "2")
            {
                vehiculeService.SupprimerVehicule();
            }
            Thread.Sleep(5000);
        }

    }
}
