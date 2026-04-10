using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAutoEcoleS4.Services
{
    internal class IFacturationService
    {
        string port;
        string password;
        FactureDAO bdd_elevefact;
        List<Facture> list_facture;

        public IFacturationService(string port, string password)
        {
            this.bdd_elevefact = new FactureDAO(port, password);
            this.list_facture = bdd_elevefact.GetAll();
            this.port = port;
            this.password = password;
        }

// ==========================================
// TYPE       : Méthode d'INSTANCE
// ENTRÉE     : aucune
// TRAITEMENT : 
//   - Affiche la liste des élèves pour sélection
//   - Récupère toutes les factures liées à l'élève choisi
//   - Calcule la somme cumulée des montants de ces factures
// SORTIE     : aucune (affiche le montant total dû en euros)
// ==========================================
        public void AfficherVoirMontant()
        {
            EleveService eleve = new EleveService(port, password);
            Eleve e = new Eleve();
            EleveDAO dao = new EleveDAO(port, password);
            FactureDAO fdao = new FactureDAO(port,password);

            List<Facture> listef = fdao.GetAll();

            eleve.AfficherAllEleve();
            List<Eleve> liste = dao.GetAll();
            Console.Write("Choisissez le numéro de l'élève dont vous souhaitez connaitre le montant qu'il doit régler : ");
            int id;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                {
                    Console.Write("Veuillez entrer un numéro valide : ");
                }
            } while (id < 0 && id > liste.Count);
            for (int i = 0; i < liste.Count(); i++)
            {
                if (liste[i].id_eleve == id) e = liste[i];
            }
            double montant = 0;
            for(int i = 0; i < listef.Count(); i++)
            {
                if(listef[i].id_eleve==e.id_eleve) montant += listef[i].montant;
            }
            //double montant = dao.MontantTotalEleve(id);
            Console.WriteLine("Le montant à régler pour l'élève " + e.nomEleve + " est de : " + montant + "EUR");
            Thread.Sleep(2500);
        }

// ==========================================
// TYPE       : Méthode d'INSTANCE
// ENTRÉE     : aucune
// TRAITEMENT : 
//   - Nettoie la console
//   - Parcourt la liste des factures chargées et les affiche ligne par ligne
// SORTIE     : aucune
// ==========================================
        public void AfficherAllFacture()
        {
            Console.Clear();
            Console.WriteLine("Voici la liste de toutes les factures : ");
            foreach (Facture f in list_facture)
            {
                Console.WriteLine("ID : " + f.id_facture + " | Nom de l'élève : " + f.nomEleve + " | Montant : " + f.montant + " | Date de la séance : " + f.dateSeance);
            }
        }

    }
}
