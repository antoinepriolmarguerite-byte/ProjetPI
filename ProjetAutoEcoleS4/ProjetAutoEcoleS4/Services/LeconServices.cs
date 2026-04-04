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

        public void Ajouterleçon(Lecon l) //Bon cette méthode marche plus
        {
            Console.Clear();
            EleveService clientservices = new EleveService(port,password);
            LeconDAO lecondao = new LeconDAO(port, password);

            Console.WriteLine("Donnez la date et l'heure de la leçon : ");
            DateTime date;
            do
            {

                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Veuillez entrer une date valide (jj/mm/aaaa h:m:s) :");
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
                else if(lecondao.VerifierLeconEleve(codeNeph, date))
                {
                    Console.WriteLine("Il existe déjà une leçon pour cet élève à cette date. Veuillez ressaisir votre leçon : ");
                    Ajouterleçon(l);
                }
            } while (string.IsNullOrWhiteSpace(codeNeph));
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
                else if (lecondao.VerifierLeconMoniteur(moniteur, date))
                {
                    Console.WriteLine("Il existe déjà une leçon pour ce moniteur à cette date. Veuillez ressaisir votre leçon : ");
                    Ajouterleçon(l);
                }
            } while (string.IsNullOrWhiteSpace(moniteur) );
            //l.moniteur = moniteur;
            Console.WriteLine("Donnez l'immatricule du véhicule pour la leçon : ");
            int vehicule = 0;
            do
            {
                vehicule = int.Parse(Console.ReadLine());
                if (vehicule<0)
                {
                    Console.WriteLine("L'immatricule du véhicule ne peut pas être vide. Veuillez réessayer : ");
                }
                else if (lecondao.VerifierLeconVehicule(vehicule, date))
                {
                    Console.WriteLine("Il existe déjà une leçon pour ce véhicule à cette date. Veuillez ressaisir votre leçon : ");
                    Ajouterleçon(l);
                }
            } while (vehicule<0);
            l.vehicule = new Vehicule();
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
            MoniteurService MS = new MoniteurService(port, password);
            MoniteurDAO bddMoniteur = new MoniteurDAO(port,password);
            List<int> idMoniteur = new List<int>();
            List<Moniteur> ListeMoniteur = bddMoniteur.GetAll(port, password);


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
                Console.WriteLine("== MONITEURS ==");
                MS.AfficherAllMoniteur(port,password);
                for(int i = 0; i < ListeMoniteur.Count(); i++)
                {
                    idMoniteur.Add(ListeMoniteur[i].id_Moniteur); 
                }
                Console.Write("Veuillez choisir un moniteur : ");
                int entreeUtilisateur = int.Parse(Console.ReadLine())!; //Jvous laisse faire le tryparse, chepa faire
                while (!idMoniteur.Contains(entreeUtilisateur))
                {
                     Console.Write("Veuillez choisir l'id du moniteur : ");
                     entreeUtilisateur = int.Parse(Console.ReadLine())!; // Les ! permettent de caché les warnings
                }
                l.id_moniteur = entreeUtilisateur;
                
                Console.Write("Donnez la date de la leçon : ");
                DateTime date;
                do
                {

                    if (!DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        Console.Write("Veuillez entrer une date valide (jj-mm-aaaa HH:mm:ss) :");
                    }
                } while (date == default(DateTime));
                l.date_Lecon = date;

                if (lecondao.VerifierLeconEleve(e.CodeNEPH, date))
                {
                    Console.WriteLine("Erreur : Cet élève a déjà une leçon prévue à cette date !");
                    return;
                }

                Console.WriteLine("Donnez l'id du véhicule pour la leçon : ");
                Vehicule vehicule = new Vehicule();
                do
                {
                    vehicule.id_vehicule = int.Parse(Console.ReadLine())!;
                    if (vehicule.id_vehicule < 0)
                    {
                        Console.WriteLine("L'id du véhicule ne peut pas être inférieur à 0. Veuillez réessayer : ");
                    }
                } while (vehicule.id_vehicule<0);
                l.vehicule.id_vehicule = vehicule.id_vehicule;

                Console.WriteLine("Veuillez entrer l'immatricule du véhicule : ");
                do
                {
                    vehicule.immatriculation = Console.ReadLine()!;
                    if (vehicule.immatriculation==null)
                    {
                        Console.WriteLine("L'immatricule du véhicule ne peut pas être vide. Veuillez réessayer : ");
                    }
                } while (vehicule.immatriculation==null);
                l.vehicule.immatriculation = vehicule.immatriculation;

                Console.WriteLine("Veuillez entrer le montant de la facture : ");
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

                lecondao.AjouterLecon_DAO(l);//C'est utile aussi
            }
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
