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

        public void AfficherAllFacture()
        {
            Console.Clear();
            Console.WriteLine("Voici la liste de toutes les factures : ");
            foreach (Facture f in list_facture)
            {
                Console.WriteLine("ID : " + f.id_facture + " | Nom de l'élève : " + f.nomEleve + " | Montant : " + f.montant + " | Date de la séance : " + f.dateSeance);
            }
        }

        public void AfficherVoirMontant()
        {
            EleveService eleve = new EleveService(port, password);
            Eleve e = new Eleve();
            EleveDAO dao = new EleveDAO(port, password);

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
            e = liste[id - 1];
            double montant = dao.MontantTotalEleve(id);
            Console.WriteLine("Le montant à régler pour l'élève " + e.nomEleve + " est de : " + montant + "EUR");
            Thread.Sleep(2500);
        }
    }
}
