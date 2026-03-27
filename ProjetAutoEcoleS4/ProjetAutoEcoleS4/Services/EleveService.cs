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

        public EleveService(string port, string password)
        {
            list_eleve = new List<Eleve>();
            this.view = new IEleveService(port,password);
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

        public void SupprimerEleve(Eleve e,string port, string password)
        {
            for(int i = 0; i < list_eleve.Count(); i++)
            {
                if(e==list_eleve[i]) {list_eleve.RemoveAt(i);break;}
            }
            Console.WriteLine("Eleve supprimé avec succès ! ");
            bdd_Eleve.Supprimer(int.Parse(e.CodeNEPH),port,password);
        }
    }
}

