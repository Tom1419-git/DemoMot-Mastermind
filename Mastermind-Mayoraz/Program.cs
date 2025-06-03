/* Title: Mastermind Game
* Autor: Thomas Mayoraz
* Last Updated: 03.06.2025
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
            { 'r', ConsoleColor.Red },       // 'r' correspond à la couleur rouge.
            { 'b', ConsoleColor.Blue },      // 'b' correspond à la couleur bleue.
            { 'j', ConsoleColor.Yellow },    // 'j' correspond à la couleur jaune.
            { 'o', ConsoleColor.DarkYellow },// 'o' correspond à la couleur jaune foncé (Orange).
            { 'v', ConsoleColor.Magenta }    // 'v' correspond à la couleur magenta (violet).
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
                        //PlaySolo();
                        break;
                    case "2":
                        //PlayTwoPlayers();
                        break;
                    case "3":
                        //ShowRules();
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

        static void Title()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t\t╔════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t\t║         JEU MASTERMIND CONSOLE         ║");
            Console.WriteLine("\t\t\t\t\t║               CREE PAR                 ║");
            Console.WriteLine("\t\t\t\t\t║            Thomas Mayoraz              ║");
            Console.WriteLine("\t\t\t\t\t║          PROJET DEMO-MOT ETML          ║");
            Console.WriteLine("\t\t\t\t\t╚════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
