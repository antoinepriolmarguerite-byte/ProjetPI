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
            EleveService Eleveservice = new EleveService(port, password);
            LeconDAO lecondao = new LeconDAO(port, password);
            MoniteurService MS = new MoniteurService(port, password);
            MoniteurDAO bddMoniteur = new MoniteurDAO(port, password);
            List<int> idMoniteurList = new List<int>();
            List<Moniteur> ListeMoniteur = bddMoniteur.GetAll(port, password);

            // --- DATE ---
            Console.Write("Donnez la date de la leçon (jj-mm-aaaa HH:mm:ss) : ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Format invalide. Veuillez entrer une date valide : ");
            }
            l.dateLecon = date;

            // --- CHOIX ÉLÈVE ---
            for (int i = 0; i < Eleveservice.list_eleve.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {Eleveservice.list_eleve[i].ToString()}");
            }

            Console.Write("Veuillez choisir un élève (n° dans la liste) : ");
            int choixEleve;
            while (!int.TryParse(Console.ReadLine(), out choixEleve) || choixEleve < 1 || choixEleve > Eleveservice.list_eleve.Count())
            {
                Console.Write($"Entrée invalide. Veuillez choisir un nombre entre 1 et {Eleveservice.list_eleve.Count()} : ");
            }
            Eleve e = Eleveservice.list_eleve[choixEleve - 1];

            if (Eleveservice.EleveExiste(e))
            {
                if (lecondao.VerifierLeconEleve(e.codeNeph, date))
                {
                    Console.WriteLine("Erreur : Cet élève a déjà une leçon prévue à cette date !");
                    return;
                }
                l.eleve = e;

                // --- CHOIX MONITEUR ---
                Console.WriteLine("== MONITEURS ==");
                MS.AfficherAllMoniteur(port, password);
                foreach (var m in ListeMoniteur) { idMoniteurList.Add(m.id_moniteur); }

                Console.Write("Veuillez entrer l'ID du moniteur : ");
                int idMoniteurSaisi;
                while (!int.TryParse(Console.ReadLine(), out idMoniteurSaisi) || !idMoniteurList.Contains(idMoniteurSaisi))
                {
                    Console.Write("ID inconnu ou invalide. Veuillez choisir un ID présent dans la liste : ");
                }

                if (lecondao.VerifierLeconMoniteur(idMoniteurSaisi.ToString(), date))
                {
                    Console.WriteLine("Erreur : ce moniteur a déjà une leçon prévue à cette date !");
                    return;
                }
                l.id_moniteur = idMoniteurSaisi;

                // --- CHOIX VÉHICULE ---
                Console.Write("Donnez l'ID du véhicule pour la leçon : ");
                int idVehiculeSaisi;
                while (!int.TryParse(Console.ReadLine(), out idVehiculeSaisi) || idVehiculeSaisi < 0)
                {
                    Console.Write("ID invalide. Veuillez entrer un nombre positif : ");
                }

                if (lecondao.VerifierLeconVehicule(idVehiculeSaisi, date))
                {
                    Console.WriteLine("Erreur : ce véhicule a déjà une leçon prévue à cette date !");
                    return;
                }
                l.vehicule.id_vehicule = idVehiculeSaisi;

                // --- MONTANT FACTURE ---
                Console.Write("Veuillez entrer le montant de la facture : ");
                double montantFacture;
                while (!double.TryParse(Console.ReadLine(), out montantFacture) || montantFacture < 0)
                {
                    Console.Write("Montant invalide. Veuillez entrer un nombre positif : ");
                }

                l.montantFacture = montantFacture;
                e.nbHeuresAPayer++;

                lecondao.AjouterLecon_DAO(l);
                Console.WriteLine("Leçon ajoutée avec succès !");
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
