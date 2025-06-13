/* Title: Mastermind Game
* Autor: Thomas Mayoraz
* Last Updated: 12.06.2025
* Description: Ce programme permettra à l'utilisateur de jouer au mastermind seul ou contre un ami 
*              
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mastermind_Mayoraz
{
    internal class Program
    {
        // Déclare une liste statique contenant les couleurs valides représentées par des caractères.
        // 'R' pour rouge, 'B' pour bleu, 'J' pour jaune, 'O' pour orange (ou jaune foncé), 'V' pour violet
        static readonly List<char> validColors = new List<char> { 'R', 'B', 'J', 'O', 'V' };
        // Déclare un dictionnaire statique qui enregistre chaque caractère de couleur valide
        // à une couleur correspondante dans la console (ConsoleColor).
        static readonly Dictionary<char, ConsoleColor> colorMap = new Dictionary<char, ConsoleColor>
        {
            { 'R', ConsoleColor.Red },       // 'r' correspond à la couleur rouge.
            { 'B', ConsoleColor.Blue },      // 'b' correspond à la couleur bleue.
            { 'J', ConsoleColor.Yellow },    // 'j' correspond à la couleur jaune.
            { 'O', ConsoleColor.DarkYellow },// 'o' correspond à la couleur jaune foncé (Orange).
            { 'V', ConsoleColor.Magenta }    // 'v' correspond à la couleur magenta (violet).
        };

        // Déclare une constante entière représentant le nombre maximum de tentatives autorisées.
        // Cette valeur est fixe et ne peut pas être modifiée.
        const int maxAttempts = 10;

        

        // Statistiques globales
        static int gamesWon = 0;
        static int gamesLost = 0;
        static TimeSpan totalTime = TimeSpan.Zero;
        const string statsFileName = "mastermind_stats.txt";
        static void Main(string[] args)
        {
            while (true)
            {
                //Définir la fenêtre de la console à sa taille maximale          
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
                        //PlayChallenge();                    to do 
                        break;
                    case "4":
                        ShowRules();
                        break;
                    case "5":
                        //ShowStats();                        to do 
                        break;
                    case "6":
                        //SaveStats();                        to do
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Appuyez sur une touche pour recommencer.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        /// <summary>
        /// Méthode qui affiche le titre du jeu 
        /// </summary>
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

        /// <summary>
        /// Méthode qui affiche les règles du jeu
        /// </summary>
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
        /// <summary>
        /// Méthode qui permet de jouer tout seul
        /// </summary>
        static void PlaySolo()
        {
            char[] secretCode = GenerateRandomCode();
            RunGame(secretCode);
        }
        /// <summary>
        /// Méthode pour jouer à deux joueurs
        /// </summary>
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
            Console.WriteLine("sans espaces: ");
            Console.WriteLine("Seules les lettres de la liste ci-dessus seront comptabilisées");
            Console.WriteLine("(Les lettres ne s'afficheront pas pour garder le secret)");
            Console.Write("Votre code secret :");
            char[] secretCode = new char[4];
            int i = 0;
            while (i < 4)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                char input = Char.ToUpper(key.KeyChar);
                if (validColors.Contains(input))
                {
                    secretCode[i++] = input;
                    Console.Write("*");
                }
            }

            Console.WriteLine("\nCombinaison enregistrée. Passez au JOUEUR 2 qui devra deviner le code entré.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear();
            RunGame(secretCode);


        }
        /// <summary>
        /// Méthode qui permet de générer le code de 4 couleurs aléatoirement
        /// </summary>
        /// <returns> un tableau en Char de 4 lettres parmis 'R', 'B', 'J', 'O', 'V' </returns>
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
        /// <summary>
        /// Méthode qui lance la partie 
        /// </summary>
        /// <param name="secretCode">tableau du code généré aléatoirement ou par l'utilisateur 2</param>
        static void RunGame(char[] secretCode)
        {
            Stopwatch stopwatch = new Stopwatch(); // Démarre le chrono
            stopwatch.Start();

            List<Tuple<char[], int, int>> history = new List<Tuple<char[], int, int>>();

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                Console.Clear();
                Title();
                Console.WriteLine($"Essai {attempt} / {maxAttempts}");
                ShowHistory(history);
                char[] guess = GetPlayerGuess();

                Tuple<int, int> result = EvaluateGuess(secretCode, guess);
                history.Add(new Tuple<char[], int, int>(guess, result.Item1, result.Item2));

                if (result.Item1 == 4)
                {
                    stopwatch.Stop(); // Arrête le chrono
                    TimeSpan timeTaken = stopwatch.Elapsed;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nFÉLICITATIONS ! Vous avez trouvé la combinaison secrète qui était : ");
                    DisplayColoredCode(secretCode);
                    Console.WriteLine($"Temps écoulé : {timeTaken.Minutes} min {timeTaken.Seconds} sec");
                    Console.ResetColor();
                    EndGame();
                    return;
                }
            }

            stopwatch.Stop(); // Arrêt aussi si le joueur perd
            TimeSpan totalTime = stopwatch.Elapsed;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nDÉSOLÉ, vous avez utilisé vos 10 essais.");
            Console.Write("La combinaison secrète était : ");
            DisplayColoredCode(secretCode);
            Console.WriteLine($"Temps écoulé : {totalTime.Minutes} min {totalTime.Seconds} sec");
            Console.ResetColor();
            EndGame();
        }

        /// <summary>
        /// Méthode qui permet d'afficher l'historique des coups effectués par le joueur
        /// </summary>
        /// <param name="history">historique de jeu</param>
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


        /// <summary>
        /// Méthode qui permet au joueur de rentrer ses valeurs
        /// </summary>
        /// <returns> un tableau contenant l'entrée (valide) de l'utilisateur</returns>
        static char[] GetPlayerGuess()
        {
            while (true)
            {
                Console.Write("Entrez une combinaison (ex: RBJO ou R B J O) : ");
                string input = Console.ReadLine().ToUpper().Replace(" ", "");

                if (input.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("▲ - Vous devez entrer exactement 4 lettres représentant des couleurs.");
                    Console.ResetColor();
                    continue;
                }

                char[] guess = new char[4];
                bool allValid = true;

                for (int i = 0; i < 4; i++)
                {
                    char c = input[i];
                    if (!validColors.Contains(c))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"▲ - '{c}' n'est pas une couleur valide. Utilisez seulement R, B, J, O, V.");
                        Console.ResetColor();
                        allValid = false;
                        break;
                    }
                    guess[i] = c;
                }

                if (allValid)
                    return guess;

                Console.WriteLine("Veuillez réessayer...\n");
            }
        }
        /// <summary>
        /// Méthode qui affiche les initiales des couleurs avec le fond de l'initiale dans la couleur correspondante
        /// </summary>
        /// <param name="code">le tableau en char qui contient les lettres qu'il faut colorer</param>
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
        /// <summary>
        /// Méthode de fin de partie (affiche un texte et retourne au menu quand on clique sur une touche) 
        /// </summary>
        static void EndGame()
        {
            Console.WriteLine("\nAppuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
        }
        /// <summary>
        /// Méthode qui vérifie les entrées de l'utilisateur pour voir si celles-ci correspondent au tableau initial 
        /// </summary>
        /// <param name="secret">tableau initial qui contient les couleurs à deviner aléatoire ou créé par le joueur 2</param>
        /// <param name="guess">tableau qui contient les couleurs entrées par l'utilisateur</param>
        /// <returns> un tuple qui contient le nombre de couleurs bien placées et le nombre de couleurs mal placées mais qui sont dans le code secret</returns>
        static Tuple<int, int> EvaluateGuess(char[] secret, char[] guess)
        {
            int wellPlaced = 0;
            int misplaced = 0;
            bool[] checkedSecret = new bool[4];
            bool[] checkedGuess = new bool[4];

            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == secret[i])
                {
                    wellPlaced++;
                    checkedSecret[i] = true;
                    checkedGuess[i] = true;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (checkedGuess[i]) continue;

                for (int j = 0; j < 4; j++)
                {
                    if (!checkedSecret[j] && guess[i] == secret[j])
                    {
                        misplaced++;
                        checkedSecret[j] = true;
                        break;
                    }
                }
            }

            return Tuple.Create(wellPlaced, misplaced);
        }
    }
}
