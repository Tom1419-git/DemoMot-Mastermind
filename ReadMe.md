# 🎯 Jeu du Mastermind 

Le Mastermind est un jeu de réflexion classique développé en C# avec une interface console colorée et interactive.

## 📋 Contenu du jeu
- [À propos](#à-propos)
- [Caractéristiques](#fonctionnalités)
- [Installation](#installation)
- [Utilisation](#utilisation)
- [Modes de jeu](#modes-de-jeu)
- [Règles du jeu](#règles-du-jeu)
- [Statistiques](#statistiques)
- [Configuration nécessaire](#configuration-requise)
- [Auteur](#auteur)

## À propos

Le Mastermind est un jeu de logique où le joueur doit deviner une combinaison secrète de 4 couleurs en 10 essais maximum. Ce projet a été réalisé dans le cadre du module "Démo-Mot" à l'ETML en 2025.

### Couleurs disponibles
- **R** - Rouge 🔴
- **B** - Bleu 🔵  
- **J** - Jaune 🟡
- **O** - Orange 🟠
- **V** - Violet/Magenta 🟣

## Caractéristiques

- **Interface colorée** - Affichage des couleurs avec des fonds colorés pour une expérience visuelle optimale
- **Titre ASCII Art** - Design stylisé pour une immersion totale
- **Historique des essais** - Consultation des essais précédents avec leurs résultats
- **Chronométrage** - Mesure du temps de jeu pour chaque partie
- **Système de statistiques** - Suivi des performances avec sauvegarde automatique
- **Saisie interactive** - Utilisation de Backspace pour corriger, validation avec Entrée
- **Feedback détaillé** - Informations sur les positions des pions dans la combinaison

## Installation

### Prérequis
- .NET Framework ou .NET Core
- Console Windows/Terminal compatible avec les couleurs

### Téléchargement et compilation

1. **Cloner le repository**
   ```bash
   git clone https://github.com/votre-username/mastermind-mayoraz.git
   cd mastermind-mayoraz
   ```

2. **Compiler le projet**
   ```bash
   dotnet build
   ```

3. **Lancer le jeu**
   ```bash
   dotnet run
   ```

   #### Ou sélectionnez simplement le fichier compilé :
   Double-cliquez sur le fichier .exe dans le dossier "Version_Finale"

## Configuration importante

> **⚠️ IMPORTANT** - Pour une expérience de jeu optimale, mettez votre console en plein écran avant de lancer le jeu. Le titre ASCII et l'interface sont conçus pour être affichés sur une console maximisée.

### Sur Windows
- Appuyez sur `F11` ou `Alt + Entrée` pour passer en plein écran
- Ou cliquez sur l'icône de maximisation de la fenêtre de console

## Utilisation

1. Lancer le programme
2. Maximiser la fenêtre de console pour une meilleure visibilité
3. Sélectionner un mode de jeu dans le menu principal
4. Suivre les instructions à l'écran

## Modes de jeu

### 1. Mode Solo
- L'ordinateur génère une combinaison secrète aléatoire
- 10 essais pour deviner la combinaison
- Pas de limite de temps

### 2. Mode Deux Joueurs
- Le **Joueur 1** crée une combinaison secrète (la saisie est masquée avec des étoiles)
- Le **Joueur 2** doit deviner cette combinaison
- Adapté pour jouer avec un ami

### 3. Mode Challenge (Temps limité)
- Comme le mode solo, mais avec une limite de **3 minutes**
- Affichage en temps réel du temps restant
- Expérience plus intense sous pression !

### 4. Règles du jeu
- Explication détaillée des règles et du système de notation

### 5. Statistiques
- Consultation de vos performances personnelles
- Parties jouées, gagnées, perdues
- Taux de victoire et temps moyen

## Règles du jeu

### Objectif
Devinez la combinaison secrète de 4 couleurs en maximum 10 essais.

### Système de notation
Après chaque essai, recevez un feedback :
- **Pions bien placés** - Bonne couleur à la bonne position (affiché en vert)
- **Pions mal placés** - Bonne couleur mais à la mauvaise position (affiché en jaune)

### Points importants
- ⚠️ **Attention** - Une même couleur peut être présente plusieurs fois dans la combinaison
- Les couleurs sont représentées par des lettres : R, B, J, O, V
- Utilisez les touches de couleur pour proposer votre combinaison
- `Backspace` pour corriger, `Entrée` pour valider

### Exemple de feedback
```
 R   J   O   V  => Bien placés : 2  Mal placés : 1
```
Cela indique que 2 couleurs sont bien placées et 1 couleur est présente mais mal placée.

## Statistiques

Vos statistiques sont automatiquement sauvegardées dans le fichier `mastermind_stats.txt` :

- **Parties jouées** - Total de parties
- **Parties gagnées/perdues** - Répartition des résultats
- **Taux de victoire** - Pourcentage de réussite
- **Temps moyen** - Temps moyen pour gagner une partie

Les statistiques sont conservées entre les sessions et se chargent automatiquement au démarrage.

## Configuration requise

- **OS** - Windows (avec un terminal supportant les couleurs)
- **.NET** - Framework .NET ou .NET Core
- **Console** - Terminal compatible avec les couleurs ANSI
- **Résolution** - Console en plein écran recommandée

## Capture d'écran

Le jeu affiche un titre ASCII art impressionnant et utilise des couleurs pour une expérience visuelle immersive :

``` 
╔══════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                          ║
║   ███╗   ███╗ █████╗ ███████╗████████╗███████╗██████╗ ███╗   ███╗██╗███╗   ██╗██████╗    ║
║   ████╗ ████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔══██╗   ║
║   ██╔████╔██║███████║███████╗   ██║   █████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║██║  ██║   ║
║   ██║╚██╔╝██║██╔══██║╚════██║   ██║   ██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██║  ██║   ║
║   ██║ ╚═╝ ██║██║  ██║███████║   ██║   ███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██████╔╝   ║
║   ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═════╝    ║
║                                                                                          ║
║                            CRÉÉ PAR : Thomas MAYORAZ                             ║
║                                PROJET : Démo-Mot - 2025                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════════╝
```

## Auteur

**Thomas MAYORAZ** — Apprenti informaticien 3e année  
Projet réalisé dans le cadre du module "Démo-Mot" à l'ETML en 2025.

---


### Licence

Ce projet a été développé dans un cadre éducatif à l'ETML.

---

**Amusez-vous et que la logique soit avec vous ! 🧠🎯**
---

<div align="center">

[![Profile](https://img.shields.io/badge/👤_Profil_GitHub-Tom1419--git-181717?style=flat-square&logo=github)](https://github.com/Tom1419-git)

</div>
