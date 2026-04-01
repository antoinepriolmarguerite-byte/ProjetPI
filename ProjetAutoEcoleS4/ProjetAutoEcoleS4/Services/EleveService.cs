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

        public Eleve CreerEleve(string codeneph, string port, string password)
        {
            
           
            Eleve e = new Eleve(codeneph);
            return e;
        }

        public bool EleveExiste(Eleve e)
        {
            for(int i = 0; i < list_eleve.Count(); i++)
            {
                if(e==list_eleve[i]) return true;
            }
            return false;
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
        public void AfficherAllEleve(string port, string password)
        {
            EleveDAO elevedao = new EleveDAO();
            List<Eleve> liste = elevedao.GetAll(port, password);
            foreach (Eleve e in liste)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

