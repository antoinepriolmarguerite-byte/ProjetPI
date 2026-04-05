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

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : Eleve e
        // TRAITEMENT : 
        //   - Ajoute l'objet élève à la liste locale synchronisée
        //   - Appelle le DAO pour l'insertion persistante en base de données
        // SORTIE     : aucune
        // ==========================================
        public void AjouterEleve(Eleve e)
        {
            list_eleve.Add(e);
            bdd_Eleve.Ajouter(e);
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : aucune
        // TRAITEMENT : 
        //   - Instancie l'interface de saisie (IEleveService)
        //   - Initialise un nouvel objet Eleve via son constructeur de saisie
        // SORTIE     : Eleve (l'objet créé)
        // ==========================================
        public Eleve CreerEleve()
        {
            IEleveService view = new IEleveService(port,password);
            Eleve e = new Eleve(view,port,password);
            return e;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : Eleve e
        // TRAITEMENT : Parcourt la liste locale pour vérifier si l'objet existe déjà
        // SORTIE     : bool (true si trouvé)
        // ==========================================
        public bool EleveExiste(Eleve e)
        {
            for(int i = 0; i < list_eleve.Count(); i++)
            {
                if(e==list_eleve[i]) return true;
            }
            return false;
        }

        // ==========================================
        // TYPE       : Méthode d'INSTANCE
        // ENTRÉE     : int id
        // TRAITEMENT : 
        //   - Supprime l'élève de la liste de cache locale par son ID
        //   - Appelle la procédure de suppression SQL (EleveDAO)
        // SORTIE     : aucune
        // ==========================================
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
            foreach (Eleve e in list_eleve)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

