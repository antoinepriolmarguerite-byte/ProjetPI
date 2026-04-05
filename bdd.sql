DROP DATABASE IF EXISTS AutoEcole;
CREATE DATABASE AutoEcole;
USE AutoEcole;


CREATE TABLE Moniteur (
   ID_Moniteur INT AUTO_INCREMENT,
   Nom VARCHAR(50) NOT NULL,
   Prenom VARCHAR(50) NOT NULL,
   Permis_Moniteur VARCHAR(50),
   Salaire_Moniteur DECIMAL(10,2),
   PRIMARY KEY(ID_Moniteur)
);
CREATE TABLE Eleve (
   ID_Eleve INT AUTO_INCREMENT,
   CodeNEPH VARCHAR(50),
   NomEleve VARCHAR(50) NOT NULL,
   PrenomEleve VARCHAR(50) NOT NULL,
   Tel VARCHAR(20),
   Mail VARCHAR(100),
   TypeEleve VARCHAR(50), 
   Adresse TEXT,
   RIB VARCHAR(50),
   DateNaissance DATE NOT NULL,
   Permis VARCHAR(50) DEFAULT NULL,
   Boite VARCHAR(50) DEFAULT NULL,
   idMoniteurReferent INT DEFAULT NULL,
   NbHeuresAPayer INT DEFAULT 0,
   MontantReglementRestant DECIMAL(10,2) DEFAULT 0.00,
   PRIMARY KEY(ID_Eleve),
   FOREIGN KEY(idMoniteurReferent) REFERENCES Moniteur(ID_Moniteur)
);


CREATE TABLE Vehicule (
   ID_Vehicule INT AUTO_INCREMENT,
   Immatriculation VARCHAR(20) UNIQUE,
   TypeVehicule VARCHAR(50) NOT NULL,
   Boite BOOLEAN NOT NULL, -- 0 pour Manuelle, 1 pour Auto par exemple
   Historique TEXT,
   CoutAssurance DECIMAL(10,2),
   Marque VARCHAR(50),
   Modele VARCHAR(50),
   Etat BOOLEAN DEFAULT 1,
   PRIMARY KEY(ID_Vehicule)
);

CREATE TABLE Lecon (
   ID_Lecon INT AUTO_INCREMENT,
   Date_ DATETIME NOT NULL,
   ID_Eleve INT NOT NULL,
   ID_Moniteur INT NOT NULL,
   ID_Vehicule INT NOT NULL,
   MontantFacture DECIMAL(10,2) NOT NULL,
   PRIMARY KEY(ID_Lecon),
   FOREIGN KEY(ID_Vehicule) REFERENCES Vehicule(ID_Vehicule),
   FOREIGN KEY(ID_Moniteur) REFERENCES Moniteur(ID_Moniteur),
   FOREIGN KEY(ID_Eleve) REFERENCES Eleve(ID_Eleve)
);

CREATE TABLE Planning (
   ID_Planning INT AUTO_INCREMENT,
   DateHeureDebut DATETIME NOT NULL,
   DateHeureFin DATETIME NOT NULL,
   Formule VARCHAR(50),
   ID_Vehicule INT NOT NULL,
   ID_Lecon INT, -- Peut être NULL si le créneau n'est pas encore une leçon validée
   ID_Moniteur INT NOT NULL,
   ID_Eleve INT,
   PRIMARY KEY(ID_Planning),
   FOREIGN KEY(ID_Vehicule) REFERENCES Vehicule(ID_Vehicule),
   FOREIGN KEY(ID_Lecon) REFERENCES Lecon(ID_Lecon),
   FOREIGN KEY(ID_Moniteur) REFERENCES Moniteur(ID_Moniteur),
   FOREIGN KEY(ID_Eleve) REFERENCES Eleve(ID_Eleve)
);

CREATE TABLE Facture (
   ID_Facture VARCHAR(50),
   Destinataire VARCHAR(50) NOT NULL,
   NomEleve VARCHAR(50) NOT NULL,
   Montant DECIMAL(10,2),
   DeadlineReglement DATE,
   DateSeance DATE NOT NULL,
   TypeReglement VARCHAR(50),
   ID_Eleve INT, 
   PRIMARY KEY(ID_Facture),
   FOREIGN KEY(ID_Eleve) REFERENCES Eleve(ID_Eleve)
);

CREATE TABLE Mois (
   Annee_mois INT, -- Format : YYYYMM (ex: 202401)
   PRIMARY KEY(Annee_mois)
);

CREATE TABLE KilometrageMois (
   ID_Vehicule INT,
   Annee_mois INT,
   Nbkilometre DOUBLE,
   PRIMARY KEY(ID_Vehicule, Annee_mois),
   FOREIGN KEY(ID_Vehicule) REFERENCES Vehicule(ID_Vehicule),
   FOREIGN KEY(Annee_mois) REFERENCES Mois(Annee_mois)
);

USE AutoEcole;

INSERT INTO Eleve (CodeNEPH, NomEleve, PrenomEleve, Tel, Mail, TypeEleve, Adresse, RIB, DateNaissance, Permis, Boite, idMoniteurReferent, NbHeuresAPayer, MontantReglementRestant) VALUES
('123456789012', 'Dupont', 'Jean', '0601020304', 'jean.dupont@email.com', 'Traditionnel', '12 rue de la Paix, Paris', 'FR763000...', '2005-05-15', 'B', 'Manuelle', 1, 5, 250.00),
('987654321098', 'Martin', 'Sophie', '0708091011', 'sophie.martin@email.com', 'AAC', '5 avenue des Champs, Lyon', 'FR764000...', '2007-11-20', 'B', 'Automatique', 2, 0, 0.00),
('456123789456', 'Lefebvre', 'Thomas', '0611223344', 'thomas.lef@email.com', 'Candidat Libre', '30 rue du Port, Marseille', 'FR765000...', '1998-02-10', 'A2', 'Manuelle', 1, 10, 500.00);

INSERT INTO Moniteur (Nom, Prenom, Permis_Moniteur, Salaire_Moniteur) VALUES
('Martin', 'Lucas', 'B, BE, A', 2200.50),
('Bernard', 'Julie', 'B, BVA', 2100.00),
('Petit', 'Nicolas', 'A, A1, A2', 2300.00);

INSERT INTO Vehicule (Immatriculation, TypeVehicule, Boite, Historique, CoutAssurance, Marque, Modele, Etat) VALUES
('AB-123-CD', 'Voiture', 0, 'Révision faite en Janvier', 450.00, 'Renault', 'Clio 5', 1),
('EF-456-GH', 'Voiture', 1, 'Neuve', 500.00, 'Peugeot', '208', 1),
('IJ-789-KL', 'Moto', 0, 'Changement pneus Mars', 300.00, 'Yamaha', 'MT-07', 1);

INSERT INTO Lecon (Date_, ID_Eleve, ID_Moniteur, ID_Vehicule, MontantFacture) VALUES
('2024-03-20 10:00:00', 1, 1, 1, 50.00),
('2024-03-20 14:00:00', 2, 2, 2, 55.00),
('2024-03-21 09:00:00', 3, 3, 3, 60.00);

INSERT INTO Planning (DateHeureDebut, DateHeureFin, Formule, ID_Vehicule, ID_Lecon, ID_Moniteur, ID_Eleve) VALUES
('2024-03-20 10:00:00', '2024-03-20 11:00:00', 'Heure Conduite', 1, 1, 1, 1),
('2024-03-20 14:00:00', '2024-03-20 15:00:00', 'Heure Conduite BVA', 2, 2, 2, 2),
('2024-03-21 09:00:00', '2024-03-21 11:00:00', 'Plateau Moto', 3, 3, 3, 3),
('2024-03-25 15:00:00', '2024-03-25 16:00:00', 'Evaluation', 1, NULL, 1, 1); -- Créneau prévu mais pas encore facturé (leçon NULL)

INSERT INTO Facture (ID_Facture, Destinataire, NomEleve, Montant, DeadlineReglement, DateSeance, TypeReglement, ID_Eleve) VALUES
('FAC-2024-001', 'Jean Dupont', 'Jean Dupont', 50.00, '2024-04-20', '2024-03-20', 'Carte Bancaire', 1),
('FAC-2024-002', 'Sophie Martin', 'Sophie Martin', 55.00, '2024-04-20', '2024-03-20', 'Virement', 2);

INSERT INTO Mois (Annee_mois) VALUES
(202401),
(202402),
(202403);

INSERT INTO KilometrageMois (ID_Vehicule, Annee_mois, Nbkilometre) VALUES
(1, 202403, 1250.5),
(2, 202403, 890.0),
(3, 202403, 450.2);
SELECT * FROM eleve;
SELECT * FROM lecon;