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
   Permis VARCHAR(50) NOT NULL,
   Boite VARCHAR(50) NOT NULL,
   MoniteurTitre VARCHAR(50),
   NbHeuresAPayer INT,
   MontantReglementRestant INT NOT NULL
   -- PRIMARY KEY(id_eleve)
);

CREATE TABLE Moniteur(
   ID_Moniteur VARCHAR(50),
   Nom VARCHAR(50) NOT NULL,
   Prenom VARCHAR(50) NOT NULL,
   Permis_Moniteur VARCHAR(50),
   Salaire_Moniteur INT,
   PRIMARY KEY(ID_Moniteur)
);

CREATE TABLE Vehicule(
   Immatriculation VARCHAR(50),
   TypeVehicule VARCHAR(50) NOT NULL,
   Boite BOOL NOT NULL,
   Historique TEXT,
   Marque VARCHAR(50),
   Modele VARCHAR(50),
   PRIMARY KEY(Immatriculation)
);

CREATE TABLE Lecon(
   ID_Lecon VARCHAR(50),
   Date_Lecon DATETIME NOT NULL,
   id_eleve int, -- BORDEL
   Moniteur VARCHAR(50) NOT NULL,
   Immatriculation VARCHAR(50) NOT NULL,
   MontantFacture INT NOT NULL,
   PRIMARY KEY(ID_Lecon),
   FOREIGN KEY(Immatriculation) REFERENCES Vehicule(Immatriculation),
   FOREIGN KEY(Moniteur) REFERENCES Moniteur(ID_Moniteur),
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
