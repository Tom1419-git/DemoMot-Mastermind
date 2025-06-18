/* Title: Mastermind Game
* Autor: Thomas Mayoraz
* Last Updated: 18.06.2025
* Description: Ce programme permettra à l'utilisateur de jouer au mastermind seul ou contre un ami 
*              
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Mastermind_Mayoraz
{
    internal class Program
    {
        // Liste statique et lecture seule des couleurs valides utilisées dans le jeu
        // R = Rouge, B = Bleu, J = Jaune, O = Orange, V = Violet/Magenta
        static readonly List<char> validColors = new List<char> { 'R', 'B', 'J', 'O', 'V' };

        // Dictionnaire associant chaque lettre de couleur à sa couleur console correspondante
        // Permet d'afficher les couleurs de manière visuelle dans la console
        static readonly Dictionary<char, ConsoleColor> colorMap = new Dictionary<char, ConsoleColor>
        {
            { 'R', ConsoleColor.Red },      // Rouge
            { 'B', ConsoleColor.Blue },     // Bleu
            { 'J', ConsoleColor.Yellow },   // Jaune
            { 'O', ConsoleColor.DarkYellow }, // Orange (utilise DarkYellow car pas d'orange direct)
            { 'V', ConsoleColor.Magenta }   // Violet/Magenta
        };

        // Constante définissant le nombre maximum d'essais autorisés par partie
        const int maxAttempts = 10;

        // Nom du fichier où sont sauvegardées les statistiques du joueur
        const string statsFileName = "mastermind_stats.txt";

        // Variables globales pour stocker les statistiques du joueur
        static int gamesWon = 0;           // Nombre de parties gagnées
        static int gamesLost = 0;          // Nombre de parties perdues
        static TimeSpan totalTime = TimeSpan.Zero; // Temps total cumulé de toutes les victoires

        /// <summary>
        /// Point d'entrée principal du programme
        /// Charge les statistiques et affiche le menu principal en boucle
        /// </summary>
        static void Main(string[] args)
        {
            // Charger les statistiques sauvegardées depuis le fichier au démarrage
            LoadStats();

            // Boucle infinie du menu principal jusqu'à ce que l'utilisateur quitte
            while (true)
            {
                // Réinitialiser l'affichage de la console pour une présentation propre
                Console.CursorVisible = true;  // Rendre le curseur visible pour la saisie
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight); // Maximiser la fenêtre
                Console.Clear(); // Effacer l'écran

                // Afficher le titre ASCII art du jeu
                Title();

                // Afficher les options du menu principal
                Console.WriteLine();
                Console.WriteLine("Bienvenue dans mon projet du jeu Mastermind réalisé à l'ETML en 2025 dans le cadre du module Demo-Mot de fin d'année");
                Console.WriteLine();
                Console.WriteLine("Choisissez un mode de jeu :");
                Console.WriteLine("1 - Mode Solo");                    // Jouer contre l'ordinateur
                Console.WriteLine("2 - Mode Deux Joueurs");           // Un joueur crée le code, l'autre devine
                Console.WriteLine("3 - Mode Challenge (temps limité)"); // Mode solo avec limite de temps de 3 minutes
                Console.WriteLine("4 - Règles du jeu");               // Afficher les règles
                Console.WriteLine("5 - Statistiques");                // Afficher les stats du joueur
                Console.WriteLine("6 - Quitter");                     // Quitter le programme

                Console.Write("Votre choix : ");
                string input = Console.ReadLine(); // Lire le choix de l'utilisateur

                // Switch pour traiter le choix de l'utilisateur
                switch (input)
                {
                    case "1":
                        PlaySolo(); // Lancer le mode solo
                        break;
                    case "2":
                        PlayTwoPlayers(); // Lancer le mode deux joueurs
                        break;
                    case "3":
                        Console.CursorVisible = false; // Masquer le curseur pour le mode challenge
                        PlayChallenge(); // Lancer le mode challenge
                        break;
                    case "4":
                        ShowRules(); // Afficher les règles
                        break;
                    case "5":
                        ShowStats(); // Afficher les statistiques
                        break;
                    case "6":
                        SaveStats(); // Sauvegarder les stats avant de quitter
                        return; // Sortir du programme
                    default:
                        // Choix invalide - afficher un message d'erreur et recommencer
                        Console.WriteLine("Choix invalide. Appuyez sur une touche pour recommencer.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Affiche le titre du jeu en ASCII art avec bordures et informations d'auteur
        /// Utilise la couleur cyan pour la mise en valeur
        /// </summary>
        static void Title()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Couleur cyan pour le titre

            // ASCII art "MASTERMIND" avec bordures décoratives
            Console.WriteLine("\t\t\t╔══════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t║                                                                                          ║");
            Console.WriteLine("\t\t\t║   ███╗   ███╗ █████╗ ███████╗████████╗███████╗██████╗ ███╗   ███╗██╗███╗   ██╗██████╗    ║");
            Console.WriteLine("\t\t\t║   ████╗ ████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔══██╗   ║");
            Console.WriteLine("\t\t\t║   ██╔████╔██║███████║███████╗   ██║   █████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║██║  ██║   ║");
            Console.WriteLine("\t\t\t║   ██║╚██╔╝██║██╔══██║╚════██║   ██║   ██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██║  ██║   ║");
            Console.WriteLine("\t\t\t║   ██║ ╚═╝ ██║██║  ██║███████║   ██║   ███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██████╔╝   ║");
            Console.WriteLine("\t\t\t║   ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═════╝    ║");
            Console.WriteLine("\t\t\t║                                                                                          ║");
            Console.WriteLine("\t\t\t║                            CRÉÉ PAR : Thomas MAYORAZ (MIN1B)                             ║");
            Console.WriteLine("\t\t\t║                                PROJET : Démo-Mot - 2025                                  ║");
            Console.WriteLine("\t\t\t║                                                                                          ║");
            Console.WriteLine("\t\t\t╚══════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.ResetColor(); // Rétablir les couleurs par défaut de la console
        }

        /// <summary>
        /// Affiche les règles complètes du jeu Mastermind
        /// Explique le but, les couleurs disponibles, le système de notation et les rôles
        /// </summary>
        static void ShowRules()
        {
            Console.Clear();
            Title(); // Afficher le titre en haut de l'écran des règles

            // Explication du but du jeu
            Console.WriteLine("\tBUT : Deviner la combinaison secrète générée par l'ordinateur ou par un des joueurs,");
            Console.WriteLine("\t      composée de 4 couleurs en 10 essais maximums");

            // Affichage des couleurs possibles avec leur représentation visuelle
            Console.Write("\tCOULEURS POSSIBLES : ");
            char[] colorOrder = { 'R', 'B', 'J', 'O', 'V' }; // Ordre d'affichage des couleurs
            foreach (char c in colorOrder)
            {
                // Afficher chaque couleur avec sa couleur de fond correspondante
                Console.BackgroundColor = colorMap[c];
                Console.ForegroundColor = ConsoleColor.Black; // Texte noir pour contraste
                Console.Write($" {c} ");
                Console.ResetColor(); // Rétablir les couleurs par défaut
                Console.Write(" "); // Espace entre les couleurs
            }
            Console.WriteLine();
            Console.WriteLine();

            // Explication du système de feedback après chaque essai
            Console.WriteLine("\t\tAprès chaque essai, vous verrez sur votre écran :");
            Console.WriteLine("\tNombre de pions bien placés (Bonne couleur et bonne position) ");
            Console.WriteLine("\tNombre de pions mal placés (Bonne couleur mais mauvaise position) ");
            Console.WriteLine();

            // Explication des rôles dans le mode deux joueurs
            Console.WriteLine("\t\tIl y a deux rôles principaux dans une partie de Mastermind à deux joueurs :");
            Console.WriteLine("\tCodeur: Cette personne crée un code secret composé de quatre pions couleur, choisis parmi");
            Console.WriteLine("\t        les couleurs possibles. Le code est placé secrètement.");
            Console.WriteLine("\tDécodeur: Cette personne tente de découvrir le code en proposant différentes combinaisons");
            Console.WriteLine("\t          de pions et de couleurs à chaque tour. Il dispose d'un maximum de dix tours pour y parvenir.");
            Console.WriteLine();

            // Information sur le mode solo
            Console.WriteLine("\tSi vous préférez jouer seul, l'ordinateur se chargera de créer la suite de couleurs à deviner.");

            // Avertissement important sur les répétitions de couleurs
            Console.ForegroundColor = ConsoleColor.Red; // Couleur rouge pour l'avertissement
            Console.WriteLine("\t▲ Attention ▲ - Chaque couleur peut se retrouver plusieurs fois dans la combinaison secrète");
            Console.WriteLine();
            Console.ResetColor();

            // Instruction pour retourner au menu
            Console.Write("Appuyez sur une touche pour revenir à l'accueil et choisir votre mode de jeu ");
            Console.ReadKey(); // Attendre une pression de touche
        }

        /// <summary>
        /// Lance le mode Challenge avec limitation de temps (3 minutes)
        /// Génère un code aléatoirement et lance le jeu en mode challenge
        /// </summary>
        static void PlayChallenge()
        {
            char[] secretCode = GenerateRandomCode(); // Générer un code secret aléatoire
            RunGame(secretCode, challengeMode: true); // Lancer le jeu en mode challenge
        }

        /// <summary>
        /// Lance le mode Solo classique sans limitation de temps
        /// Génère un code aléatoirement et lance le jeu en mode normal
        /// </summary>
        static void PlaySolo()
        {
            char[] secretCode = GenerateRandomCode(); // Générer un code secret aléatoire
            RunGame(secretCode); // Lancer le jeu en mode normal
        }

        /// <summary>
        /// Lance le mode Deux Joueurs
        /// Le premier joueur saisit le code secret, le second doit le deviner
        /// </summary>
        static void PlayTwoPlayers()
        {
            Console.Clear();
            Title();

            // Instructions pour le joueur 1 (celui qui crée le code)
            Console.Write("JOUEUR 1 : Entrez la combinaison secrète contenant les lettres ");

            // Afficher les couleurs disponibles avec leur représentation visuelle
            char[] colorOrder = { 'R', 'B', 'J', 'O', 'V' };
            foreach (char c in colorOrder)
            {
                Console.BackgroundColor = colorMap[c];
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {c} ");
                Console.ResetColor();
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("Utilisez les touches pour saisir votre code secret :");
            Console.CursorVisible = false; // Masquer le curseur pendant la saisie secrète

            // Obtenir le code secret du joueur 1 (saisie masquée)
            char[] secretCode = GetSecretCodeInput();

            Console.CursorVisible = true; // Rétablir la visibilité du curseur

            // Confirmation et transition vers le joueur 2
            Console.WriteLine("\nCombinaison enregistrée. Passez au JOUEUR 2 qui devra deviner le code entré.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(); // Pause pour permettre le changement de joueur
            Console.Clear();

            RunGame(secretCode); // Lancer le jeu avec le code créé par le joueur 1
        }

        /// <summary>
        /// Permet au joueur 1 de saisir le code secret de manière masquée
        /// Affiche des étoiles au lieu des lettres pour garder le secret
        /// Gère la navigation avec Backspace et valide les couleurs
        /// </summary>
        /// <returns>Tableau de 4 caractères représentant le code secret</returns>
        static char[] GetSecretCodeInput()
        {
            char[] secretCode = new char[4]; // Code secret de 4 couleurs
            int index = 0; // Position actuelle dans le code

            // Boucle de saisie jusqu'à ce que les 4 positions soient remplies
            while (index < 4)
            {
                // Afficher la saisie en cours avec des étoiles pour les positions remplies
                Console.Write("\rCode secret : ");
                for (int i = 0; i < index; i++)
                {
                    Console.Write("* "); // Étoile pour les positions déjà saisies
                }
                for (int i = index; i < 4; i++)
                {
                    Console.Write("_ "); // Underscore pour les positions vides
                }

                // Lire la touche pressée sans l'afficher
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char keyChar = Char.ToUpper(keyInfo.KeyChar); // Convertir en majuscule

                if (keyInfo.Key == ConsoleKey.Backspace && index > 0)
                {
                    // Backspace : reculer d'une position si possible
                    index--;
                    secretCode[index] = '\0'; // Effacer la valeur
                }
                else if (validColors.Contains(keyChar) && index < 4)
                {
                    // Couleur valide : l'ajouter au code et avancer
                    secretCode[index++] = keyChar;
                }
                // Ignorer les autres touches
            }

            // Afficher le code final avec toutes les étoiles
            Console.Write("\rCode secret : ");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
            return secretCode;
        }

        /// <summary>
        /// Génère un code secret aléatoire de 4 couleurs
        /// Les couleurs peuvent se répéter dans le code
        /// </summary>
        /// <returns>Tableau de 4 caractères représentant le code aléatoire</returns>
        static char[] GenerateRandomCode()
        {
            Random rand = new Random(); // Générateur de nombres aléatoires
            char[] code = new char[4];   // Code de 4 couleurs

            // Remplir chaque position avec une couleur aléatoire
            for (int i = 0; i < 4; i++)
            {
                code[i] = validColors[rand.Next(validColors.Count)]; // Choisir une couleur au hasard
            }
            return code;
        }

        /// <summary>
        /// Méthode principale qui gère le déroulement d'une partie
        /// Gère les essais, l'historique, le chronométrage et les conditions de victoire/défaite
        /// </summary>
        /// <param name="secretCode">Le code secret à deviner</param>
        /// <param name="challengeMode">Si true, active la limite de temps de 3 minutes</param>
        static void RunGame(char[] secretCode, bool challengeMode = false)
        {
            Stopwatch stopwatch = new Stopwatch(); // Chronomètre pour mesurer le temps
            stopwatch.Start();

            // Liste pour stocker l'historique des essais avec leurs résultats
            List<Tuple<char[], int, int>> history = new List<Tuple<char[], int, int>>();

            // Boucle principale du jeu pour chaque essai
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                Console.Clear();
                Title(); // Afficher le titre à chaque essai

                // Vérification du temps en mode challenge
                if (challengeMode)
                {
                    TimeSpan remaining = TimeSpan.FromMinutes(3) - stopwatch.Elapsed; // Temps restant
                    if (remaining <= TimeSpan.Zero)
                    {
                        // Temps écoulé - fin de partie par timeout
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n TEMPS ÉCOULÉ ! Vous avez dépassé la limite de 3 minutes.");
                        Console.Write("La combinaison secrète était : ");
                        DisplayColoredCode(secretCode); // Révéler le code secret
                        Console.ResetColor();
                        gamesLost++; // Incrémenter les défaites
                        SaveStats(); // Sauvegarder les statistiques 
                        EndGame(); // Afficher les options de fin de partie
                        return;
                    }
                }

                // Afficher le numéro d'essai actuel
                Console.WriteLine($"Essai {attempt} / {maxAttempts}");

                // Afficher l'historique des essais précédents
                ShowHistory(history);

                char[] guess; // Variable pour stocker la proposition du joueur

                if (challengeMode)
                {
                    // Mode challenge : saisie avec affichage du temps restant
                    Console.CursorVisible = false;
                    guess = GetPlayerGuess(true, stopwatch);
                    if (guess == null)
                    {
                        // Temps écoulé pendant la saisie
                        Console.Clear();
                        Title();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n TEMPS ÉCOULÉ ! Vous avez dépassé la limite de 3 minutes.");
                        Console.Write("La combinaison secrète était : ");
                        DisplayColoredCode(secretCode);
                        Console.ResetColor();
                        gamesLost++;
                        EndGame();
                        return;
                    }
                }
                else
                {
                    // Mode normal : saisie sans contrainte de temps
                    Console.CursorVisible = false;
                    guess = GetPlayerGuess();
                }

                // Évaluer la proposition par rapport au code secret
                Tuple<int, int> result = EvaluateGuess(secretCode, guess);

                // Ajouter l'essai à l'historique (proposition, bien placés, mal placés)
                history.Add(new Tuple<char[], int, int>(guess, result.Item1, result.Item2));

                // Vérifier si le joueur a trouvé le code (4 couleurs bien placées)
                if (result.Item1 == 4)
                {
                    stopwatch.Stop(); // Arrêter le chronomètre
                    TimeSpan timeTakenVictory = stopwatch.Elapsed;

                    // Afficher le message de victoire
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nFÉLICITATIONS ! Vous avez trouvé la combinaison secrète qui était : ");
                    DisplayColoredCode(secretCode);
                    Console.WriteLine($"Temps écoulé : {timeTakenVictory.Minutes} min {timeTakenVictory.Seconds} sec");
                    Console.ResetColor();

                    // Mettre à jour les statistiques de victoire
                    gamesWon++;
                    totalTime += timeTakenVictory;
                    SaveStats();

                    Console.CursorVisible = true;
                    EndGame();
                    return; // Sortir de la méthode (victoire)
                }
            }

            // Si on arrive ici, le joueur n'a pas trouvé en maxAttempts essais
            stopwatch.Stop();
            TimeSpan timeTakenDefeat = stopwatch.Elapsed;

            // Afficher le message de défaite
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nDÉSOLÉ, vous avez utilisé vos 10 essais.");
            Console.Write("La combinaison secrète était : ");
            DisplayColoredCode(secretCode); // Révéler le code secret
            Console.WriteLine($"Temps écoulé : {timeTakenDefeat.Minutes} min {timeTakenDefeat.Seconds} sec");
            Console.ResetColor();

            // Mettre à jour les statistiques de défaite
            gamesLost++;
            totalTime += timeTakenDefeat;
            SaveStats();

            Console.CursorVisible = true;
            EndGame();
        }

        /// <summary>
        /// Affiche l'historique de tous les essais précédents avec leurs résultats
        /// Montre les couleurs disponibles, puis chaque essai avec le feedback
        /// </summary>
        /// <param name="history">Liste des essais précédents (proposition, bien placés, mal placés)</param>
        static void ShowHistory(List<Tuple<char[], int, int>> history)
        {
            // Afficher les couleurs disponibles en rappel
            Console.Write("COULEURS POSSIBLES : ");
            char[] colorOrder = { 'R', 'B', 'J', 'O', 'V' };
            foreach (char c in colorOrder)
            {
                Console.BackgroundColor = colorMap[c];
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {c} ");
                Console.ResetColor();
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("Historique des essais :\n");

            // Parcourir et afficher chaque essai de l'historique
            foreach (var entry in history)
            {
                char[] guess = entry.Item1;      // La proposition
                int wellPlaced = entry.Item2;    // Nombre de bien placés
                int misplaced = entry.Item3;     // Nombre de mal placés

                // Afficher la proposition avec les couleurs
                foreach (char c in guess)
                {
                    Console.BackgroundColor = colorMap[c];
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($" {c} ");
                    Console.ResetColor();
                    Console.Write(" ");
                }

                // Afficher le résultat de l'évaluation
                Console.Write($"=> ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Bien placés : {wellPlaced}  ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Mal placés : {misplaced}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine(); // Ligne vide entre les essais
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Gère la saisie de la proposition du joueur
        /// Deux modes : normal (interactif) et challenge (avec affichage du temps)
        /// </summary>
        /// <param name="challengeMode">Si true, affiche le temps restant et peut retourner null si timeout</param>
        /// <param name="stopwatch">Chronomètre pour le mode challenge</param>
        /// <returns>Proposition du joueur ou null si timeout en mode challenge</returns>
        static char[] GetPlayerGuess(bool challengeMode = false, Stopwatch stopwatch = null)
        {
            char[] guess = new char[4]; // Proposition de 4 couleurs
            int index = 0; // Position actuelle dans la proposition

            if (!challengeMode)
            {
                // Mode normal - saisie interactive colorée sans contrainte de temps
                Console.WriteLine("Entrez votre proposition (utilisez les touches R, B, J, O, V) :");

                while (true)
                {
                    // Afficher la saisie en cours avec les couleurs
                    Console.Write("\rProposition : ");
                    for (int i = 0; i < index; i++)
                    {
                        // Afficher les couleurs déjà saisies avec leur couleur de fond
                        Console.BackgroundColor = colorMap[guess[i]];
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($" {guess[i]} ");
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                    for (int i = index; i < 4; i++)
                    {
                        Console.Write("_ "); // Positions vides
                    }

                    // Lire la touche pressée
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    char keyChar = Char.ToUpper(keyInfo.KeyChar);

                    if (keyInfo.Key == ConsoleKey.Backspace && index > 0)
                    {
                        // Backspace : reculer d'une position
                        index--;
                        guess[index] = '\0';
                    }
                    else if (validColors.Contains(keyChar) && index < 4)
                    {
                        // Couleur valide : l'ajouter
                        guess[index++] = keyChar;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter && index == 4)
                    {
                        // Entrée valide si 4 couleurs saisies
                        Console.WriteLine();
                        return guess;
                    }
                    // Ignorer les autres touches
                }
            }
            else
            {
                // Mode Challenge avec minuteur en direct
                if (stopwatch == null) throw new ArgumentNullException(nameof(stopwatch));

                Console.WriteLine("Entrez votre proposition (4 lettres, sans espaces) : ");

                while (true)
                {
                    // Vérifier si le temps est écoulé
                    TimeSpan remaining = TimeSpan.FromMinutes(3) - stopwatch.Elapsed;
                    if (remaining <= TimeSpan.Zero)
                    {
                        return null; // Temps écoulé
                    }

                    // Sauvegarder la position actuelle du curseur
                    int curLeft = Console.CursorLeft;
                    int curTop = Console.CursorTop;

                    // Afficher le temps restant à une position fixe
                    Console.SetCursorPosition(14, 14);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Temps restant : {remaining.Minutes:D2} min {remaining.Seconds:D2} sec   ");
                    Console.ResetColor();

                    // Rétablir la position du curseur pour la saisie
                    Console.SetCursorPosition(curLeft, curTop);

                    // Afficher la saisie en cours
                    Console.Write("\rProposition : ");
                    for (int i = 0; i < index; i++)
                    {
                        Console.BackgroundColor = colorMap[guess[i]];
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($" {guess[i]} ");
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                    for (int i = index; i < 4; i++)
                    {
                        Console.Write("_ ");
                    }

                    // Lire les touches disponibles (non-bloquant)
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        char keyChar = Char.ToUpper(keyInfo.KeyChar);

                        if (keyInfo.Key == ConsoleKey.Backspace && index > 0)
                        {
                            index--;
                            guess[index] = '\0';
                        }
                        else if (validColors.Contains(keyChar) && index < 4)
                        {
                            guess[index++] = keyChar;
                        }
                        else if (keyInfo.Key == ConsoleKey.Enter && index == 4)
                        {
                            Console.WriteLine();
                            return guess;
                        }
                        // Ignorer les autres touches
                    }

                    // Petite pause pour éviter de surcharger le CPU
                    System.Threading.Thread.Sleep(50);
                }
            }
        }

        /// <summary>
        /// Évalue une proposition par rapport au code secret
        /// Calcule le nombre de couleurs bien placées et mal placées
        /// Utilise un algorithme en deux passes pour éviter les doublons
        /// </summary>
        /// <param name="secretCode">Le code secret à deviner</param>
        /// <param name="guess">La proposition du joueur</param>
        /// <returns>Tuple contenant (bien placés, mal placés)</returns>
        static Tuple<int, int> EvaluateGuess(char[] secretCode, char[] guess)
        {
            int wellPlaced = 0;  // Compteur des couleurs bien placées
            int misplaced = 0;   // Compteur des couleurs mal placées

            // Tableaux pour marquer les positions déjà utilisées dans le calcul
            bool[] secretUsed = new bool[4];  // Positions du code secret déjà comptées
            bool[] guessUsed = new bool[4];   // Positions de la proposition déjà comptées

            // Première passe : compter les couleurs bien placées (même couleur, même position)
            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == secretCode[i])
                {
                    wellPlaced++;        // Incrémenter le compteur de bien placés
                    secretUsed[i] = true; // Marquer cette position du code secret comme utilisée
                    guessUsed[i] = true;  // Marquer cette position de la proposition comme utilisée
                }
            }

            // Deuxième passe : compter les couleurs mal placées (bonne couleur, mauvaise position)
            for (int i = 0; i < 4; i++)
            {
                if (guessUsed[i])
                    continue; // Ignorer les positions déjà comptées comme bien placées

                // Chercher si cette couleur existe ailleurs dans le code secret
                for (int j = 0; j < 4; j++)
                {
                    if (!secretUsed[j] && guess[i] == secretCode[j])
                    {
                        misplaced++;      // Incrémenter le compteur de mal placés
                        secretUsed[j] = true; // Marquer cette position du code secret comme utilisée
                        break; // Sortir de la boucle interne pour éviter les doublons
                    }
                }
            }

            return new Tuple<int, int>(wellPlaced, misplaced);
        }

        /// <summary>
        /// Affiche un code (secret ou proposition) avec les couleurs de fond correspondantes
        /// Utilisé pour révéler le code secret ou afficher les propositions
        /// </summary>
        /// <param name="code">Le code à afficher</param>
        static void DisplayColoredCode(char[] code)
        {
            foreach (char c in code)
            {
                // Afficher chaque couleur avec sa couleur de fond
                Console.BackgroundColor = colorMap[c];
                Console.ForegroundColor = ConsoleColor.Black; // Texte noir pour le contraste
                Console.Write($" {c} ");
                Console.ResetColor();
                Console.Write(" "); // Espace entre les couleurs
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Affiche les statistiques complètes du joueur
        /// Calcule et montre les parties jouées, le taux de victoire et le temps moyen
        /// </summary>
        static void ShowStats()
        {
            Console.Clear();
            Title(); // Afficher le titre en haut des statistiques

            int totalGames = gamesWon + gamesLost; // Calculer le total de parties

            // Afficher les statistiques de base
            Console.WriteLine($"Parties jouées : {totalGames}");
            Console.WriteLine($"Parties gagnées : {gamesWon}");
            Console.WriteLine($"Parties perdues : {gamesLost}");

            // Calculer et afficher le taux de victoire si au moins une partie jouée
            if (totalGames > 0)
            {
                double winRate = (double)gamesWon / totalGames * 100;
                Console.WriteLine($"Taux de victoire : {winRate:F1}%");
            }

            // Calculer et afficher le temps moyen de victoire si au moins une victoire
            if (gamesWon > 0)
            {
                TimeSpan avgTime = new TimeSpan(totalTime.Ticks / gamesWon);
                Console.WriteLine($"Temps moyen pour gagner : {avgTime.Minutes} min {avgTime.Seconds} sec");
            }
            else
            {
                Console.WriteLine("Aucune victoire enregistrée, pas de temps moyen à afficher.");
            }

            Console.WriteLine();
            Console.Write("Appuyez sur une touche pour revenir au menu principal...");
            Console.ReadKey(); // Attendre une pression de touche
        }

        /// <summary>
        /// Charge les statistiques depuis le fichier de sauvegarde
        /// Initialise les variables globales avec les données sauvegardées
        /// En cas d'erreur, conserve les valeurs par défaut (0)
        /// </summary>
        static void LoadStats()
        {
            try
            {
                // Vérifier si le fichier de statistiques existe
                if (File.Exists(statsFileName))
                {
                    string[] lines = File.ReadAllLines(statsFileName); // Lire toutes les lignes
                    if (lines.Length >= 3) // Vérifier qu'il y a au moins 3 lignes
                    {
                        // Ligne 0 : parties gagnées
                        gamesWon = int.Parse(lines[0]);
                        // Ligne 1 : parties perdues
                        gamesLost = int.Parse(lines[1]);
                        // Ligne 2 : temps total en ticks
                        totalTime = TimeSpan.FromTicks(long.Parse(lines[2]));
                    }
                }
            }
            catch (Exception)
            {
                // En cas d'erreur (fichier corrompu, etc.), garder les valeurs par défaut
                gamesWon = 0;
                gamesLost = 0;
                totalTime = TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Sauvegarde les statistiques actuelles dans le fichier
        /// Écrit les données sur 3 lignes : gagnées, perdues, temps total
        /// Ignore silencieusement les erreurs pour ne pas interrompre le jeu
        /// </summary>
        static void SaveStats()
        {
            try
            {
                string[] lines = new string[3]; // Tableau de 3 lignes
                lines[0] = gamesWon.ToString();     // Parties gagnées
                lines[1] = gamesLost.ToString();    // Parties perdues
                lines[2] = totalTime.Ticks.ToString(); // Temps total en ticks (plus précis)
                File.WriteAllLines(statsFileName, lines); // Écrire dans le fichier
            }
            catch (Exception)
            {
                // En cas d'erreur de sauvegarde, ignorer silencieusement
                // pour ne pas interrompre le flux du jeu
            }
        }

        /// <summary>
        /// Gère les options de fin de partie (retour menu ou quitter)
        /// Permet au joueur de choisir s'il veut rejouer ou quitter le programme
        /// Sauvegarde les statistiques avant de quitter si demandé
        /// </summary>
        static void EndGame()
        {
            Console.WriteLine("\nQue voulez-vous faire ?");
            Console.WriteLine("1 - Revenir au menu principal"); // Rejouer
            Console.WriteLine("2 - Quitter");                   // Quitter le programme

            // Boucle jusqu'à obtenir un choix valide
            while (true)
            {
                Console.Write("Votre choix : ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        return; // Retourner au menu principal (sortir de la méthode)
                    case "2":
                        SaveStats(); // Sauvegarder les statistiques avant de quitter
                        Environment.Exit(0); // Quitter le programme complètement
                        break;
                    default:
                        // Choix invalide - afficher un message d'erreur et recommencer
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Choix invalide, veuillez réessayer.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}