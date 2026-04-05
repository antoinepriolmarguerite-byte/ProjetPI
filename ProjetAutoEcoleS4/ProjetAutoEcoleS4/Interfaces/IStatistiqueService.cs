using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4
{
    internal class IStatistiqueService
    {
        string port;
        string pwd;

        public IStatistiqueService(string port, string pwd)
        {
            this.port = port;
            this.pwd = pwd;
        }

        public void AfficherKiloVehicule()
        {
            Vehicule vehicule = new Vehicule();
            VehiculeDAO vehiculeDAO = new VehiculeDAO(port, pwd);
            vehicule.afficherallvehicule(port, pwd);
            List<Vehicule> listeVehicules = vehiculeDAO.GetAll();
            Console.Write("Chosisissez le numéro du véhicule que vous souhaitez connaitre le kilométrage : ");
            int idv;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out idv) || idv < 0)
                {
                    Console.WriteLine("Veuillez entrer un numéro valide : ");
                }
            } while (idv < 0 && idv > listeVehicules.Count - 1);
            Console.Write("Donnez le mois que vous souhaitez regarder le kilométrage : ");
            int Moiskilo;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out Moiskilo) || Moiskilo < 1 || Moiskilo > 12)
                {
                    Console.Write("Veuillez entrer un nombre valide : ");
                }
            } while (Moiskilo < 1 || Moiskilo > 12);
            Console.Write("Donnez l'année que vous souhaitez regarder le kilométrage : ");
            int annekilo;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out annekilo) || annekilo < 0)
                {
                    Console.Write("Veuillez entrer un nombre valide : ");
                }
            } while (annekilo < 0);

            for (int i = 0; i < listeVehicules.Count(); i++)
            {
                if (listeVehicules[i].id_vehicule == idv) vehicule = listeVehicules[i];
            }
            //vehicule = listeVehicules[idv];
            double nbrkilometre = vehiculeDAO.Nbrkilometre(idv, annekilo, Moiskilo);
            Console.WriteLine("Le kilométrage du véhicule " + vehicule.marque + " " + vehicule.modele + " est de : " + nbrkilometre + "km");
            Thread.Sleep(2500);
        }

        public void AfficherHeureEleveMoni()
        {
            string choix;
            int id;
            EleveService eleve = new EleveService(port, pwd);
            Eleve e = new Eleve();
            EleveDAO dao = new EleveDAO(port, pwd);

            do
            {
                Console.WriteLine("Voulez-vous connaitre le nombre d'heures d'un moniteur ou d'un élève ?");
                Console.WriteLine("1 - Moniteur");
                Console.WriteLine("2 - Eleve");
                choix = Console.ReadLine()!;
                if (choix != "1" && choix != "2")
                {
                    Console.Write("Veuillez entrer un choix valide : ");
                }
            } while (choix != "1" && choix != "2");
            if (choix == "1")
            {
                Moniteur moniteur = new Moniteur();
                MoniteurDAO moniteurDAO = new MoniteurDAO(port, pwd);
                MoniteurService moniteurService = new MoniteurService(port, pwd);
                moniteurService.AfficherAllMoniteur();
                List<Moniteur> listmon = moniteurDAO.GetAll();
                Console.WriteLine("Chosisissez le numéro du moniteur que vous souhaitez connaitre le nombre d'heures");

                Moniteur m = new Moniteur();
                int idmon = 0;
                do
                {
                    if (!int.TryParse(Console.ReadLine(), out idmon) || idmon < 0)
                    {
                        Console.Write("Veuillez entrer un numéro valide : ");
                    }
                } while (idmon < 0 && idmon > listmon.Count);
                for (int i = 0; i < listmon.Count(); i++)
                {
                    if (listmon[i].id_moniteur == idmon) m = listmon[i];
                }
                int nbrheuremoniteur = moniteurDAO.NbrheureMoniteur(idmon, port, pwd);
                Console.WriteLine("Le nombre d'heures du moniteur " + moniteur.nom + " " + moniteur.prenom + " est de : " + nbrheuremoniteur + "h");
                Thread.Sleep(2500);
            }
            else if (choix == "2")
            {
                eleve.AfficherAllEleve();
                List<Eleve> listeeleve = dao.GetAll();
                Console.Write("Choisissez le numéro de l'élève que vous souhaitez connaitre le nombre d'heures : ");
                do
                {
                    if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                    {
                        Console.Write("Veuillez entrer un numéro valide : ");
                    }
                } while (id < 0 && id > listeeleve.Count);
                for (int i = 0; i < listeeleve.Count(); i++)
                {
                    if (listeeleve[i].id_eleve == id) e = listeeleve[i];
                }
                int nbrheureeleve = dao.NbrheureEleve(id);
                Console.WriteLine("Le nombre d'heures de l'élève " + e.nomEleve + " " + e.prenomEleve + " est de : " + nbrheureeleve + "h");
            }
            Thread.Sleep(2500);
        }

        public void AfficherCAmensuel()
        {
            LeconDAO leconDAO = new LeconDAO(port, pwd);
            Console.Write("Donnez le mois que vous souhaitez regarder le chiffre d'affaire : ");
            int Mois;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out Mois) || Mois < 1 || Mois > 12)
                {
                    Console.Write("Veuillez entrer un nombre valide : ");
                }
            } while (Mois < 1 || Mois > 12);
            Console.Write("Donnez l'année que vous souhaitez regarder le chiffre d'affaire : ");
            int anne;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out anne) || anne < 0)
                {
                    Console.Write("Veuillez entrer un nombre valide : ");
                }
            } while (anne < 0);
            double chiffremensuel = leconDAO.Chiffremensuel(anne, Mois);
            Console.WriteLine("\nLe chiffre d'affaire du mois " + Mois + " de l'année " + anne + " est de : " + chiffremensuel + "EUR");
            Thread.Sleep(2500);
        }
    }
}
