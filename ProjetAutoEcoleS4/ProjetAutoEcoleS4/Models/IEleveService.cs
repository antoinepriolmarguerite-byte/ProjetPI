using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Data;

namespace ProjetAutoEcoleS4.Interfaces
{
    internal class IEleveService
    {
        private List<Eleve> list_eleve;
        //private EleveService services;
        private EleveDAO bdd_Eleve;

        public IEleveService(string port, string password)
        {
            this.bdd_Eleve = new EleveDAO();
            this.list_eleve = bdd_Eleve.GetAll(port, password);//Comment ça Derefence bordel
        }
        public string[] AjouterEleve()
        {
            string[] retour = new string[12];

            Console.WriteLine("Veuillez écrire votre CodeNEPH : ");
            string CodeNEPH = Console.ReadLine(); long intNEPH;
            while (string.IsNullOrWhiteSpace(CodeNEPH) || CodeNEPH.Length != 12 || !long.TryParse(CodeNEPH, out intNEPH))
            {
                Console.WriteLine("Mauvais format ! Veuillez recommencer : ");
                CodeNEPH = Console.ReadLine();
            }
            retour[0] = CodeNEPH;

            Console.WriteLine("Veuillez écrire votre nom : ");
            string nom = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nom) || nom.Length >= 50)
            {
                Console.WriteLine("Veuillez raccourcir le nom : ");
                nom = Console.ReadLine();
            }
            retour[1] = nom;


            Console.WriteLine("Veuillez écrire votre Prenom : ");
            string prenom = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(prenom) || prenom.Length >= 50)
            {
                Console.WriteLine("Veuillez raccourcir le prénom : ");
                prenom = Console.ReadLine();
            }
            retour[2] = prenom;

            Console.WriteLine("Veuillez écrire votre tel : ");
            string tel = Console.ReadLine(); int telInt;
            while (string.IsNullOrWhiteSpace(tel) || tel.Length != 10 || !int.TryParse(tel, out telInt))
            {
                Console.WriteLine("Mauvais format ! Veuillez recommencer : ");
                tel = Console.ReadLine();
            }
            retour[3] = tel;

            Console.WriteLine("Veuillez écrire votre Mail : ");
            string Mail = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Mail) || Mail.Length >= 50)
            {
                Console.WriteLine("Veuillez raccourcir le Mail : ");
                Mail = Console.ReadLine();
            }
            retour[4] = Mail;

            Console.WriteLine("Veuillez écrire l'Adresse de l'élève : ");
            string adresse = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(adresse) || adresse.Length >= 50)
            {
                Console.WriteLine("Veuillez raccourcir l'adresse : ");
                adresse = Console.ReadLine();
            }
            retour[5] = adresse;

            Console.WriteLine("Veuillez écrire le RIB de l'élève : ");
            string rib = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(rib) || rib.Length >= 50)
            {
                Console.WriteLine("Veuillez raccourcir le rib : ");
                rib = Console.ReadLine();
            }
            retour[6] = rib;

            Console.WriteLine("Veuillez écrire la date de naissance de l'élève : ");
            string DateNaissance = Console.ReadLine(); DateTime Date;
            while (string.IsNullOrWhiteSpace(DateNaissance) || DateNaissance.Length >= 50 || !DateTime.TryParse(DateNaissance, out Date))
            {
                Console.WriteLine("Veuillez raccourcir la DateNaissance(format : jj/mm/aaaa) : ");
                DateNaissance = Console.ReadLine();
            }
            retour[7] = DateNaissance;

            Console.WriteLine("Veuillez écrire le Permis de l'élève : ");
            string Permis = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Permis) || Permis.Length >= 50)
            {
                Console.WriteLine("Veuillez raccourcir la Permis : ");
                Permis = Console.ReadLine();
            }
            retour[8] = Permis;

            Console.WriteLine("Veuillez écrire EstBoiteManuelle de l'élève (true/false) : ");
            string EstBoiteManuelle = Console.ReadLine().ToLower();
            while (string.IsNullOrWhiteSpace(EstBoiteManuelle) || EstBoiteManuelle != "true" && EstBoiteManuelle != "false")
            {
                Console.WriteLine(EstBoiteManuelle);
                EstBoiteManuelle = Console.ReadLine();
            }
            retour[9] = EstBoiteManuelle;

            Console.WriteLine("Veuillez écrire le nom du moniteur de l'élève (true/false) : ");
            string MoniteurTitre = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(MoniteurTitre) || MoniteurTitre.Length > 50)
            {
                Console.WriteLine("Veuillez raccourcir le nom du moniteur : ");
                MoniteurTitre = Console.ReadLine();
            }
            retour[10] = MoniteurTitre;

            string NbHeureARegler = "0";
            retour[11] = NbHeureARegler;

            return retour;




        }
        /*
        //bool VerifierEligibilite(Client client);
        // Récupérer tous les clients (pour les afficher dans la console)
        List<Eleve> RecupEleve();

        // Récupérer un seul Eleve par son code NEPH
        Eleve RecupEleveParNEPH(string codeNeph); 

        // Ajouter un nouveau Eleve
        bool AjouterEleve(Eleve Eleve);

        // Mettre à jour les infos d'un Eleve
        bool ModifierEleve(Eleve Eleve);

        // Supprimer un Eleve
        bool SupprimerEleve(string codeNeph); */
    }
}
