using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using ProjetAutoEcoleS4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Services
{
    internal class EleveService
    {
        private List<Eleve> list_eleve;
        private IEleveService view;
        private EleveDAO bdd_Eleve;

        public EleveService()
        {
            list_eleve = new List<Eleve>();
            this.view = new IEleveService();
            this.bdd_Eleve = new EleveDAO();
        }

        public void AjouterEleve(Eleve e, string port, string password)
        {
            list_eleve.Add(e);
            bdd_Eleve.Ajouter(e, port, password);
        }

        public Eleve CreerEleve(string port, string password)
        {
            Console.WriteLine("Veuillez entrer le code NEPH de l'élève : ");
            string codeneph = Console.ReadLine();
            Eleve e = new Eleve(codeneph);
            return e;
        }
    }
}

