//tous les codes qui ne servent à rien.
/*
public void Ajouterleçon(Lecon l) //Bon cette méthode marche plus
        {
            Console.Clear();
            EleveService eleveservices = new EleveService(port,password);
            LeconDAO lecondao = new LeconDAO(port, password);

            Console.WriteLine("Donnez la date et l'heure de la leçon : ");
            DateTime date;
            do
            {

                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Veuillez entrer une date valide (jj/mm/aaaa h:m:s) :");
                }
            } while (date == default(DateTime));
            l.dateLecon = date;
            Console.WriteLine("Donnez le code NEPH de l'élève : ");
            string codeNeph;
            do
            {
                codeNeph = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(codeNeph))
                {
                    Console.Write("Le code NEPH de l'élève ne peut pas être vide. Veuillez réessayer : ");
                }
                else if(lecondao.VerifierLeconEleve(codeNeph, date))
                {
                    Console.WriteLine("Il existe déjà une leçon pour cet élève à cette date. Veuillez ressaisir votre leçon : ");
                    Ajouterleçon(l);
                }
            } while (string.IsNullOrWhiteSpace(codeNeph));
            l.eleve = eleveservices.CreerEleve(port, password);
            eleveservices.AjouterEleve(l.eleve, port, password);
            l.eleve = eleve;
            Console.WriteLine("Donnez le nom du moniteur : ");
            string moniteur;
            do
            {
                moniteur = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(moniteur))
                {
                    Console.Write("Le nom du moniteur ne peut pas être vide. Veuillez réessayer : ");
                }
                else if (lecondao.VerifierLeconMoniteur(moniteur, date))
                {
                    Console.WriteLine("Il existe déjà une leçon pour ce moniteur à cette date. Veuillez ressaisir votre leçon : ");
                    Ajouterleçon(l);
                }
            } while (string.IsNullOrWhiteSpace(moniteur) );
            //l.moniteur = moniteur;
            Console.WriteLine("Donnez l'immatricule du véhicule pour la leçon : ");
            int vehicule = 0;
            do
            {
                vehicule = int.Parse(Console.ReadLine());
                if (vehicule<0)
                {
                    Console.WriteLine("L'immatricule du véhicule ne peut pas être vide. Veuillez réessayer : ");
                }
                else if (lecondao.VerifierLeconVehicule(vehicule, date))
                {
                    Console.WriteLine("Il existe déjà une leçon pour ce véhicule à cette date. Veuillez ressaisir votre leçon : ");
                    Ajouterleçon(l);
                }
            } while (vehicule<0);
            l.vehicule = new Vehicule();
            Console.WriteLine("Donnez le montant de la facture pour la leçon : ");
            double montantFacture;
            do
            {
                if (!double.TryParse(Console.ReadLine(), out montantFacture) || montantFacture < 0)
                {
                    Console.Write("Veuillez entrer un montant valide : ");
                }
            } while (montantFacture < 0);
            l.montantFacture = montantFacture;

            lecondao.AjouterLecon_DAO(l);
        }
*/

        /*
        //bool VerifierEligibilite(Client client);
        // Récupérer tous les clients (pour les afficher dans la console)
        List<Eleve> RecupEleve();

        // Récupérer un seul Eleve par son code NEPH
        Eleve RecupEleveParNEPH(string codeNeph); 

        // Ajouter un nouveau Eleve
        bool AjouterEleve(Eleve Eleve);

        // Mettre à jour les infos d'un Eleve
        bool ModifierEleve(Eleve Eleve);

        // Supprimer un Eleve
        bool SupprimerEleve(string codeNeph); */
        /*
        Console.WriteLine("\n== MONITEURS ==");
                MS.AfficherAllMoniteur(port,password);
                for(int i = 0; i < ListeMoniteur.Count(); i++)
                {
                    idMoniteur.Add(ListeMoniteur[i].id_moniteur); 
                }
                Console.Write("Veuillez choisir un moniteur : ");
                int entreeUtilisateur = int.Parse(Console.ReadLine()!); //Jvous laisse faire le tryparse, chepa faire
                while (!idMoniteur.Contains(entreeUtilisateur))
                {
                     Console.Write("Veuillez choisir l'id du moniteur : ");
                     entreeUtilisateur = int.Parse(Console.ReadLine()!); // Les ! permettent de caché les warnings
                }
            retour[11] = entreeUtilisateur+"";
            */


/*LeconDAO lecondao = new LeconDAO(port, password);
            Console.Write("Donnez la date de la leçon : ");
            DateTime date;
            do
            {

                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Veuillez entrer une date valide (jj/mm/aaaa) :");
                }
            } while (date == default(DateTime));

            Console.Write("Donnez le code NEPH de l'élève : ");
            string codeNeph;
            do
            {
                codeNeph = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(codeNeph))
                {
                    Console.Write("Le code NEPH de l'élève ne peut pas être vide. Veuillez réessayer : ");
                }
            } while (string.IsNullOrWhiteSpace(codeNeph));

            lecondao.SupprimerLecon_DAO(codeNeph, date);*/