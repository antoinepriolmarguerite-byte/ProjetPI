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
        private EleveDAO bdd_Eleve;
        string port;
        string password;

        public IEleveService(string port,string password)
        {
            this.bdd_Eleve = new EleveDAO(port, password);
            this.list_eleve = bdd_Eleve.GetAll();
            this.port = port;
            this.password = password;
        }

// ==========================================
// TYPE       : Méthode d'INSTANCE
// ENTRÉE     : aucune
// TRAITEMENT : 
//   - Affiche un formulaire de saisie complet dans la console
//   - Valide le format du CodeNEPH (12 chiffres)
//   - Propose une sélection interactive du moniteur référent
//   - Récupère les informations personnelles (Nom, Prénom, Mail, etc.)
// SORTIE     : string[] (tableau contenant toutes les données de l'élève saisies)
// ==========================================
        public string[] CreerEleve()
        {
            string[] retour = new string[13];
            MoniteurService MS = new MoniteurService(port, password);
            MoniteurDAO bddMoniteur = new MoniteurDAO(port,password);
            List<int> idMoniteur = new List<int>();
            List<Moniteur> ListeMoniteur = bddMoniteur.GetAll();

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

            Console.Write("Est-ce une boite manuelle ?(true/false) : ");
            string EstBoiteManuelle = Console.ReadLine()!.ToLower();
            while (string.IsNullOrWhiteSpace(EstBoiteManuelle)|| EstBoiteManuelle != "true" && EstBoiteManuelle != "false") 
            {
                Console.Write("Est-ce une boite manuelle ?(true/false) : ");
                EstBoiteManuelle = Console.ReadLine()!;
            }
            if(EstBoiteManuelle=="true") EstBoiteManuelle = "Manuelle";
            else EstBoiteManuelle = "Automatique";  
            retour[10] = EstBoiteManuelle; 

            Console.WriteLine("\n== MONITEURS ==");
                MS.AfficherAllMoniteur();
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

// ==========================================
// TYPE       : Méthode d'INSTANCE
// ENTRÉE     : aucune
// TRAITEMENT : 
//   - Affiche un menu de choix (Ajouter ou Supprimer)
//   - En cas d'ajout : appelle la création et l'insertion d'un élève
//   - En cas de suppression : affiche la liste et valide l'ID à supprimer
// SORTIE     : aucune
// ==========================================
        public void AfficherAjoutSuppEleve()
        {
            EleveDAO dao = new EleveDAO(port, password);
            Eleve e = new Eleve();
            EleveService eleveService = new EleveService(port, password);

            Console.WriteLine("Voulez-vous ajouter ou supprimer un élève ?");
            Console.WriteLine("1 - Ajouter");
            Console.WriteLine("2 - Supprimer");
            string choix;
            do
            {
                choix = Console.ReadLine();
                if (choix != "1" && choix != "2")
                {
                    Console.Write("Veuillez entrer un choix valide : ");
                }
            } while (choix != "1" && choix != "2");
            if (choix == "1")
            {
                e = eleveService.CreerEleve();
                eleveService.AjouterEleve(e);
            }
            else if (choix == "2")
            {
                eleveService.AfficherAllEleve();
                List<Eleve> listeeleve = dao.GetAll();
                Console.Write("Chosisissez le numéro de l'élève que vous souhaitez supprimer : ");
                int ideleve;
                do
                {
                    if (!int.TryParse(Console.ReadLine(), out ideleve) || ideleve < 0)
                    {
                        Console.Write("Veuillez entrer un numéro valide : ");
                    }
                } while (ideleve < 0 && ideleve > listeeleve.Count);
                eleveService.SupprimerEleve(ideleve);
            }
        }
    }
}
