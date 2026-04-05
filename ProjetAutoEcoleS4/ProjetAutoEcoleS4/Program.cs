using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Services;

//Bonjour, bienvenue dans notre projet de gestion d'auto-école !
//Nous avons tenu à réaliser ce projet avec le minimun d'utilisation de l'IA
// 95% du code que vous voyez n'a jamais vu une ligne d'IA.
// L'IA a été utilisée pour nous aider à corriger les bugs que nous avons rencontrés, 
// et pour nous aider à trouver des solutions à certains problèmes sur lesquels nous n'avions pas encore été formés
// Nous sommes conscient que ce projet n'est pas parfait, mais nous avons fait de notre mieux pour respecter les consignes 
// et pour fournir un projet fonctionnel et complet à temps.

Console.WriteLine("========== CONFIGURATION BDD ===========");
Console.Write("Port (3306 par défaut) : ");
string port = Console.ReadLine();
port = string.IsNullOrEmpty(port) ? "3306" : port;

Console.Write("Mot de passe SQL : ");
string pwd = Console.ReadLine();

Database db = new Database(port, pwd);
Console.WriteLine(db.TestConnection());
Console.WriteLine("========================================\n");

Console.WriteLine("==== Projet 2026 Auto-Ecole ====");
bool continuer = true;
while (continuer)
{
    AfficherMenu();
    Console.Write("\nVotre choix (1-9) : ");
    string choix = Console.ReadLine();
    Console.Clear();

    switch (choix)
    {
        case "1":
            LeconServices lecon1 = new LeconServices(port, pwd);
            lecon1.AjouterLeconAEleve(new Lecon());
            Console.Clear();
            break;
        case "2":
            LeconServices lecon2 = new LeconServices(port, pwd);
            lecon2.SupprimerLeçon();
            Console.Clear();
            break;
        case "3":
            PlanningService planning = new PlanningService(port, pwd);
            planning.AfficherPlanning();
            Console.Clear();
            break;
        case "4":
            IFacturationService IFS = new IFacturationService(port, pwd);
            IFS.AfficherVoirMontant();
            Console.Clear();
            break;
        case "5":
            IStatistiqueService IS1S = new IStatistiqueService(port, pwd);
            IS1S.AfficherKiloVehicule();
            Console.Clear();
            break;
        case "6":
            IStatistiqueService IS2S = new IStatistiqueService(port, pwd);
            IS2S.AfficherHeureEleveMoni();
            Console.Clear();
            break;
        case "7":
            IStatistiqueService IS3S = new IStatistiqueService(port, pwd);
            IS3S.AfficherCAmensuel();
            Console.Clear();
            break;
        case "8":
            IEleveService IES = new IEleveService(port, pwd);
            IES.AfficherAjoutSuppEleve();
            Console.Clear();
            break;
        case "9":
            IVehiculeServices IVS = new IVehiculeServices(port, pwd);
            IVS.AfficherAjoutSuppVehicule();
            Console.Clear();
            break;
        case "10":
            continuer = false;
            Console.Clear();
            break;
        default:
            Console.WriteLine("Option invalide, veuillez réessayer.");
            break;
    }
}

void AfficherMenu()
{
    Console.Clear();
    Console.WriteLine("\n========== MENU PRINCIPAL ==========");
    Console.WriteLine("1 - Ajouter une leçon");
    Console.WriteLine("2 - Supprimer une leçon");
    Console.WriteLine("3 - Voir le planning");
    Console.WriteLine("4 - Voir le montant à régler");
    Console.WriteLine("5 - Kilométrage véhicule");
    Console.WriteLine("6 - Heures élève/moniteur");
    Console.WriteLine("7 - Chiffre d'affaire mensuel");
    Console.WriteLine("8 - Ajoutez/Supprimer Eleve");
    Console.WriteLine("9 - Ajoutez/Supprimer Véhicule");
    Console.WriteLine("10 - Quitter");
}



