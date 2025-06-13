/* Title: Mastermind Game
* Autor: Thomas Mayoraz
* Last Updated: 12.06.2025
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
        static readonly List<char> validColors = new List<char> { 'R', 'B', 'J', 'O', 'V' };
        static readonly Dictionary<char, ConsoleColor> colorMap = new Dictionary<char, ConsoleColor>
        {
            { 'R', ConsoleColor.Red },
            { 'B', ConsoleColor.Blue },
            { 'J', ConsoleColor.Yellow },
            { 'O', ConsoleColor.DarkYellow },
            { 'V', ConsoleColor.Magenta }
        };

        const int maxAttempts = 10;
        const string statsFileName = "mastermind_stats.txt";

        // Statistiques globales
        static int gamesWon = 0;
        static int gamesLost = 0;
        static TimeSpan totalTime = TimeSpan.Zero;

        static void Main(string[] args)
        {
            // Charger les statistiques au démarrage
            LoadStats();

            while (true)
            {
                Console.CursorVisible = true;
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.Clear();
                Title();
                Console.WriteLine();
                Console.WriteLine("Bienvenue dans mon projet du jeu Mastermind réalisé à l'ETML en 2025 dans le cadre du module Demo-Mot de fin d'année");
                Console.WriteLine();
                Console.WriteLine("Choisissez un mode de jeu :");
                Console.WriteLine("1 - Mode Solo");
                Console.WriteLine("2 - Mode Deux Joueurs");
                Console.WriteLine("3 - Mode Challenge (temps limité)");
                Console.WriteLine("4 - Règles du jeu");
                Console.WriteLine("5 - Statistiques");
                Console.WriteLine("6 - Quitter");

                Console.Write("Votre choix : ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PlaySolo();
                        break;
                    case "2":
                        PlayTwoPlayers();
                        break;
                    case "3":
                        Console.CursorVisible = false;
                        PlayChallenge();
                        break;
                    case "4":
                        ShowRules();
                        break;
                    case "5":
                        ShowStats();
                        break;
                    case "6":
                        SaveStats();
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Appuyez sur une touche pour recommencer.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void Title()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
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
            Console.ResetColor();
        }

        static void ShowRules()
        {
            Console.Clear();
            Title();
            Console.WriteLine("\tBUT : Deviner la combinaison secrète générée par l'ordinateur ou par un des joueurs,");
            Console.WriteLine("\t      composée de 4 couleurs en 10 essais maximums");
            Console.Write("\tCOULEURS POSSIBLES : ");
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
            Console.WriteLine();
            Console.WriteLine("\t\tAprès chaque essai, vous verrez sur votre écran :");
            Console.WriteLine("\tNombre de pions bien placés (Bonne couleur et bonne position) ");
            Console.WriteLine("\tNombre de pions mal placés (Bonne couleur mais mauvaise position) ");
            Console.WriteLine();
            Console.WriteLine("\t\tIl y a deux rôles principaux dans une partie de Mastermind à deux joueurs :");
            Console.WriteLine("\tCodeur: Cette personne crée un code secret composé de quatre pions couleur, choisis parmi");
            Console.WriteLine("\t        les couleurs possibles. Le code est placé secrètement.");
            Console.WriteLine("\tDécodeur: Cette personne tente de découvrir le code en proposant différentes combinaisons");
            Console.WriteLine("\t          de pions et de couleurs à chaque tour. Il dispose d'un maximum de dix tours pour y parvenir.");
            Console.WriteLine();
            Console.WriteLine("\tSi vous préférez jouer seul, l'ordinateur se chargera de créer la suite de couleurs à deviner.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t▲ Attention ▲ - Chaque couleur peut se retrouver plusieurs fois dans la combinaison secrète");
            Console.WriteLine();
            Console.ResetColor();
            Console.Write("Appuyez sur une touche pour revenir à l'accueil et choisir votre mode de jeu ");
            Console.ReadKey();
        }

        static void PlayChallenge()
        {
            char[] secretCode = GenerateRandomCode();
            RunGame(secretCode, challengeMode: true);
        }

        static void PlaySolo()
        {
            char[] secretCode = GenerateRandomCode();
            RunGame(secretCode);
        }

        static void PlayTwoPlayers()
        {
            Console.Clear();
            Title();
            Console.Write("JOUEUR 1 : Entrez la combinaison secrète contenant les lettres ");
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
            Console.CursorVisible = false;

            char[] secretCode = GetSecretCodeInput();

            Console.CursorVisible = true;
            Console.WriteLine("\nCombinaison enregistrée. Passez au JOUEUR 2 qui devra deviner le code entré.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear();
            RunGame(secretCode);
        }

        static char[] GetSecretCodeInput()
        {
            char[] secretCode = new char[4];
            int index = 0;



            while (index < 4)
            {
                // Afficher la saisie en cours avec des étoiles simples
                Console.Write("\rCode secret : ");
                for (int i = 0; i < index; i++)
                {
                    Console.Write("* ");
                }
                for (int i = index; i < 4; i++)
                {
                    Console.Write("_ ");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char keyChar = Char.ToUpper(keyInfo.KeyChar);

                if (keyInfo.Key == ConsoleKey.Backspace && index > 0)
                {
                    index--;
                    secretCode[index] = '\0';
                }
                else if (validColors.Contains(keyChar) && index < 4)
                {
                    secretCode[index++] = keyChar;
                }
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


        static char[] GenerateRandomCode()
        {
            Random rand = new Random();
            char[] code = new char[4];
            for (int i = 0; i < 4; i++)
            {
                code[i] = validColors[rand.Next(validColors.Count)];
            }
            return code;
        }

        static void RunGame(char[] secretCode, bool challengeMode = false)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Tuple<char[], int, int>> history = new List<Tuple<char[], int, int>>();

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                Console.Clear();
                Title();

                if (challengeMode)
                {
                    TimeSpan remaining = TimeSpan.FromMinutes(3) - stopwatch.Elapsed;
                    if (remaining <= TimeSpan.Zero)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n TEMPS ÉCOULÉ ! Vous avez dépassé la limite de 3 minutes.");
                        Console.Write("La combinaison secrète était : ");
                        DisplayColoredCode(secretCode);
                        Console.ResetColor();
                        gamesLost++;
                        SaveStats(); // Sauvegarder en cas de timeout pendant la saisie
                        SaveStats(); // Sauvegarder en cas de timeout
                        EndGame();
                        return;
                    }
                }

                Console.WriteLine($"Essai {attempt} / {maxAttempts}");
                ShowHistory(history);
                char[] guess;

                if (challengeMode)
                {
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
                    Console.CursorVisible = false;
                    guess = GetPlayerGuess();
                }

                Tuple<int, int> result = EvaluateGuess(secretCode, guess);
                history.Add(new Tuple<char[], int, int>(guess, result.Item1, result.Item2));

                if (result.Item1 == 4)
                {
                    stopwatch.Stop();
                    TimeSpan timeTakenVictory = stopwatch.Elapsed;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nFÉLICITATIONS ! Vous avez trouvé la combinaison secrète qui était : ");
                    DisplayColoredCode(secretCode);
                    Console.WriteLine($"Temps écoulé : {timeTakenVictory.Minutes} min {timeTakenVictory.Seconds} sec");
                    Console.ResetColor();

                    gamesWon++;
                    totalTime += timeTakenVictory;
                    SaveStats(); // Sauvegarder après chaque victoire

                    Console.CursorVisible = true;
                    EndGame();
                    return;
                }
            }

            // Si on arrive ici, ça veut dire que le joueur n'a pas trouvé en maxAttempts
            stopwatch.Stop();
            TimeSpan timeTakenDefeat = stopwatch.Elapsed;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nDÉSOLÉ, vous avez utilisé vos 10 essais.");
            Console.Write("La combinaison secrète était : ");
            DisplayColoredCode(secretCode);
            Console.WriteLine($"Temps écoulé : {timeTakenDefeat.Minutes} min {timeTakenDefeat.Seconds} sec");
            Console.ResetColor();

            gamesLost++;
            totalTime += timeTakenDefeat;
            SaveStats(); // Sauvegarder après chaque défaite

            Console.CursorVisible = true;
            EndGame();
        }

        static void ShowHistory(List<Tuple<char[], int, int>> history)
        {
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

            foreach (var entry in history)
            {
                char[] guess = entry.Item1;
                int wellPlaced = entry.Item2;
                int misplaced = entry.Item3;

                foreach (char c in guess)
                {
                    Console.BackgroundColor = colorMap[c];
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($" {c} ");
                    Console.ResetColor();
                    Console.Write(" ");
                }

                Console.Write($"=> ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Bien placés : {wellPlaced}  ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Mal placés : {misplaced}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static char[] GetPlayerGuess(bool challengeMode = false, Stopwatch stopwatch = null)
        {
            char[] guess = new char[4];
            int index = 0;

            if (!challengeMode)
            {
                // Mode normal - saisie interactive colorée sans contrainte de temps
                Console.WriteLine("Entrez votre proposition (utilisez les touches R, B, J, O, V) :");

                while (true)
                {
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
                }
            }
            else
            {
                // Mode Challenge avec minuteur en direct
                if (stopwatch == null) throw new ArgumentNullException(nameof(stopwatch));

                Console.WriteLine("Entrez votre proposition (4 lettres, sans espaces) : ");

                while (true)
                {
                    // Afficher le temps restant en haut
                    TimeSpan remaining = TimeSpan.FromMinutes(3) - stopwatch.Elapsed;
                    if (remaining <= TimeSpan.Zero)
                    {
                        return null; // Temps écoulé, on gère ça en appelant la méthode
                    }

                    // Sauvegarder position curseur
                    int curLeft = Console.CursorLeft;
                    int curTop = Console.CursorTop;

                    // Afficher temps restant
                    Console.SetCursorPosition(14, 14);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Temps restant : {remaining.Minutes:D2} min {remaining.Seconds:D2} sec   ");
                    Console.ResetColor();

                    // Rétablir position curseur pour saisir la lettre
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

                    // Lire la touche
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
                        // Sinon ignorer la touche
                    }

                    // Petite pause pour éviter de trop charger le CPU
                    System.Threading.Thread.Sleep(50);
                }
            }
        }

        static Tuple<int, int> EvaluateGuess(char[] secretCode, char[] guess)
        {
            int wellPlaced = 0;
            int misplaced = 0;

            bool[] secretUsed = new bool[4];
            bool[] guessUsed = new bool[4];

            // Première passe : compte les bien placés
            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == secretCode[i])
                {
                    wellPlaced++;
                    secretUsed[i] = true;
                    guessUsed[i] = true;
                }
            }

            // Deuxième passe : compte les mal placés
            for (int i = 0; i < 4; i++)
            {
                if (guessUsed[i])
                    continue;

                for (int j = 0; j < 4; j++)
                {
                    if (!secretUsed[j] && guess[i] == secretCode[j])
                    {
                        misplaced++;
                        secretUsed[j] = true;
                        break;
                    }
                }
            }

            return new Tuple<int, int>(wellPlaced, misplaced);
        }

        static void DisplayColoredCode(char[] code)
        {
            foreach (char c in code)
            {
                Console.BackgroundColor = colorMap[c];
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {c} ");
                Console.ResetColor();
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        static void ShowStats()
        {
            Console.Clear();
            Title();

            int totalGames = gamesWon + gamesLost;

            Console.WriteLine($"Parties jouées : {totalGames}");
            Console.WriteLine($"Parties gagnées : {gamesWon}");
            Console.WriteLine($"Parties perdues : {gamesLost}");

            if (totalGames > 0)
            {
                double winRate = (double)gamesWon / totalGames * 100;
                Console.WriteLine($"Taux de victoire : {winRate:F1}%");
            }

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
            Console.ReadKey();
        }

        static void LoadStats()
        {
            try
            {
                if (File.Exists(statsFileName))
                {
                    string[] lines = File.ReadAllLines(statsFileName);
                    if (lines.Length >= 3)
                    {
                        gamesWon = int.Parse(lines[0]);
                        gamesLost = int.Parse(lines[1]);
                        totalTime = TimeSpan.FromTicks(long.Parse(lines[2]));
                    }
                }
            }
            catch (Exception)
            {
                // En cas d'erreur, on garde les valeurs par défaut (0)
                gamesWon = 0;
                gamesLost = 0;
                totalTime = TimeSpan.Zero;
            }
        }

        static void SaveStats()
        {
            try
            {
                string[] lines = new string[3];
                lines[0] = gamesWon.ToString();
                lines[1] = gamesLost.ToString();
                lines[2] = totalTime.Ticks.ToString();
                File.WriteAllLines(statsFileName, lines);
            }
            catch (Exception)
            {
                // En cas d'erreur de sauvegarde, on ignore silencieusement
                // pour ne pas interrompre le jeu
            }
        }

        static void EndGame()
        {
            Console.WriteLine("\nQue voulez-vous faire ?");
            Console.WriteLine("1 - Revenir au menu principal");
            Console.WriteLine("2 - Quitter");

            while (true)
            {
                Console.Write("Votre choix : ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        return;
                    case "2":
                        SaveStats();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Choix invalide, veuillez réessayer.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}