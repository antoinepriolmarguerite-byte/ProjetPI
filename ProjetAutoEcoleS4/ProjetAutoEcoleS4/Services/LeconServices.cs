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
            l.Date = date;
            Console.WriteLine("Donnez le code NEPH de l'élève : ");
            string codeNeph;
            do
            {
                codeNeph = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(codeNeph))
                {
                    Console.Write("Le code NEPH de l'élève ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(codeNeph));
            l.Eleve = clientservices.CreerEleve(port, password);
            clientservices.AjouterEleve(l.Eleve, port, password);

            l.Eleve = eleve;
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
            l.Moniteur = moniteur;
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
            l.Vehicule = vehicule;
            Console.WriteLine("Donnez le montant de la facture pour la leçon : ");
            double montantFacture;
            do
            {
                if (!double.TryParse(Console.ReadLine(), out montantFacture) || montantFacture < 0)
                {
                    Console.Write("Veuillez entrer un montant valide : ");
                }
            } while (montantFacture < 0);
            l.MontantFacture = montantFacture;

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
