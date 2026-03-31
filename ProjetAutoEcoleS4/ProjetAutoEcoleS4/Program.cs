using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
using ProjetAutoEcoleS4.Services;

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

    switch (choix)
    {
        case "1":
            LeconServices lecon1 = new LeconServices(port, pwd);
            lecon1.Ajouterleçon(new Lecon());
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
            Eleve eleve = new Eleve();
            EleveDAO dao = new EleveDAO();
            eleve.afficheralleleve(port, pwd);

            List<Eleve> liste = dao.GetAll(port, pwd);
            Console.WriteLine("Chosisissez le numéro de l'élève que vous souhaitez connaitre le montant à régler");
            int id;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                {
                    Console.WriteLine("Veuillez entrer un montant valide :");
                }
            } while (id < 0 && id > liste.Count);
            eleve = liste[id - 1];
            Console.WriteLine("Le montant à régler pour l'élève " + eleve.Nom + " est de : " + eleve.MontantReglementRestant + "€");
            break;
        case "5":
            Vehicule vehicule = new Vehicule();
            VehiculeDAO vehiculeDAO = new VehiculeDAO();
            vehicule.afficherallvehicule(port, pwd);
            List<Vehicule> listeVehicules = vehiculeDAO.GetAll(port, pwd);
            Console.WriteLine("Chosisissez le numéro du véhicule que vous souhaitez connaitre le kilométrage");
            int idv;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out idv) || idv < 0)
                {
                    Console.WriteLine("Veuillez entrer un montant valide :");
                }
            } while (idv < 0 && idv > listeVehicules.Count);
            vehicule = listeVehicules[idv - 1];
            Console.WriteLine("Le kilométrage du véhicule " + vehicule.marque + " " + vehicule.modele + " est de : " + 0 + "km");
            break;
        case "7":
            Console.WriteLine("Chiffre d'affaires mensuel...");
            Console.Clear();
            break;
        case "8":
            Console.WriteLine("Ajout d'un élève ...");
            Console.Clear();
            break;
        case "9":
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
    Console.WriteLine("7 - Chiffre mensuel");
    Console.WriteLine("8 - Ajoutez Eleve");
    Console.WriteLine("9 - Quitter");
}

