using ProjetAutoEcoleS4.Data;
using ProjetAutoEcoleS4.Interfaces;
using ProjetAutoEcoleS4.Models;
//using System;
using System.Collections.Generic;

namespace ProjetAutoEcoleS4.Services
{
    internal class ClientService
    {
        //Allez hop les interfaces, ça dégage !
        private List<Eleve> list_eleve;
        private IClientService view;
        private EleveDAO bdd_Eleve;

        public ClientService() //On devrait le nommer Eleve service mais flemme <3
        {
            list_eleve = new List<Eleve>();
            IClientService view = new IClientService();
            EleveDAO bdd_Eleve = new EleveDAO();


        }
        public void AjouterEleve()
        {
            Console.WriteLine("Veuillez entrer le code NEPH de l'élève : ");
            string codeneph = Console.ReadLine();
            Eleve e = new Eleve(codeneph);

            list_eleve.Add(e);
            bdd_Eleve.Ajouter(e);
        }
        
       
    }
}