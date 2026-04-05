using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Services;

namespace ProjetAutoEcoleS4.Interfaces
{
    internal class IEleveService
    {
        private List<Eleve> list_eleve;
        //private EleveService services;
        private EleveDAO bdd_Eleve;
        
        public IEleveService(string port,string password)
        {
            this.bdd_Eleve = new EleveDAO();
            this.list_eleve = bdd_Eleve.GetAll(port,password);
        }
        public string[] AjouterEleve(string port,string password)
        {
            string[] retour = new string[13];
            MoniteurService MS = new MoniteurService(port, password);
            MoniteurDAO bddMoniteur = new MoniteurDAO(port,password);
            List<int> idMoniteur = new List<int>();
            List<Moniteur> ListeMoniteur = bddMoniteur.GetAll(port, password);

            Console.Write("Veuillez écrire votre CodeNEPH : ");
            string codeNEPH = Console.ReadLine()!; long intNEPH;
            while (string.IsNullOrWhiteSpace(codeNEPH) || codeNEPH.Length != 12 || !long.TryParse(codeNEPH, out intNEPH))
            {
                Console.Write("Mauvais format ! Veuillez recommencer (Il faut 12 chiffre pour qu'il soit valide): ");
                codeNEPH = Console.ReadLine()!;
            }
            retour[0] = codeNEPH;

            Console.Write("Veuillez écrire votre nom : ");
            string nom = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(nom)|| nom.Length >= 50 ) 
            {
                Console.Write("Veuillez raccourcir le nom : ");
                nom = Console.ReadLine()!;
            }
            retour[1] = nom;


            Console.Write("Veuillez écrire votre Prenom : ");
            string prenom = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(prenom)|| prenom.Length >= 50 ) 
            {
                Console.Write("Veuillez raccourcir le prénom : ");
                prenom = Console.ReadLine()!;
            }
            retour[2] = prenom;

            Console.Write("Veuillez écrire votre tel : ");
            string tel = Console.ReadLine()!; int telInt;
            while (string.IsNullOrWhiteSpace(tel)|| tel.Length != 10 ||!int.TryParse(tel, out telInt )) 
            {
                Console.Write("Mauvais format ! Veuillez recommencer : ");
                tel = Console.ReadLine()!;
            }
            retour[3] = tel;

            Console.Write("Veuillez écrire votre Mail : ");
            string Mail = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(Mail)|| Mail.Length >= 50 ) 
            {
                Console.Write("Veuillez raccourcir le Mail : ");
                Mail = Console.ReadLine()!;
            }
            retour[4] = Mail;   

            Console.Write("Veuillez écrire le Type de l'élève (Traditionnel,AAC,Candidat libre) : ");
            string typeEleve = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(typeEleve)|| typeEleve.Length >= 50 ) 
            {
                Console.Write("Veuillez raccourcir le Mail : ");
                typeEleve = Console.ReadLine()!;
            }
            retour[5] = typeEleve;   

            Console.Write("Veuillez écrire l'Adresse de l'élève : ");
            string adresse = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(adresse)|| adresse.Length >= 50 ) 
            {
                Console.Write("Veuillez raccourcir l'adresse : ");
                adresse = Console.ReadLine()!;
            } 
            retour[6] = adresse;  

            Console.Write("Veuillez écrire le RIB de l'élève : ");
            string rib = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(rib)|| rib.Length >= 50 ) 
            {
                Console.Write("Veuillez raccourcir le rib : ");
                rib = Console.ReadLine()!;
            }
            retour[7] = rib;         

            Console.Write("Veuillez écrire la date de naissance de l'élève : ");
            string DateNaissance = Console.ReadLine()!;DateTime Date;
            while (string.IsNullOrWhiteSpace(DateNaissance)|| DateNaissance.Length >= 50 || !DateTime.TryParse(DateNaissance, out Date))
            {
                Console.Write("Veuillez raccourcir la DateNaissance(format : jj/mm/aaaa) : ");
                DateNaissance = Console.ReadLine()!;
            } 
            retour[8] = DateNaissance; 

            Console.Write("Veuillez écrire le Permis de l'élève : ");
            string Permis = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(Permis)|| Permis.Length >= 50 ) 
            {
                Console.Write("Veuillez raccourcir la Permis : ");
                Permis = Console.ReadLine()!;
            }   
            retour[9] = Permis;

            Console.Write("Veuillez écrire EstBoiteManuelle de l'élève (true/false) : ");
            string EstBoiteManuelle = Console.ReadLine()!.ToLower();
            while (string.IsNullOrWhiteSpace(EstBoiteManuelle)|| EstBoiteManuelle != "true" && EstBoiteManuelle != "false") 
            {
                Console.Write(EstBoiteManuelle);
                EstBoiteManuelle = Console.ReadLine()!;
            }   
            retour[10] = EstBoiteManuelle; 

            Console.WriteLine("\n== MONITEURS ==");
                MS.AfficherAllMoniteur(port,password);
                for(int i = 0; i < ListeMoniteur.Count(); i++)
                {
                    idMoniteur.Add(ListeMoniteur[i].id_moniteur); 
                }
                Console.Write("Veuillez choisir un moniteur : ");
                int entreeUtilisateur = int.Parse(Console.ReadLine()!); //Jvous laisse faire le tryparse, chepa faire
                while (!idMoniteur.Contains(entreeUtilisateur))
                {
                     Console.Write("Veuillez choisir l'id du moniteur : ");
                     entreeUtilisateur = int.Parse(Console.ReadLine()!); // Les ! permettent de caché les warnings
                }
            retour[11] = entreeUtilisateur+"";

            string NbHeureARegler = "0"; 
            retour[12] = NbHeureARegler;

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
