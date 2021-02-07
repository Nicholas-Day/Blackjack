using Blackjack.Helpers;
using System;

namespace Blackjack
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowWidth = Console.BufferWidth;
            Console.Title = "Blackjack";
            Game.InitializeParticipants();
            Table.Display();
            //Game.Start();
        }
    }
}
