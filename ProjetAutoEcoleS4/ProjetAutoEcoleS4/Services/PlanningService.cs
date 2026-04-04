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
        public void AfficherPlanning()
        {
            Console.Clear();
            Console.WriteLine("\n--- PLANNING DE L'AUTO-ÉCOLE ---");
            PlanningDAO planningDAO = new PlanningDAO(port, password);
            List<string> lecons = planningDAO.RecupererPlanningDAO();

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
            Thread.Sleep(10000);
        }
    }
}
