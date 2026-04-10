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
        string port;
        string password;

        public MoniteurService(string port, string password)
        {
            this.bdd_Moniteur = new MoniteurDAO(port,password);
            this.port = port;
            this.password = password;
            list_moniteur = bdd_Moniteur.GetAll();
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int m (ID du moniteur)
        // TRAITEMENT : Vérifie dans la liste des moniteurs chargés si l'ID correspond à un moniteur existant
        // SORTIE     : bool (true si le moniteur est valide)
        // ==========================================
        public bool MoniteurExiste(int m)
        {
            for(int i = 0; i < list_moniteur.Count(); i++)
            {
                if(m==list_moniteur[i].id_moniteur) return true;
            }
            return false;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : aucune
        // TRAITEMENT : 
        //   - Récupère la liste actualisée via le MoniteurDAO
        //   - Parcourt et affiche chaque moniteur dans la console (via ToString)
        // SORTIE     : aucune
        // ==========================================
        public void AfficherAllMoniteur()
        {
            MoniteurDAO moniteurDAO = new MoniteurDAO(port,password);
            List<Moniteur> liste = moniteurDAO.GetAll();
            foreach (Moniteur e in liste)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
    }
}
