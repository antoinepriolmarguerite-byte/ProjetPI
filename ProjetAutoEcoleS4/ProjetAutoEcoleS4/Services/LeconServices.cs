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

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : Lecon l
        // TRAITEMENT : 
        //   - Gère le formulaire complet de création d'une leçon en console
        //   - Sélectionne l'élève, le moniteur et le véhicule
        //   - Valide la date, l'heure et le montant saisi
        //   - Met à jour le compteur d'heures et déclenche l'ajout en base
        // SORTIE     : aucune
        // ==========================================
        public void AjouterLeconAEleve(Lecon l)
        {
            EleveService Eleveservice = new EleveService(port, password);
            LeconDAO lecondao = new LeconDAO(port, password);
            MoniteurService MS = new MoniteurService(port, password);
            MoniteurDAO bddMoniteur = new MoniteurDAO(port, password);
            VehiculeServices VS = new VehiculeServices(port, password);
            VehiculeDAO bddVehicule = new VehiculeDAO(port, password);
            IVehiculeServices IVS = new IVehiculeServices(port, password);
            List<int> idMoniteurList = new List<int>();
            List<Moniteur> ListeMoniteur = bddMoniteur.GetAll();

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
                MS.AfficherAllMoniteur();
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
                IVS.AfficherAllVehicule();
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
                else if(!bddVehicule.VerifmodeleVehicule(idVehiculeSaisi, e.id_eleve))
                {
                    Console.WriteLine("Erreur : ce véhicule n'est pas adapté au type de permis de l'élève !");
                    return;
                }
                l.vehicule.id_vehicule = idVehiculeSaisi;

                // --- MONTANT FACTURE ---
                Console.Write("Veuillez entrer le montant de la facture (en euros) : ");
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

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : aucune
        // TRAITEMENT : 
        //   - Affiche la liste de toutes les leçons existantes
        //   - Demande la saisie d'un identifiant de leçon
        //   - Appelle le DAO pour supprimer la leçon sélectionnée
        // SORTIE     : aucune
        // ==========================================        
        public void SupprimerLeçon()
        {
            LeconDAO leconbdd = new LeconDAO(port, password);
            List<Lecon> ListeLecon = leconbdd.GetAll(port, password);

            for (int i = 0; i < ListeLecon.Count(); i++)
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
