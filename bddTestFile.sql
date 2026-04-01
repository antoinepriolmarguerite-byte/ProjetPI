USE AutoEcole;
-- ================================
-- TABLE : Eleve
-- ================================
INSERT INTO Eleve (CodeNEPH, Nom, Prenom, Tel, Mail, Type_eleve, Adresse, RIB, DateNaissance, Permis, Boite, MoniteurTitre, NbHeuresAPayer, MontantReglementRestant)
VALUES
('NEPH001', 'Dupont', 'Jean', '0601020304', 'jean.dupont@email.com', 'Initial', '12 rue des Fleurs, Paris', 'FR7612345', '2005-05-15', 'B', 'Manuelle', 'MONIT01', 5, 250),
('NEPH002', 'Martin', 'Sophie', '0611223344', 's.martin@email.com', 'AAC', '45 avenue de Lyon, Lyon', 'FR7698765', '2008-10-20', 'B', 'Automatique', 'MONIT02', 2, 100),
('NEPH003', 'Lefebvre', 'Thomas', '0788990011', 't.lefebvre@email.com', 'Initial', '8 impasse du Stade, Lille', 'FR7644556', '2004-02-02', 'A2', 'Manuelle', 'MONIT01', 10, 500);

-- ================================
-- TABLE : Moniteur
-- ================================
INSERT INTO Moniteur (ID_Moniteur, Nom, Prenom, Permis_Moniteur, Salaire_Moniteur)
VALUES
('MONIT01', 'Durand', 'Marc', 'A, B, BE', 2200),
('MONIT02', 'Petit', 'Julie', 'B', 2100),
('MONIT03', 'Moreau', 'Alain', 'B, C, D', 2500);

-- ================================
-- TABLE : Vehicule
-- ================================
INSERT INTO Vehicule (Immatriculation, TypeVehicule, Boite, Historique, Marque, Modele)
VALUES
('AA-123-BB', 'Voiture', 0, 'Revision faite en Janvier 2026', 800, 'Peugeot', '208'),
('CC-456-DD', 'Voiture', 1, 'Neuve - Boite Auto', 950, 'Renault', 'Clio 5'),
('EE-789-FF', 'Moto', 0, 'Chaîne graissee', 600, 'Yamaha', 'MT-07');

SELECT * FROM ELEVE;

-- ================================
-- TABLE : Leçon
-- ================================
INSERT INTO Lecon (ID_Lecon, Date_Lecon, Eleve, Moniteur, Vehicule, MontantFacture)
VALUES
('LEC001', '2026-03-10 14:00:00', 'NEPH001', 'MONIT01', 'AA-123-BB', 50),
('LEC002', '2026-03-10 15:00:00', 'NEPH002', 'MONIT02', 'CC-456-DD', 55),
('LEC003', '2026-03-11 10:00:00', 'NEPH001', 'MONIT01', 'AA-123-BB', 50);

-- ================================
-- TABLE : Planning
-- ================================
INSERT INTO Planning (ID_Planning, DateHeureDebut, DateHeureFin, Formule, Immatriculation, ID_Lecon, ID_Moniteur, CodeNEPH)
VALUES
('PLAN001', '2026-03-10 14:00:00', '2026-03-10 15:00:00', 'Heure Solo', 'AA-123-BB', 'LEC001', 'MONIT01', 'NEPH001'),
('PLAN002', '2026-03-10 15:00:00', '2026-03-10 16:00:00', 'Heure Solo', 'CC-456-DD', 'LEC002', 'MONIT02', 'NEPH002');

-- ================================
-- TABLE : Facture
-- ================================
INSERT INTO Facture (ID_Facture, Destinataire, Eleve, Montant, DeadlineReglement, DateSeance, TypeReglement, CodeNEPH)
VALUES
('FAC001', 'Jean Dupont', 'Jean Dupont', 50, '2026-03-20', '2026-03-10', 'CB', 'NEPH001'),
('FAC002', 'Sophie Martin', 'Sophie Martin', 55, '2026-03-25', '2026-03-10', 'Virement', 'NEPH002');

-- ================================
-- TABLE : Mois
-- ================================
INSERT INTO Mois (Annee_mois)
VALUES (202601), (202602), (202603), (202604);

-- ================================
-- TABLE : Kilometrage
-- ================================
INSERT INTO KilmometrageMois (Immatriculation, Annee_mois, Nbkilometre)
VALUES
('AA-123-BB', 202603, 1250.5),
('CC-456-DD', 202603, 890.0);