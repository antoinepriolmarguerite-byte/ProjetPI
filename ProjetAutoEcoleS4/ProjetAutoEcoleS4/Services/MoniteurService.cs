using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
using ProjetAutoEcoleS4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Services
{
    internal class MoniteurService
    {
        List<Moniteur> list_moniteur;
        private MoniteurDAO bdd_Moniteur;

        public MoniteurService(string port, string password)
        {
            this.bdd_Moniteur = new MoniteurDAO(port,password);
            list_moniteur = bdd_Moniteur.GetAll(port, password);
        }
        public bool MoniteurExiste(int m)
        {
            for(int i = 0; i < list_moniteur.Count(); i++)
            {
                if(m==list_moniteur[i].id_moniteur) return true;
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
