USE AutoEcole;

-- 1. Insertion des Moniteurs
INSERT INTO Moniteur (ID_Moniteur, Nom, Prenom, Permis_Moniteur, Salaire_Moniteur) VALUES
('MONIT01', 'Rossi', 'Marco', 'A, B, BE', 2200),
('MONIT02', 'Lemoine', 'Catherine', 'B', 2100),
('MONIT03', 'Dufour', 'Alain', 'B, C', 2300);

-- 2. Insertion des Véhicules (Boite : 0 = Manuelle, 1 = Auto)
INSERT INTO Vehicule (Immatriculation, TypeVehicule, Boite, Historique, Marque, Modele) VALUES
('AA-123-BB', 'Voiture', 0, 'Révision OK - Janvier 2026', 'Peugeot', '208'),
('CC-456-DD', 'Voiture', 1, 'Neuve', 'Renault', 'Clio V'),
('EE-789-FF', 'Moto', 0, 'Entretien chaîne fait', 'Yamaha', 'MT-07');

-- 3. Insertion des Élèves 
-- Note : id_eleve est AUTO_INCREMENT, donc on ne le précise pas.
INSERT INTO Eleve (CodeNEPH, Nom, Prenom, Tel, Mail, Type_eleve, Adresse, RIB, DateNaissance, Permis, Boite, MoniteurTitre, NbHeuresAPayer, MontantReglementRestant) VALUES
('190175100432', 'Dupont', 'Jean', '0601020304', 'jean.dupont@email.com', 'Initial', '12 rue des Fleurs, Paris', 'FR7612345', '2005-05-15', 'B', 'Manuelle', 'Rossi', 5, 250),
('200299200555', 'Martin', 'Sophie', '0611223344', 's.martin@email.com', 'AAC', '45 avenue de Lyon, Lyon', 'FR7698765', '2008-10-20', 'B', 'Automatique', 'Lemoine', 2, 100),
('180344300666', 'Lefebvre', 'Thomas', '0788990011', 't.lefebvre@email.com', 'Initial', '8 impasse du Stade, Lille', 'FR7644556', '2004-02-02', 'A2', 'Manuelle', 'Rossi', 10, 500);

-- 4. Insertion des Leçons
-- On suppose que Jean est ID 1, Sophie ID 2, Thomas ID 3
INSERT INTO Lecon (ID_Lecon, Date_Lecon, id_eleve, Moniteur, Immatriculation, MontantFacture) VALUES
('LEC001', '2026-04-10 14:00:00', 1, 'MONIT01', 'AA-123-BB', 50),
('LEC002', '2026-04-10 16:00:00', 2, 'MONIT02', 'CC-456-DD', 55),
('LEC003', '2026-04-11 09:00:00', 3, 'MONIT01', 'EE-789-FF', 60);

-- 5. Insertion du Planning
INSERT INTO Planning (ID_Planning, DateHeureDebut, DateHeureFin, Formule, Immatriculation, ID_Lecon, ID_Moniteur, id_eleve) VALUES
('PLAN001', '2026-04-10 14:00:00', '2026-04-10 15:00:00', 'Heure Solo', 'AA-123-BB', 'LEC001', 'MONIT01', 1),
('PLAN002', '2026-04-10 16:00:00', '2026-04-10 17:00:00', 'Conduite Accompagnée', 'CC-456-DD', 'LEC002', 'MONIT02', 2);

-- 6. Insertion des Factures
INSERT INTO Facture (ID_Facture, Destinataire, Eleve, Montant, DeadlineReglement, DateSeance, TypeReglement, id_eleve) VALUES
('FACT001', 'Jean Dupont', 'Jean Dupont', 50, '2026-05-10', '2026-04-10', 'Carte Bancaire', 1),
('FACT002', 'Sophie Martin', 'Sophie Martin', 55, '2026-05-10', '2026-04-10', 'Virement', 2);

-- 7. Données Kilométrage
INSERT INTO Mois (Annee_mois) VALUES (202604);
INSERT INTO KilmometrageMois (Immatriculation, Annee_mois, Nbkilometre) VALUES
('AA-123-BB', 202604, 1250.5),
('CC-456-DD', 202604, 800.0);

SELECT * FROM lecon;
SELECT * FROM moniteur;