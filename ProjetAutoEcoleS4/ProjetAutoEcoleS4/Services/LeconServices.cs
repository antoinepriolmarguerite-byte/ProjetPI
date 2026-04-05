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
            if (!int.TryParse(Console.ReadLine(), out id) || id> Eleveservice.list_eleve.Count()-1 || id<1)
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
                    idMoniteur.Add(ListeMoniteur[i].id_moniteur); 
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
                l.dateLecon = date;

                if (lecondao.VerifierLeconEleve(e.codeNeph, date))
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
                e.nbHeuresAPayer++;

                lecondao.AjouterLecon_DAO(l);//C'est utile aussi
            }
        }

        public void SupprimerLeçon()
        {
            LeconDAO leconbdd = new LeconDAO(port,password);
            List<Lecon> ListeLecon = leconbdd.GetAll(port,password);

            for(int i = 0; i < ListeLecon.Count(); i++)
            {
                Console.WriteLine(ListeLecon[i].ToString());
            }
            Console.Write("Veuillez choisir une leçon : ");
            int idlecon = 0;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out idlecon))
                {
                    Console.Write("Veuillez entrer un id valide : ");
                }
            } while (idlecon <= 0);

            leconbdd.SupprimerLecon_DAO(idlecon);
        }
    }
}
