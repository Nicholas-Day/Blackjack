using System;
using System.Text;

namespace Blackjack.Helpers
{
    public static class Table
    {
        public static void Display()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;

            var consoleWindowWidth = Game.Players.Count * 20 + 20;

            var tableSB = new StringBuilder();

            tableSB.Append(HorizontalBorder(consoleWindowWidth));
            tableSB.Append(DealerDetails(consoleWindowWidth));
            //tableSB.Append(PlayerDetails(consoleWindowWidth));
            tableSB.Append(HorizontalBorder(consoleWindowWidth));

            Console.Write(tableSB);

            Console.ReadKey();
        }

        private static StringBuilder DealerDetails(int consoleWindowWidth)
        {
            var detailsSB = new StringBuilder();

            detailsSB.Append(HouseBank(consoleWindowWidth));
            detailsSB.Append(DealerCards(consoleWindowWidth));

            return detailsSB;
        }

        private static StringBuilder DealerCards(int consoleWindowWidth)
        {
            var cards = new StringBuilder();
            var upCard = CardFactory.GenerateCard(); //Game.Dealer.Hand.Cards[1];
            var currentLine = new StringBuilder();
            cards.Append(Line(consoleWindowWidth, " ____   ____ "));
            cards.Append(Line(consoleWindowWidth, "|    | |    |"));
            cards.Append(Line(consoleWindowWidth, $"| \\/ | | {upCard.Rank.Value.DisplayName} |"));
            cards.Append(Line(consoleWindowWidth, $"| /\\ | |  {upCard.Suit.Value.DisplayName} |"));
            cards.Append(Line(consoleWindowWidth, "|____| |____|"));
            return cards;
        }

        private static StringBuilder Line(int consoleWindowWidth, string line)
        {
            var currentLine = new StringBuilder();
            currentLine.Append("|");
            for (var j = 0; j < consoleWindowWidth - 13; j++)
            {
                currentLine.Append(" ");
            }
            currentLine.AppendLine("|");
            currentLine.Insert((consoleWindowWidth - 13) / 2, line);
            return currentLine;
        }

        private static StringBuilder HouseBank(int consoleWindowWidth)
        {
            var currentLine = new StringBuilder();
            currentLine.Append("|");
            var houseBank = $"House Bank: {Game.Dealer.Bank.Balance}";
            for (var i = 0; i < consoleWindowWidth - houseBank.Length; i++)
            {
                currentLine.Append(" ");

            }
            currentLine.Insert((consoleWindowWidth - houseBank.Length) / 2, houseBank);
            currentLine.AppendLine("|");
            return currentLine;
        }

        private static StringBuilder HorizontalBorder(int consoleWindowWidth)
        {
            var currentLine = new StringBuilder();
            currentLine.Append("+");
            for (var i = 0; i < consoleWindowWidth; i++)
            {
                currentLine.Append("-");
            }
            currentLine.AppendLine("+");
            return currentLine;
        }
    }
}