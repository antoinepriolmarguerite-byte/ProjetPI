using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjetAutoEcoleS4.Data
{
    internal class VehiculeServices
    {
        string port;
        string password;

        public VehiculeServices(string port, string password)
        {
            this.port = port;
            this.password = password;
        }

        public void AjouterVehicule(Vehicule v)
        {
            Console.Clear();
            VehiculeDAO vehiculedao = new VehiculeDAO(port, password);

            Console.WriteLine("Donnez l'immatriculation du véhicule : ");
            string immatriculation;
            do
            {

                immatriculation = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(immatriculation))
                {
                    Console.Write("L'immatriculation du véhicule ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(immatriculation));
            v.immatriculation = immatriculation;
            Console.WriteLine("Donnez le type du véhicule : ");
            string Type;
            do
            {
                Type = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(Type))
                {
                    Console.Write("Le type du véhicule ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(Type));
            v.typevehicule = Type;
            Console.WriteLine("Donnez la boite de vitesse du véhicule (auto ou manuelle) : ");
            string boitevitesse;
            do
            {
                boitevitesse = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(boitevitesse))
                {
                    Console.Write("Une voiture doit avoir sa boite de vitesse d'indiquer. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(boitevitesse) || boitevitesse!="AUTO" && boitevitesse!="MANUELLE");
            if(boitevitesse == "AUTO")
            {
                v.boitevitesse = true;
            }
            else
            {
                v.boitevitesse = false;
            }
            Console.WriteLine("Donnez la marque du véhicule : ");
            string marque;
            do
            {
                marque = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(marque))
                {
                    Console.WriteLine("Une voiture doit avoir sa marque d'indiquer. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(marque));
            v.marque = marque;
            Console.WriteLine("Donnez le modèle du véhicule : ");
            string modele;
            do
            {
                modele = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(modele))
                {
                    Console.WriteLine("Une voiture doit avoir son modèle d'indiquer. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(modele));
            v.modele = modele;
            vehiculedao.Ajouter(v);
        }

        public void SupprimerVehicule()
        {
            Console.Clear();
            VehiculeDAO vehiculedao = new VehiculeDAO(port, password);
            AfficherAllVehicule();
            Console.WriteLine("Donnez l'immatriculation du véhicule à supprimer : ");
            string immatriculation;
            do
            {

                immatriculation = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(immatriculation))
                {
                    Console.Write("L'immatriculation du véhicule ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(immatriculation));
            int id_vehicule = vehiculedao.FindVehicule(immatriculation);
            if (id_vehicule != 0)
            {
                vehiculedao.Supprimer(id_vehicule);
                Console.WriteLine("Le véhicule avec l'immatriculation " + immatriculation + " a été supprimé.");
            }
            else
            {
                Console.WriteLine("Aucun véhicule trouvé avec l'immatriculation " + immatriculation + ".");
            }
        }

        public void AfficherAllVehicule()
        {
            Console.Clear();
            VehiculeDAO vehiculedao = new VehiculeDAO(port, password);
            List<Vehicule> list_vehicule = vehiculedao.GetAll();
            Console.WriteLine("Voici la liste de tous les véhicules : ");
            foreach (Vehicule v in list_vehicule)
            {
                Console.WriteLine("ID : " + v.id_vehicule + " | Immatriculation : " + v.immatriculation + " | Type : " + v.typevehicule + " | Boite de vitesse : " + (v.boitevitesse ? "Automatique" : "Manuelle") + " | Marque : " + v.marque + " | Modèle : " + v.modele);
            }
        }
    }
}
