DROP DATABASE IF EXISTS AutoEcole;
CREATE DATABASE AutoEcole;
USE AutoEcole;

CREATE TABLE Eleve( -- Client
	id_eleve INT AUTO_INCREMENT PRIMARY KEY,
   CodeNEPH VARCHAR(50),
   Nom VARCHAR(50) NOT NULL,
   Prenom VARCHAR(50) NOT NULL,
   Tel VARCHAR(50),
   Mail VARCHAR(50),
   Type_eleve VARCHAR(50), 
   Adresse TEXT,
   RIB VARCHAR(50),
   DateNaissance DATE NOT NULL,
   Permis VARCHAR(50) DEFAULT NULL,
   Boite VARCHAR(50) DEFAULT NULL,
   MoniteurTitre VARCHAR(50) DEFAULT NULL,
   NbHeuresAPayer INT DEFAULT NULL,
   MontantReglementRestant INT DEFAULT NULL
);

CREATE TABLE Moniteur(
   ID_Moniteur int AUTO_INCREMENT,
   Nom VARCHAR(50),
   Prenom VARCHAR(50) NOT NULL,
   Permis_Moniteur VARCHAR(50),
   Salaire_Moniteur INT,
   PRIMARY KEY(ID_Moniteur)
);

CREATE TABLE Vehicule(
	id_vehicule INT AUTO_INCREMENT,
   Immatriculation VARCHAR(50),
   TypeVehicule VARCHAR(50) NOT NULL,
   Boite BOOLEAN NOT NULL,
   Historique TEXT,
   CoutAssurance INT,
   Marque VARCHAR(50),
   Modele VARCHAR(50),
   PRIMARY KEY(id_vehicule)
);

CREATE TABLE Lecon(
   ID_Lecon int,
   Date_ DATETIME NOT NULL,
   id_eleve int NOT NULL,
   id_moniteur int NOT NULL,
   Immatriculation VARCHAR(50) NOT NULL,
   MontantFacture INT NOT NULL,
   id_vehicule int not null,
   PRIMARY KEY(ID_Lecon),
   FOREIGN KEY(id_vehicule) REFERENCES Vehicule(id_vehicule),
   FOREIGN KEY(ID_Moniteur) REFERENCES Moniteur(ID_Moniteur),
   FOREIGN KEY(id_eleve) REFERENCES Eleve(id_eleve)
);

CREATE TABLE Planning(
   ID_Planning VARCHAR(50),
   DateHeureDebut DATETIME NOT NULL,
   DateHeureFin DATETIME NOT NULL,
   Formule VARCHAR(50),
   Immatriculation VARCHAR(50) NOT NULL,
   ID_Lecon VARCHAR(50) NOT NULL,
   ID_Moniteur VARCHAR(50) NOT NULL,
   id_eleve INT, 
   PRIMARY KEY(ID_Planning),
   FOREIGN KEY(Immatriculation) REFERENCES Vehicule(Immatriculation),
   FOREIGN KEY(ID_Lecon) REFERENCES Lecon(ID_Lecon),
   FOREIGN KEY(ID_Moniteur) REFERENCES Moniteur(ID_Moniteur),
   FOREIGN KEY(id_eleve) REFERENCES Eleve(id_eleve)
);

CREATE TABLE Facture(
   ID_Facture VARCHAR(50),
   Destinataire VARCHAR(50) NOT NULL,
   Eleve VARCHAR(50) NOT NULL,
   Montant INT NOT NULL,
   DeadlineReglement DATE NOT NULL,
   DateSéance DATE NOT NULL,
   TypeReglement VARCHAR(50),
   id_eleve INT, 
   PRIMARY KEY(ID_Facture),
   FOREIGN KEY(id_eleve) REFERENCES Eleve(id_eleve)
);

CREATE TABLE Mois(
   Annee_mois INT,
   PRIMARY KEY(Annee_mois)
);

CREATE TABLE KilmometrageMois(
   Immatriculation VARCHAR(50),
   Annee_mois INT,
   Nbkilometre DOUBLE,
   PRIMARY KEY(Immatriculation, Annee_mois),
   FOREIGN KEY(Immatriculation) REFERENCES Vehicule(Immatriculation),
   FOREIGN KEY(Annee_mois) REFERENCES Mois(Annee_mois)
);

-- ELEVE
INSERT INTO Eleve (CodeNEPH, Nom, Prenom, Tel, Mail, Type_eleve, Adresse, RIB, DateNaissance, Permis, Boite, MoniteurTitre, NbHeuresAPayer, MontantReglementRestant)
VALUES
('NEPH001', 'Dupont', 'Lucas', '0612345678', 'lucas.dupont@mail.com', 'Classique', 'Paris', 'FR761234', '2000-05-12', 'B', 'Manuelle', 'Débutant', 20, 800),
('NEPH002', 'Martin', 'Emma', '0623456789', 'emma.martin@mail.com', 'Accéléré', 'Lyon', 'FR761235', '1999-08-22', 'B', 'Automatique', 'Intermédiaire', 15, 600),
('NEPH003', 'Durand', 'Noah', '0634567890', 'noah.durand@mail.com', 'Classique', 'Marseille', 'FR761236', '2001-01-10', 'B', 'Manuelle', 'Débutant', 25, 1000);

-- MONITEUR
INSERT INTO Moniteur (Nom, Prenom, Permis_Moniteur, Salaire_Moniteur)
VALUES
('Bernard', 'Paul', 'B', 2200),
('Petit', 'Sophie', 'B', 2300),
('Robert', 'Marc', 'B', 2100);

-- VEHICULE
INSERT INTO Vehicule (Immatriculation, TypeVehicule, Boite, Historique, CoutAssurance, Marque, Modele)
VALUES
('AB-123-CD', 'Voiture', TRUE, 'RAS', 1200, 'Peugeot', '208'),
('EF-456-GH', 'Voiture', FALSE, 'Révision OK', 1300, 'Renault', 'Clio'),
('IJ-789-KL', 'Voiture', TRUE, 'Changement pneus', 1100, 'Citroen', 'C3');

-- PLANNING
INSERT INTO Planning (ID_Planning, DateHeureDebut, DateHeureFin, Formule, Immatriculation, ID_Lecon, ID_Moniteur, CodeNEPH, id_eleve)
VALUES
(1, '2025-03-01 10:00:00', '2025-03-01 11:00:00', '1h', 'AB-123-CD', 1, 1, 'NEPH001', 1),
(2, '2025-03-02 14:00:00', '2025-03-02 15:00:00', '1h', 'EF-456-GH', 2, 2, 'NEPH002', 2),
(3, '2025-03-03 09:00:00', '2025-03-03 10:00:00', '1h', 'IJ-789-KL', 3, 3, 'NEPH003', 3);

-- LECON
INSERT INTO Lecon (ID_Lecon, Date_, id_eleve, id_moniteur, Immatriculation, MontantFacture, id_vehicule)
VALUES
(1, '2025-03-01 10:00:00', 1, 1, 'AB-123-CD', 50, 1),
(2, '2025-03-02 14:00:00', 2, 2, 'EF-456-GH', 55, 2),
(3, '2025-03-03 09:00:00', 3, 3, 'IJ-789-KL', 50, 3);

-- FACTURATION
INSERT INTO Facture (ID_Facture, Destinataire, Eleve, Montant, DeadlineReglement, DateSeance, TypeReglement, CodeNEPH, id_eleve)
VALUES
('F001', 'Dupont', 'Lucas', 50, '2025-03-10', '2025-03-01', 'CB', 'NEPH001', 1),
('F002', 'Martin', 'Emma', 55, '2025-03-11', '2025-03-02', 'Espèces', 'NEPH002', 2),
('F003', 'Durand', 'Noah', 50, '2025-03-12', '2025-03-03', 'Virement', 'NEPH003', 3);

INSERT INTO Mois (Annee_mois)
VALUES
(202503),
(202504),
(202505);

INSERT INTO KilmometrageMois (Immatriculation, Annee_mois, Nbkilometre)
VALUES
('AB-123-CD', 202503, 1200.5),
('EF-456-GH', 202503, 980.3),
('IJ-789-KL', 202503, 1500.0);


