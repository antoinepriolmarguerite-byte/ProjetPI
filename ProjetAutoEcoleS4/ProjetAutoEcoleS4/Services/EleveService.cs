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
        internal List<Eleve> list_eleve {set; get;}
        private EleveDAO bdd_Eleve;
        string port;
        string password;

        public EleveService(string port, string password)
        {
            this.bdd_Eleve = new EleveDAO(port, password);
            list_eleve = bdd_Eleve.GetAll();
            this.port = port;
            this.password = password;
        }

        public void AjouterEleve(Eleve e)
        {
            list_eleve.Add(e);
            bdd_Eleve.Ajouter(e);
        }

        public Eleve CreerEleve()
        {
            IEleveService view = new IEleveService(port,password);
            Eleve e = new Eleve(view,port,password);
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

        public void SupprimerEleve(int id)
        {
            for(int i = 0; i < list_eleve.Count(); i++)
            {
                if(id == list_eleve[i].id_eleve) {list_eleve.RemoveAt(i);break;}
            }
            bdd_Eleve.Supprimer(id);
            Console.WriteLine("Eleve supprimé avec succès ! ");
        }

        public void AfficherAllEleve()
        {
            EleveDAO elevedao = new EleveDAO(port, password);
            List<Eleve> liste = elevedao.GetAll();
            int i=1;
            foreach (Eleve e in liste)
            {
                Console.WriteLine(i+e.ToString());
                i++;
            }
        }
    }
}

