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
            AfficherVoirMontant();
            Console.Clear();
            break;
        case "5":
            AfficherKiloVehicule();
            Console.Clear();
            break;
        case "6":
            AfficherHeureEleveMoni();
            Console.Clear();
            break;
        case "7":
            AfficherCAmensuel();
            Console.Clear();
            break;
        case "8":
            AfficherAjoutSuppEleve();
            Console.Clear();
            break;
        case "9":
            AfficherAjoutSuppVehicule();
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

void AfficherVoirMontant()
{
    EleveService eleve = new EleveService(port, pwd);
    Eleve e = new Eleve();
    EleveDAO dao = new EleveDAO(port, pwd);

    eleve.AfficherAllEleve();
    List<Eleve> liste = dao.GetAll();
    Console.WriteLine("Choisissez le numéro de l'élève dont vous souhaitez connaitre le montant qu'il doit régler");
    int id;
    do
    {
        if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.Write("Veuillez entrer un numéro valide : ");
        }
    } while (id < 0 && id > liste.Count);
    e = liste[id - 1];
    double montant = dao.MontantTotalEleve(id);
    Console.WriteLine("Le montant à régler pour l'élève " + e.nomEleve + " est de : " + montant + "EUR");
    Thread.Sleep(2500);
}

void AfficherKiloVehicule()
{
    Vehicule vehicule = new Vehicule();
    VehiculeDAO vehiculeDAO = new VehiculeDAO(port, pwd);

    vehicule.afficherallvehicule(port, pwd);
    List<Vehicule> listeVehicules = vehiculeDAO.GetAll();
    Console.WriteLine("Chosisissez le numéro du véhicule que vous souhaitez connaitre le kilométrage");
    int idv;
    do
    {
        if (!int.TryParse(Console.ReadLine(), out idv) || idv < 0)
        {
            Console.WriteLine("Veuillez entrer un numéro valide :");
        }
    } while (idv < 0 && idv > listeVehicules.Count - 1);
    Console.WriteLine("Donnez le mois que vous souhaitez regarder le kilométrage :");
    int Moiskilo;
    do
    {
        if (!int.TryParse(Console.ReadLine(), out Moiskilo) || Moiskilo < 1 || Moiskilo > 12)
        {
            Console.Write("Veuillez entrer un nombre valide : ");
        }
    } while (Moiskilo < 1 || Moiskilo > 12);
    Console.WriteLine("Donnez l'année que vous souhaitez regarder le kilométrage :");
    int annekilo;
    do
    {
        if (!int.TryParse(Console.ReadLine(), out annekilo) || annekilo < 0)
        {
            Console.Write("Veuillez entrer un nombre valide : ");
        }
    } while (annekilo < 0);
    vehicule = listeVehicules[idv - 1];
    double nbrkilometre = vehiculeDAO.Nbrkilometre(idv, annekilo, Moiskilo);
    Console.WriteLine("Le kilométrage du véhicule " + vehicule.marque + " " + vehicule.modele + " est de : " + nbrkilometre + "km");
    Thread.Sleep(2500);
}

void AfficherHeureEleveMoni()
{
    string choix;
    int id;
    EleveService eleve = new EleveService(port, pwd);
    Eleve e = new Eleve();
    EleveDAO dao = new EleveDAO(port, pwd);

    do
    {
        Console.WriteLine("Voulez-vous connaitre le nombre d'heures d'un moniteur ou d'un élève ?");
        Console.WriteLine("1 - Moniteur");
        Console.WriteLine("2 - Eleve");
        choix = Console.ReadLine();
        if (choix != "1" && choix != "2")
        {
            Console.Write("Veuillez entrer un choix valide : ");
        }
    } while (choix != "1" && choix != "2");
    if (choix == "1")
    {
        Moniteur moniteur = new Moniteur();
        MoniteurDAO moniteurDAO = new MoniteurDAO(port, pwd);
        MoniteurService moniteurService = new MoniteurService(port, pwd);
        moniteurService.AfficherAllMoniteur();
        List<Moniteur> listmon = moniteurDAO.GetAll();
        Console.WriteLine("Chosisissez le numéro du moniteur que vous souhaitez connaitre le nombre d'heures");

        Moniteur m = new Moniteur();
        int idmon=0;
        do
        {
            if (!int.TryParse(Console.ReadLine(), out idmon) || idmon < 0)
            {
                Console.Write("Veuillez entrer un numéro valide : ");
            }
        } while (idmon < 0 && idmon > listmon.Count);
        for(int i = 0; i < listmon.Count(); i++)
        {
            if(listmon[i].id_moniteur == idmon) m=listmon[i];
        }
        int nbrheuremoniteur = moniteurDAO.NbrheureMoniteur(idmon, port, pwd);
        Console.WriteLine("Le nombre d'heures du moniteur " + moniteur.nom + " " + moniteur.prenom + " est de : " + nbrheuremoniteur + "h");
        Thread.Sleep(2500);
    }
    else if (choix == "2")
    {
        eleve.AfficherAllEleve();
        List<Eleve> listeeleve = dao.GetAll();
        Console.Write("Choisissez le numéro de l'élève que vous souhaitez connaitre le nombre d'heures : ");
        do
        {
            if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.Write("Veuillez entrer un numéro valide : ");
            }
        } while (id < 0 && id > listeeleve.Count);
        for(int i = 0; i < listeeleve.Count(); i++)
        {
            if(listeeleve[i].id_eleve == id) e=listeeleve[i];
        }
        int nbrheureeleve = dao.NbrheureEleve(id);
        Console.WriteLine("Le nombre d'heures de l'élève " + e.nomEleve + " " + e.prenomEleve + " est de : " + nbrheureeleve + "h");
    }
    Thread.Sleep(2500);
}

void AfficherCAmensuel()
{     
    LeconDAO leconDAO = new LeconDAO(port, pwd);
    Console.WriteLine("Donnez le mois que vous souhaitez regarder le chiffre d'affaire");
    int Mois;
    do
    {
        if (!int.TryParse(Console.ReadLine(), out Mois) || Mois < 1 || Mois > 12)
        {
            Console.Write("Veuillez entrer un nombre valide : ");
        }
    } while (Mois < 1 || Mois > 12);
    Console.WriteLine("Donnez l'année que vous souhaitez regarder le chiffre d'affaire :");
    int anne;
    do
    {
        if (!int.TryParse(Console.ReadLine(), out anne) || anne < 0)
        {
            Console.Write("Veuillez entrer un nombre valide : ");
        }
    } while (anne < 0);
    double chiffremensuel = leconDAO.Chiffremensuel(anne, Mois);
    Console.WriteLine("\nLe chiffre d'affaire du mois " + Mois + " de l'année " + anne + " est de : " + chiffremensuel + "EUR");
    Thread.Sleep(2500);
}

void AfficherAjoutSuppEleve()
{
    EleveDAO dao = new EleveDAO(port, pwd);
    Eleve e = new Eleve();
    EleveService eleveService = new EleveService(port, pwd);

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
        Console.WriteLine("Ajout d'un élève ...");
        e = eleveService.CreerEleve();
        eleveService.AjouterEleve(e);
    }
    else if (choix == "2")
    {
        Console.WriteLine("Suppression d'un élève ...");
        eleveService.AfficherAllEleve();
        List<Eleve> listeeleve = dao.GetAll();
        Console.Write("Chosisissez le numéro de l'élève que vous souhaitez supprimer");
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

void AfficherAjoutSuppVehicule()
{
    VehiculeDAO dao = new VehiculeDAO(port, pwd);
    Vehicule v = new Vehicule();
    VehiculeServices vehiculeService = new VehiculeServices(port, pwd);
    Console.WriteLine("Voulez-vous ajouter ou supprimer un véhicule ?");
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
        VehiculeServices ajvehicule = new VehiculeServices(port, pwd);
        ajvehicule.AjouterVehicule(new Vehicule());
    }
    else if (choix == "2")
    {
        Console.WriteLine("Suppression d'un véhicule ...");
        vehiculeService.SupprimerVehicule();
    }
    Thread.Sleep(5000);
}