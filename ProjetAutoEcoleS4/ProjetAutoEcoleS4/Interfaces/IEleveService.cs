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
        
        public IEleveService(string port,string password)
        {
            this.list_eleve = bdd_Eleve.GetAll(port,password);
            this.bdd_Eleve = new EleveDAO();
        }
        public string[] AjouterEleve()
        {
            string[] retour = new string[13];

            Console.WriteLine("Veuillez écrire votre CodeNEPH : ");
            string CodeNEPH = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(CodeNEPH)|| CodeNEPH.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir le CodeNEPH : ");
                Console.ReadLine();
            }

            Console.WriteLine("Veuillez écrire votre nom : ");
            string nom = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nom)|| nom.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir le nom : ");
                Console.ReadLine();
            }

            Console.WriteLine("Veuillez écrire votre Prenom : ");
            string prenom = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(prenom)|| prenom.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir le prénom : ");
                Console.ReadLine();
            }

            Console.WriteLine("Veuillez écrire votre tel : ");
            string tel = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(tel)|| tel.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir le tel : ");
                Console.ReadLine();
            }

            Console.WriteLine("Veuillez écrire votre Mail : ");
            string Mail = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Mail)|| Mail.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir le Mail : ");
                Console.ReadLine();
            }

            Console.WriteLine("Veuillez écrire l'Adresse de l'élève : ");
            string adresse = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(adresse)|| adresse.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir l'adresse : ");
                Console.ReadLine();
            }   

            Console.WriteLine("Veuillez écrire le RIB de l'élève : ");
            string rib = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(rib)|| rib.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir le rib : ");
                Console.ReadLine();
            }         

            Console.WriteLine("Veuillez écrire la date de naissance de l'élève : ");
            string DateNaissance = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(DateNaissance)|| DateNaissance.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir la DateNaissance : ");
                Console.ReadLine();
            }  

            Console.WriteLine("Veuillez écrire le Permis de l'élève : ");
            string Permis = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Permis)|| Permis.Length >= 50 ) 
            {
                Console.WriteLine("Veuillez raccourcir la Permis : ");
                Console.ReadLine();
            }   

            Console.WriteLine("Veuillez écrire EstBoiteManuelle de l'élève (true/false) : ");
            string EstBoiteManuelle = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(EstBoiteManuelle)|| EstBoiteManuelle.Length >= 5 ) 
            {
                Console.WriteLine("Veuillez raccourcir la EstBoiteManuelle : ");
                Console.ReadLine();
            }    

            Console.WriteLine("Veuillez écrire le nom du moniteur de l'élève (true/false) : ");
            string MoniteurTitre = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(MoniteurTitre)|| MoniteurTitre.Length >= 5 ) 
            {
                Console.WriteLine("Veuillez raccourcir le nom du moniteur : ");
                Console.ReadLine();
            }    

            string NbHeureARegler = "0";   



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
