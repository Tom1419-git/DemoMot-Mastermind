<div align="center">

# 🎯 Mastermind — DemoMot

**Jeu Mastermind en C# (WinForms) — projet ETML, version finale**

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D4?style=for-the-badge&logo=windows&logoColor=white)

</div>

---

## 📖 Présentation

**Mastermind** développé en C# avec Windows Forms dans le cadre du module **DemoMot** à l'ETML. Le joueur doit deviner une combinaison secrète en un nombre d'essais limité, avec retour visuel sur chaque proposition (bien placés / mal placés).

Le projet met l'accent sur la logique de jeu, l'interface WinForms et la persistance des résultats.

## ✨ Fonctionnalités

- 🎮 **Boucle de jeu complète** — propositions, validation et retour par couleur
- 💾 **Statistiques persistantes** — les résultats des parties sont sauvegardés localement
- 📦 **Exécutable autonome** — dépendances intégrées (Fody/Costura), aucun instal nécessaire
- 🖥️ **Interface WinForms** — application desktop native Windows

## 🚀 Lancement

**Option 1 — exécutable prêt à l'emploi** : télécharger `bin/Release/Mastermind-Mayoraz_DemoMot.exe` et le lancer directement.

**Option 2 — compilation** : ouvrir `Mastermind-Mayoraz_DemoMot.sln` dans Visual Studio, puis générer la solution (cible .NET Framework 4.7.2).

## 🛠️ Stack technique

- **Langage** : C# (.NET Framework 4.7.2)
- **Interface** : Windows Forms
- **Packaging** : Fody + Costura (fusion des dépendances en un seul .exe)

---

<div align="center"><i>Projet scolaire ETML — Thomas Mayoraz © 2026</i></div>
