# 🚗 Mini-Projet Auto-École - ESILV 2026

<p align="center">
  <img src="https://www.itii-pdl.com/wp-content/uploads/2024/11/logo_esilv_couleur.jpg" width="200" alt="Logo ESILV">
</p>

Ce projet a été développé par **Ronan**, **Bastien** et **Antoine** dans le cadre du cours de *Problèmes Informatiques* à l'**ESILV** (École Supérieure d'Ingénieurs Léonard-de-Vinci).

---

## 🛠️ Technologies & Langages

![C#](https://img.shields.io/badge/language-C%23-blue.svg)
![MySQL](https://img.shields.io/badge/database-MySQL-orange.svg)
![.NET](https://img.shields.io/badge/framework-.NET%208.0-purple.svg)

Le projet repose sur une application console robuste communiquant avec une base de données relationnelle.

<p align="">
  <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Logo_C_sharp.svg/1200px-Logo_C_sharp.svg.png" width="50" alt="C# Logo" style="margin-right: 20px;">
  <img src="https://pngimg.com/uploads/mysql/mysql_PNG23.png" width="80" alt="MySQL Logo">
</p>

---

## ⚙️ Configuration & Installation

### 📊 Base de données
Le projet supporte deux environnements principaux :

1.  **MySQL Workbench**
    * **Port :** `3306`
    * **Utilisateur :** `root`
    * **Mot de passe :** Celui défini lors de votre installation.
2.  **XAMPP**
    * **Port :** Vérifiez la colonne *Port(s)* dans le panneau de contrôle XAMPP (souvent `3306`).
    * **Utilisateur :** `root`
    * **Mot de passe :** `""` (vide par défaut).

> [!IMPORTANT]
> ## N'oubliez pas d'exécuter le script `bdd.sql` fourni à la racine du projet pour initialiser les tables et les données de test.

---

## 🏗️ Architecture du Projet

Le projet adopte une architecture **N-Tier** (DAO / Service) pour une séparation nette des responsabilités :

```text
📂 ProjetAutoEcole
├── 📂 Data          # Accès persistant (DAO) - Requêtes SQL pures
├── 📂 Models        # Objets métier (POCO) - Classes et propriétés
├── 📂 Services      # Logique métier - Fait le pont entre Data et UI
├── 📂 Interfaces    # Contrats de services et gestion des entrées/sorties
├── 📄 Program.cs    # Point d'entrée principal (Interface Console)
├── 📄 bdd.sql       # Script de création de la base de données
└── 📄 SujetProjet.pdf
```
### Pour cloner le projet via SSH
`git clone git@github.com:RonanLEROUZIC/ProjetAutoEcole.git`

### Pour cloner le projet via HTTPS
`git clone [https://github.com/RonanLEROUZIC/ProjetAutoEcole.git](https://github.com/RonanLEROUZIC/ProjetAutoEcole.git)`

- note sur l'utilisation de l'ia, nous l'avons utiliser pour dupliquer les commentaires et pour nous aider sur la correction d'erreur
<p align="center">© 2026 - Projet Informatique S4 - ESILV</p>
