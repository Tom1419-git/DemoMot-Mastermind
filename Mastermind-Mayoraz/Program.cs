/* Title: Mastermind Game
* Autor: Thomas Mayoraz
* Last Updated: 06.06.2025
* Description: Ce programme permettra à l'utilisateur de jouer au mastermind seul ou contre un ami 
*              
*/
using System;
using System.Collections.Generic;

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
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Title();
                Console.WriteLine("Choisissez un mode :");
                Console.WriteLine("1 - Mode Solo");
                Console.WriteLine("2 - Mode Deux Joueurs");
                Console.WriteLine("3 - Règles du jeu");
                Console.WriteLine("4 - Quitter");
                Console.Write("Votre choix : ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PlaySolo();
                        break;
                    case "2":
                        //PlayTwoPlayers();
                        break;
                    case "3":
                        ShowRules();
                        break;
                    case "4":
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
            Console.WriteLine("\t\t\t\t\t╔════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t\t║         JEU MASTERMIND CONSOLE         ║");
            Console.WriteLine("\t\t\t\t\t║               CREE PAR                 ║");
            Console.WriteLine("\t\t\t\t\t║            Thomas Mayoraz              ║");
            Console.WriteLine("\t\t\t\t\t║                MIN1B                   ║");
            Console.WriteLine("\t\t\t\t\t║          PROJET DEMO-MOT ETML          ║");
            Console.WriteLine("\t\t\t\t\t╚════════════════════════════════════════╝");
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
            Console.WriteLine("\t\tAprès chaque essai, vous verrez sur votre écran :");
            Console.WriteLine("\tNombre de pions bien placés (Bonne couleur et bonne position) ");
            Console.WriteLine("\tNombre de pions mal placés (Bonne couleur mais mauvaise position) ");
            Console.WriteLine("\t\tIl y a deux rôles principaux dans une partie de Mastermind à deux joueurs :");
            Console.WriteLine("\tCodeur: Cette personne crée un code secret composé de quatre pions couleur, choisis parmi");
            Console.WriteLine("\t        les couleurs possibles. Le code est placé secrètement.");
            Console.WriteLine("\tDécodeur: Cette personne tente de découvrir le code en proposant différentes combinaisons");
            Console.WriteLine("\t          de pions et de couleurs à chaque tour. Il dispose d'un maximum de dix tours pour y parvenir.");
            Console.WriteLine("\tSi vous préférez jouer seul, l'ordinateur se chargera de créer la suite de couleurs à deviner.");
            Console.WriteLine();
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
        /// Méthode qui lance la game 
        /// </summary>
        /// <param name="secretCode">tableau du code généré aléatoirement ou par l'utilisateur 2</param>
        static void RunGame(char[] secretCode)
        {
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nFÉLICITATIONS ! Vous avez trouvé la combinaison qui était !");
                    DisplayColoredCode(secretCode);
                    Console.ResetColor();
                    EndGame();
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nDÉSOLÉ, vous avez utilisé vos 10 essais.");
            Console.Write("La combinaison était : ");
            DisplayColoredCode(secretCode);
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

            Console.WriteLine("Historique des essais :");
            foreach (var entry in history)
            {
                DisplayColoredCode(entry.Item1);
                Console.WriteLine($" => Bien placés : {entry.Item2}, Mal placés : {entry.Item3}");
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
                Console.Write("Entrez une combinaison (ex: R B J O) : ");
                string input = Console.ReadLine().ToUpper();
                string[] parts = input.Split(' ');

                if (parts.Length != 4)
                {
                    Console.WriteLine("Veuillez entrer exactement 4 lettres de la liste en couleur ci-dessus séparées par des espaces.");
                    continue;
                }

                char[] guess = new char[4];
                bool valid = true;

                for (int i = 0; i < 4; i++)
                {
                    if (parts[i].Length == 1 && validColors.Contains(parts[i][0]))
                    {
                        guess[i] = parts[i][0];
                    }
                    else
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid) return guess;

                Console.WriteLine("Entrée invalide. Utilisez uniquement R, B, J, O, V.");
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
        /// <param name="secret">tableau initial qui contient les couleurs à deviner</param>
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
