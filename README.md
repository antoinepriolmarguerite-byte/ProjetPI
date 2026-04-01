# Mini-Projet Auto-École 🚗
Ce mini-projet a été développé par `Ronan`, `Bastien`, `Antoine` en 2026. Il a été implementé dans le cadre du cours de *Problèmes informatiques* à l'École supérieure d'ingénieurs Léonard-de-Vinci (ESILV
<img src="https://www.itii-pdl.com/wp-content/uploads/2024/11/logo_esilv_couleur.jpg" width="15px"></img>)

## Langages 🌍
Le projet est principalement implémenté en **C#**. La partie base de données est quand-à elle implementée en **MySQL**.

<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Python-logo-notext.svg/250px-Python-logo-notext.svg.png" width="100px" style="padding: 5px 10px"></img>
<img src="https://pngimg.com/uploads/mysql/mysql_PNG23.png" width="100px" style="padding: 5px 10px"></img>


## Architecture ⚙️
```
└──ProjetAutoEcole
├── ProjetAutoEcoleS4
│   ├── ProjetAutoEcoleS4
│   │   ├── Data
│   │   │   ├── Database.cs
│   │   │   ├── EleveDAO.cs
│   │   │   └── LeconDAO.cs
│   │   ├── Interfaces
│   │   │   ├── IClientService.cs
│   │   │   └── ILeconService.cs
│   │   ├── Models
│   │   │   ├── Eleve.cs
│   │   │   ├── Facture.cs
│   │   │   ├── KilometrageMensuel.cs
│   │   │   ├── Lecon.cs
│   │   │   ├── Mois.cs
│   │   │   ├── Moniteur.cs
│   │   │   ├── Planning.cs
│   │   │   ├── Vehicule.cs
│   │   ├── Program.cs
│   │   ├── ProjetAutoEcoleS4.csproj
│   │   └── Services
│   │       └── ClientServices.cs
│   └── ProjetAutoEcoleS4.sln
├── README.md
├── SujetProjet.pdf
└── bdd.sql
```
Le projet est divisé en quatre dossiers principaux : **Data** pour les codes traitant du SQL, **Interfaces** pour les codes traitant des affichages entrée/sortie, **Models** pour les implémentations classes et **Services** pour les actions.

## Clone this Repository
##### HTTPS
```bash
git clone https://github.com/RonanLEROUZIC/ProjetAutoEcole.git
```
##### SSH
```bash
git clone git@github.com:RonanLEROUZIC/ProjetAutoEcole.git
```
