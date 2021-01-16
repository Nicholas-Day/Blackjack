using Blackjack.Helpers;
using Blackjack.Models;
using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            //Game.InitializeParticipants();
            //Game.Start();
            var card = CardFactory.GenerateCard();
            PrintCard(card);
        }

        private static void PrintCard(Card card)
        {
            PlayerIO.TurnOptions();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = card.Suit.Value.Color;
            Console.WriteLine(" ____ ");
            Console.WriteLine("|    |");
            Console.WriteLine($"| {card.Rank.Value.DisplayName} |");
            Console.WriteLine($"|  {card.Suit.Value.DisplayName} |");
            Console.WriteLine("|____|");
        }
    }
}
