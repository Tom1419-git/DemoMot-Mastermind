# 🎯 Mastermind Game

Un jeu de réflexion classique développé en C# avec une interface console colorée et interactive.

## 📋 Table des matières
- [À propos](#à-propos)
- [Fonctionnalités](#fonctionnalités)
- [Installation](#installation)
- [Utilisation](#utilisation)
- [Modes de jeu](#modes-de-jeu)
- [Règles du jeu](#règles-du-jeu)
- [Statistiques](#statistiques)
- [Configuration requise](#configuration-requise)
- [Auteur](#auteur)

## À propos

Mastermind est un jeu de logique où l'objectif est de deviner une combinaison secrète de 4 couleurs en 10 essais maximum. Ce projet a été développé dans le cadre du module "Démo-Mot" à l'ETML en 2025.

### Couleurs disponibles
- **R** - Rouge 🔴
- **B** - Bleu 🔵  
- **J** - Jaune 🟡
- **O** - Orange 🟠
- **V** - Violet/Magenta 🟣

## Fonctionnalités

- **Interface colorée** - Affichage des couleurs avec des fonds colorés pour une meilleure expérience visuelle
- **Titre ASCII Art** - Design soigné avec un titre stylisé
- **Historique des essais** - Visualisation de tous les essais précédents avec leurs résultats
- **Chronométrage** - Mesure du temps de jeu pour chaque partie
- **Système de statistiques** - Suivi des performances avec sauvegarde automatique
- **Saisie interactive** - Navigation avec Backspace, validation avec Entrée
- **Feedback détaillé** - Indication des pions bien placés et mal placés

## Installation

### Prérequis
- .NET Framework ou .NET Core
- Console Windows/Terminal avec support des couleurs

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

   #### Ou si vous avez sélectionnez le fichier compilé :
   Double cliquez simplement sur fichier .exe dans le dossier "Version_Finale"
   

## Configuration importante

> **⚠️ IMPORTANT** - Pour une expérience de jeu optimale, **mettez votre console en plein écran** avant de lancer le jeu. Le titre ASCII et l'interface sont conçus pour être affichés sur une console maximisée.

### Sur Windows
- Appuyez sur `F11` ou `Alt + Entrée` pour passer en plein écran
- Ou cliquez sur l'icône de maximisation de la fenêtre de console

## Utilisation

1. Lancez le programme
2. Maximisez la fenêtre de console pour un affichage optimal
3. Choisissez un mode de jeu dans le menu principal
4. Suivez les instructions à l'écran

## Modes de jeu

### 1. Mode Solo
- L'ordinateur génère une combinaison secrète aléatoire
- Vous disposez de 10 essais pour la deviner
- Pas de limite de temps

### 2. Mode Deux Joueurs
- Le **Joueur 1** crée une combinaison secrète (saisie masquée avec des étoiles)
- Le **Joueur 2** tente de deviner cette combinaison
- Idéal pour jouer avec un ami

### 3. Mode Challenge (Temps limité)
- Comme le mode solo, mais avec une **limite de 3 minutes**
- Affichage du temps restant en temps réel
- Plus de pression pour une expérience intense !

### 4. Règles du jeu
- Affichage détaillé des règles et du système de notation

### 5. Statistiques
- Consultation des performances personnelles
- Parties jouées, gagnées, perdues
- Taux de victoire et temps moyen

## Règles du jeu

### Objectif
Deviner la combinaison secrète de 4 couleurs en maximum 10 essais.

### Système de notation
Après chaque essai, vous recevez un feedback :
- **Pions bien placés** - Bonne couleur à la bonne position (affiché en vert)
- **Pions mal placés** - Bonne couleur mais à la mauvaise position (affiché en jaune)

### Points importants
- ⚠️ **Attention** - Une même couleur peut apparaître plusieurs fois dans la combinaison
- Les couleurs sont représentées par des lettres : R, B, J, O, V
- Utilisez les touches de couleur pour saisir votre proposition
- `Backspace` pour corriger, `Entrée` pour valider

### Exemple de feedback
```
 R   J   O   V  => Bien placés : 2  Mal placés : 1
```
Cela signifie que 2 couleurs sont à la bonne position, et 1 couleur est présente mais mal placée.

## Statistiques

Le jeu sauvegarde automatiquement vos statistiques dans le fichier `mastermind_stats.txt` :

- **Parties jouées** - Total des parties
- **Parties gagnées/perdues** - Répartition des résultats
- **Taux de victoire** - Pourcentage de réussite
- **Temps moyen** - Temps moyen pour gagner une partie

Les statistiques sont conservées entre les sessions et se chargent automatiquement au démarrage.

## Configuration requise

- **OS** - Windows (avec terminal supportant les couleurs)
- **.NET** - Framework .NET ou .NET Core
- **Console** - Terminal avec support des couleurs ANSI
- **Résolution** - Console en plein écran recommandée

## Capture d'écran

Le jeu affiche un magnifique titre ASCII art et utilise des couleurs pour une expérience visuelle riche :

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
║                            CRÉÉ PAR : Thomas MAYORAZ (MIN1B)                             ║
║                                PROJET : Démo-Mot - 2025                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════════╝
```

## Auteur

**Thomas MAYORAZ** - Étudiant MIN1B  
Projet réalisé dans le cadre du module "Démo-Mot" à l'ETML en 2025.

---


### Licence

Ce projet est développé dans un cadre éducatif à l'ETML.

---

**Amusez-vous bien et que la logique soit avec vous ! 🧠🎯**