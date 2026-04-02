using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Data
{
    internal class LeconServices
    {
        Eleve eleve;
        string port;
        string password;

        public LeconServices(string port, string password)
        {
            this.port = port;
            this.password = password;
        }

        public void Ajouterleçon(Lecon l)
        {
            EleveService clientservices = new EleveService(port,password);
            LeconDAO lecondao = new LeconDAO(port, password);

            Console.WriteLine("Donnez la date de la leçon : ");
            DateTime date;
            do
            {

                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Veuillez entrer une date valide (jj/mm/aaaa) :");
                }
            } while (date == default(DateTime));
            l.date_Lecon = date;
            Console.WriteLine("Donnez le code NEPH de l'élève : ");
            string codeNeph;
            do
            {
                codeNeph = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(codeNeph))
                {
                    Console.Write("Le code NEPH de l'élève ne peut pas être vide. Veuillez réessayer : ");
                }

            } while (string.IsNullOrWhiteSpace(codeNeph) && !lecondao.VerifierLeconEleve(codeNeph, date));
            l.eleve = clientservices.CreerEleve(codeNeph, port, password);
            clientservices.AjouterEleve(l.eleve, port, password);
            l.eleve = eleve;
            Console.WriteLine("Donnez le nom du moniteur : ");
            string moniteur;
            do
            {
                moniteur = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(moniteur))
                {
                    Console.Write("Le nom du moniteur ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(moniteur) && !lecondao.VerifierLeconMoniteur(moniteur, date));
            l.moniteur = moniteur;
            Console.WriteLine("Donnez l'immatricule du véhicule pour la leçon : ");
            string vehicule;
            do
            {
                vehicule = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(vehicule))
                {
                    Console.WriteLine("L'immatricule du véhicule ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(vehicule) && !lecondao.VerifierLeconVehicule(vehicule, date));
            l.vehicule = vehicule;
            Console.WriteLine("Donnez le montant de la facture pour la leçon : ");
            double montantFacture;
            do
            {
                if (!double.TryParse(Console.ReadLine(), out montantFacture) || montantFacture < 0)
                {
                    Console.Write("Veuillez entrer un montant valide : ");
                }
            } while (montantFacture < 0);
            l.montantFacture = montantFacture;

            lecondao.AjouterLecon_DAO(l);
        }

        public void AjouterLeconAEleve(Lecon l)
        {
            EleveService Eleveservice = new EleveService(port,password);
            LeconDAO lecondao = new LeconDAO(port, password);
            MoniteurService MS = new MoniteurService();
            MoniteurDAO bddMoniteur = new MoniteurDAO(port,password);
            List<Moniteur> ListeMoniteur = bddMoniteur.GetAll(port, password);
            List<string> idMoniteur = new List<string>();
            

            
            for(int i = 0; i < Eleveservice.list_eleve.Count(); i++)
            {
                Console.WriteLine(Eleveservice.list_eleve[i].ToString());
            }
            Console.Write("Veuillez choisir un élève (en entrant le n° de l'élève) : ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre entier naturel : ");
            }
            Eleve e = Eleveservice.list_eleve[id-1];

            if (Eleveservice.EleveExiste(e))
            {
                l.eleve = e;
                Console.Write("Veuillez choisir un moniteur : ");
                MS.AfficherAllMoniteur(port,password);
                for(int i = 0; i < ListeMoniteur.Count(); i++)
                {
                    idMoniteur.Add(ListeMoniteur[i].id_Moniteur); 
                }
                string entreeUtilisateur = Console.ReadLine()!;
                while (!idMoniteur.Contains(entreeUtilisateur))
                {
                     Console.Write("Veuillez choisir l'id du moniteur (FORMAT : MONIT--) : ");
                     entreeUtilisateur = Console.ReadLine()!; // Les ! permettent de caché les warnings
                }
                
                Console.WriteLine("Donnez la date de la leçon : ");
                DateTime date;
                do
                {

                    if (!DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        Console.Write("Veuillez entrer une date valide (jj/mm/aaaa) :");
                    }
                } while (date == default(DateTime));
                l.date_Lecon = date;

                Console.WriteLine("Donnez l'immatricule du véhicule pour la leçon : ");
                string vehicule;
                do
                {
                    vehicule = Console.ReadLine().ToUpper()!;
                    if (string.IsNullOrWhiteSpace(vehicule))
                    {
                        Console.WriteLine("L'immatricule du véhicule ne peut pas être vide. Veuillez réessayer : ");
                    }
                } while (string.IsNullOrWhiteSpace(vehicule));
                l.vehicule = vehicule;

                double montantFacture;
                do
                {
                    if (!double.TryParse(Console.ReadLine(), out montantFacture) || montantFacture < 0)
                    {
                        Console.Write("Veuillez entrer un montant valide : ");
                    }
                } while (montantFacture < 0);
                l.montantFacture = montantFacture;
                e.NbHeureARegler++;

            }
            e.NbHeureARegler++;

            lecondao.AjouterLecon_DAO(l);
        }

        public void SupprimerLeçon()
        {
            LeconDAO lecondao = new LeconDAO(port, password);
            Console.Write("Donnez la date de la leçon : ");
            DateTime date;
            do
            {

                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Veuillez entrer une date valide (jj/mm/aaaa) :");
                }
            } while (date == default(DateTime));

            Console.Write("Donnez le code NEPH de l'élève : ");
            string codeNeph;
            do
            {
                codeNeph = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(codeNeph))
                {
                    Console.Write("Le code NEPH de l'élève ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(codeNeph));

            lecondao.SupprimerLecon_DAO(codeNeph, date);
        }
    }
}
