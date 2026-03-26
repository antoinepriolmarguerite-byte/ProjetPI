using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProjetAutoEcoleS4.Models
{
    internal class Lecon
    {
        private static int idCounter = 1; // Compteur pour générer des ID uniques
        public int id_Lecon { get; set; } //Clé primaire
        public DateTime date { get; set; }
        public Eleve eleve { get; set; } //Auto ou manuelle
        public string moniteur { get; set; }
        public string vehicule { get; set; }
        public double montantFacture { get; set; }

        public int Id_Lecon
        {
            get { return id_Lecon; }
            set { id_Lecon = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public Eleve Eleve
        {
            get { return eleve; }
            set { eleve = value; }
        }
        public string Moniteur
        {
            get { return moniteur; }
            set { moniteur = value; }
        }
        public string Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }
        public double MontantFacture
        {
            get { return montantFacture; }
            set { montantFacture = value; }
        }
        public Lecon(DateTime date, Eleve eleve, string moniteur, string vehicule, double montantFacture)
        {
            id_Lecon = idCounter;
            idCounter++;
            this.date = date;
            this.eleve = eleve;
            this.moniteur = moniteur;
            this.vehicule = vehicule;
            this.montantFacture = montantFacture;
        }
        public Lecon()
        {
            id_Lecon = idCounter;
            idCounter++;
            date = new DateTime();
            eleve = new Eleve();
            moniteur = "";
            vehicule = "";
            montantFacture = 0;
        }

        public void Ajouterleçon(Lecon l,string port,string password)
        {
            EleveService clientservices = new EleveService();
            Lecon_DAO lecondao = new Lecon_DAO(port,password);

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
            /*do
            {
                codeNeph = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(codeNeph))
                {
                    Console.Write("Le code NEPH de l'élève ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(codeNeph));*/
            l.Eleve=clientservices.CreerEleve(port,password);
            clientservices.AjouterEleve(l.Eleve,port,password);

            //l.Eleve = eleve;
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

        public void SupprimerLeçon(string port, string password)
        {
            Lecon_DAO lecondao = new Lecon_DAO(port,password);
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