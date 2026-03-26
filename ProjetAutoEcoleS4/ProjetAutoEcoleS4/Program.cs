// See https://aka.ms/new-console-template for more information

//using MySqlX.XDevAPI;
using MySql.Data.MySqlClient;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
using ProjetAutoEcoleS4.Services;

//==========UTILITÉ FONCTIONNEMENT===========
Console.WriteLine("==========UTILITÉ FONCTIONNEMENT===========");
Console.Write("Veuillez entrer votre port (par defaut, 3306) : ");
string port = Console.ReadLine();
Console.Write("\nVeuillez entrer votre mot de passe (SQL hein) : ");
string sql = Console.ReadLine();
Database db = new Database(port,sql);
Console.WriteLine(db.TestConnection());
Console.WriteLine("\n==========UTILITÉ FONCTIONNEMENT===========");
//==========UTILITÉ FONCTIONNEMENT===========

Eleve bonjour = new Eleve("Salut");
Console.WriteLine("================ Projet 2026 AutoEcole ================");
string a;
Console.WriteLine("\n\n");
Console.WriteLine("1 - Ajouter une leçon");
Console.WriteLine("1 - Suprimer une leçon");
Console.WriteLine("2 - Voir le planing");
Console.WriteLine("3 - Voir le montant à régler");
Console.WriteLine("4 - Voir le kilométrage d'un véhicule");
Console.WriteLine("5 - Voir le nombre d'heure d'un élève ou d'un moniteur");
Console.WriteLine("6 - Chiffre mensuel de l'auto-école");
do
{
    a = Console.ReadLine();
    if (a != "1" && a != "2" && a != "3" && a != "4" && a != "5" && a != "6" && a != "7" && a != "8" && a != "9") { Console.WriteLine("Veuillez entrer un chiffre entre 1 et 9"); }
} while(a!="1" && a !="2" && a !="3" && a !="4" && a !="5" && a !="6" && a !="7" && a !="8" && a !="9");
switch (a)
{
    case "1":
        Lecon lecon = new Lecon();
        lecon.Ajouterleçon(lecon);
        break;
    case "2":

        break;
    case "3":
        Console.WriteLine("Vous avez choisi l'option 3");
        break;
    case "4":
        Console.WriteLine("Vous avez choisi l'option 4");
        break;
}
//Database conn = new Database("3312","");//D'abord le port puis le mdp
//Console.WriteLine(conn.TestConnection());
Console.ReadKey();
