using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Data;

namespace ProjetAutoEcoleS4.Services
{
    internal class MoniteurService
    {
        List<Moniteur> list_moniteur;
        private MoniteurDAO bdd_Moniteur;


        public bool MoniteurExiste(string m)
        {
            for(int i = 0; i < list_moniteur.Count(); i++)
            {
                if(m==list_moniteur[i].id_Moniteur) return true;
            }
            return false;
        }
        public void AfficherAllMoniteur(string port, string password)
        {
            MoniteurDAO moniteurDAO = new MoniteurDAO(port,password);
            List<Moniteur> liste = moniteurDAO.GetAll(port, password);
            foreach (Moniteur e in liste)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
    }
}
