using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Data
{
    internal class PlanningService
    {
        string port;
        string password;

        public PlanningService(string port, string password)
        {
            this.port = port;
            this.password = password;
        }

// ==========================================
// TYPE       : Méthode d'INSTANCE
// ENTRÉE     : aucune
// TRAITEMENT : 
//   - Efface la console et affiche l'en-tête du planning
//   - Récupère les données formatées (Date | Elève | Moniteur) depuis le PlanningDAO
//   - Gère l'affichage des messages si le planning est vide
//   - Marque une pause de 10 secondes pour la lecture
// SORTIE     : aucune
// ==========================================
        public void AfficherPlanning()
        {
            Console.Write("Donnez la date de la leçon (jj-mm-aaaa) : ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Format invalide. Veuillez entrer une date valide : ");
            }
            Console.Write("Veuillez entrer l'ID du moniteur : ");
            int idMoniteurSaisi;
            while (!int.TryParse(Console.ReadLine(), out idMoniteurSaisi))
            {
                Console.Write("ID inconnu ou invalide. Veuillez choisir un ID présent dans la liste : ");
            }
            Console.Clear();
            Console.WriteLine("\n--- PLANNING DE L'AUTO-ÉCOLE ---");
            PlanningDAO planningDAO = new PlanningDAO(port, password);
            List<string> lecons = planningDAO.RecupererPlanningDAO(date, idMoniteurSaisi);

            if (lecons.Count == 0)
            {
                Console.WriteLine("Aucune leçon de planifiée pour le moment.");
            }
            else
            {
                foreach (string lecon in lecons)
                {
                    Console.WriteLine(lecon);
                }
            }
            Console.WriteLine("--------------------------------\n");
            Thread.Sleep(2500);
        }
    }
}
