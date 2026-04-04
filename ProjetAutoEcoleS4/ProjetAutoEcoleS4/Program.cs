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
EleveService eleve = new EleveService(port, pwd);
Eleve e = new Eleve();
EleveDAO dao = new EleveDAO();
while (continuer)
{
    AfficherMenu();
    Console.Write("\nVotre choix (1-9) : ");
    string choix = Console.ReadLine();

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

            eleve.AfficherAllEleve(port, pwd);

            List<Eleve> liste = dao.GetAll(port, pwd);
            Console.WriteLine("Chosisissez le numéro de l'élève que vous souhaitez connaitre le montant à régler");
            int id;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                {
                    Console.WriteLine("Veuillez entrer un numéro valide :");
                }
            } while (id < 0 && id > liste.Count);
            e = liste[id];
            Console.WriteLine("Le montant à régler pour l'élève " + e.nomEleve + " est de : " + e.montantReglementRestant + "EUR");
            Thread.Sleep(2500);
            break;
        case "5":
            Vehicule vehicule = new Vehicule();
            VehiculeDAO vehiculeDAO = new VehiculeDAO(port,pwd);
            vehicule.afficherallvehicule(port, pwd);
            List<Vehicule> listeVehicules = vehiculeDAO.GetAll(port, pwd);
            Console.WriteLine("Chosisissez le numéro du véhicule que vous souhaitez connaitre le kilométrage");
            int idv;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out idv) || idv < 0)
                {
                    Console.WriteLine("Veuillez entrer un numéro valide :");
                }
            } while (idv < 0 && idv > listeVehicules.Count);
            vehicule = listeVehicules[idv - 1];
            double nbrkilometre=vehiculeDAO.Nbrkilometre(idv,  port,  pwd);
            Console.WriteLine("Le kilométrage du véhicule " + vehicule.marque + " " + vehicule.modele + " est de : " + nbrkilometre + "km");
            Thread.Sleep(2500);
            break;
        case "6":
            

            do {
                Console.WriteLine("Voulez-vous connaitre le nombre d'heures d'un moniteur ou d'un élève ?");
                Console.WriteLine("1 - Moniteur");
                Console.WriteLine("2 - Eleve");
                choix = Console.ReadLine();
                if (choix != "1" && choix != "2") { 
                    Console.WriteLine("Veuillez entrer un choix valide :");
                }
            } while (choix != "1" && choix != "2");
            if (choix == "1")
            {
                Moniteur moniteur = new Moniteur();
                MoniteurDAO moniteurDAO = new MoniteurDAO(port, pwd);
                MoniteurService moniteurService = new MoniteurService(port, pwd);
                moniteurService.AfficherAllMoniteur(port, pwd);
                List<Moniteur> listmon = moniteurDAO.GetAll(port, pwd);
                Console.WriteLine("Chosisissez le numéro du moniteur que vous souhaitez connaitre le nombre d'heures");
                int idmon;
                do
                {
                    if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                    {
                        Console.WriteLine("Veuillez entrer un numéro valide :");
                    }
                } while (id < 0 && id > listmon.Count);
                moniteur = listmon[id-1];
                int nbrheuremoniteur = moniteurDAO.NbrheureMoniteur(id, port, pwd);
                Console.WriteLine("Le nombre d'heures du moniteur " + moniteur.nom + " " + moniteur.prenom + " est de : " + nbrheuremoniteur + "h");
                Thread.Sleep(2500);
            }
            else if (choix == "2")
            {
                eleve.AfficherAllEleve(port, pwd);
                List<Eleve> listeeleve = dao.GetAll(port, pwd);
                Console.WriteLine("Chosisissez le numéro de l'élève que vous souhaitez connaitre le nombre d'heures");
                int ideleve;
                do
                {
                    if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                    {
                        Console.WriteLine("Veuillez entrer un numéro valide :");
                    }
                } while (id < 0 && id > listeeleve.Count);
                e = listeeleve[id-1];
                int nbrheureeleve = dao.NbrheureEleve(id, port, pwd);
                Console.WriteLine("Le nombre d'heures de l'élève " + e.nomEleve + " " + e.prenomEleve + " est de : " + nbrheureeleve + "h");
            }
            
            Thread.Sleep(2500);
            break;
        case "7":
            LeconDAO leconDAO = new LeconDAO(port, pwd);
            Console.WriteLine("Donnez le mois que vous souhaitez regarder le chiffre d'affaire :");
            int Mois;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out Mois) || Mois < 1 || Mois>12)
                {
                    Console.Write("Veuillez entrer un nombre valide : ");
                }
            } while (Mois<1 || Mois >12);
            Console.WriteLine("Donnez l'année que vous souhaitez regarder le chiffre d'affaire :");
            int anne;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out anne) || anne < 0)
                {
                    Console.Write("Veuillez entrer un nombre valide : ");
                }
            } while (anne<0);
            double chiffremensuel=leconDAO.Chiffremensuel(anne, Mois);
            Console.WriteLine("\nLe chiffre d'affaire du mois " + Mois + " de l'année " + anne + " est de : " + chiffremensuel + "EUR");
            Thread.Sleep(2500);
            Console.Clear();
            break;
        case "8":
            Console.WriteLine("Ajout d'un élève ...");
            EleveService eleveService = new EleveService(port,pwd);
            eleveService.CreerEleve(port,pwd);
            eleveService.AjouterEleve(e,port,pwd);
            Console.Clear();
            break;
        case "9":
            Console.Clear();
            VehiculeServices ajvehicule= new VehiculeServices();
            ajvehicule.AjouterVehicule(new Vehicule(), port, pwd);
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
    Console.WriteLine("8 - Ajoutez Eleve");
    Console.WriteLine("9 - Ajoutez Véhicule");
    Console.WriteLine("10 - Quitter");
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
    Console.WriteLine("8 - Ajoutez Eleve");
    Console.WriteLine("9 - Ajoutez Véhicule");
    Console.WriteLine("10 - Quitter");
}
