using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Services;
using System;
using System.Collections.Generic;
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
            } while (string.IsNullOrWhiteSpace(codeNeph) && lecondao.VerifierLeconEleve(codeNeph, date));
            l.eleve = clientservices.CreerEleve(port, password);
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
            } while (string.IsNullOrWhiteSpace(moniteur) && lecondao.VerifierLeconMoniteur(moniteur, date));
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
            } while (string.IsNullOrWhiteSpace(vehicule) && lecondao.VerifierLeconVehicule(vehicule, date));
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

        public void AjouterLeconAEleve(Lecon l,Eleve e)
        {
            EleveService clientservices = new EleveService(port,password);
            LeconDAO lecondao = new LeconDAO(port, password);

            l.eleve = e;
            Console.WriteLine("Donnez le nom du moniteur : ");
            string moniteur;
            do
            {
                moniteur = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(moniteur))
                {
                    Console.Write("Le nom du moniteur ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(moniteur));
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
            } while (string.IsNullOrWhiteSpace(vehicule));
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
