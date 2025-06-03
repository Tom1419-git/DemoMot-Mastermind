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
        static readonly List<char> validColors = new List<char> { 'R', 'B', 'J', 'O', 'V' };
        static readonly Dictionary<char, ConsoleColor> colorMap = new Dictionary<char, ConsoleColor>
        {
            { 'R', ConsoleColor.Red },
            { 'B', ConsoleColor.Blue },
            { 'J', ConsoleColor.Yellow },
            { 'O', ConsoleColor.DarkYellow},
            { 'V', ConsoleColor.Magenta }
        };
        const int MaxAttempts = 10;
        static void Main(string[] args)
        {

        }
    }
}
