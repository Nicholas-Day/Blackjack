using Blackjack.Enums;
using Blackjack.Models;
using System;
using System.Linq;

namespace Blackjack.Helpers
{
    public static class PlayerIO
    {
        private static bool YesNoDialog(string message)
        {
            Console.Clear();
            var inputIsValid = false;
            Console.Write($"1. Yes\n2. No\n{message}");
            while (!inputIsValid)
            {
                try
                {
                    var response = Console.ReadKey();
                    if (!YesNoInputValue.ValidValues.Contains(response.KeyChar.ToString()))
                    {
                        throw new InvalidInputException();
                    }
                    inputIsValid = true;
                    if (YesNoInputValue.Yes.Contains(response.KeyChar.ToString()))
                    {
                        return true;
                    }
                }
                catch
                {
                }
            }
            return false;
        }
        public static void AskToPlayAgain(Player player)
        {
            if (!WantToKeepPlaying())
            {
                Game.CloseOutPlayer(player);
                return;
            }
            if (player.Bank.Balance == 0)
            {
                player.Bank.Deposit(depositAmount: GetBuyIn("Amount to buy back in for: "));
            }

        }
        public static void TurnOptions(Hand hand)
        {
            var validOptions = Enums.TurnOptions.AllOptions;
            if (!hand.CanSplit)
            {
                validOptions = validOptions.Where(option => option.Value != Enums.TurnOptions.Split.Value).ToList();
            }
            if (hand.Cards.Count > 2)
            {
                validOptions = validOptions.Where(option => option.IsExclusiveToNewHands != true).ToList();
            }
            foreach (var option in validOptions)
            {
                Console.WriteLine($"{option.Value}. {option.DisplayName}");
            }
        }
        public static TurnOptions GetTurnDecision()
        {
            throw new NotImplementedException();
        }
        private static bool WantToKeepPlaying()
        {
            return YesNoDialog("Would you like continue playing: ");
        }
        public static bool GetInsuranceResponse()
        {
            return YesNoDialog("Would you like to place an insurance bet: ");
        }
        public static int GetBuyIn(string message)
        {
            Console.Write(message);
            // TODO: try/catch all int32.parse methods
            var buyInAmount = Int32.Parse(Console.ReadLine());
            return buyInAmount;
        }
        public static string PlayerName(int i)
        {
            Console.Clear();
            Console.Write($"Enter name for player {i + 1}: ");
            var name = Console.ReadLine();
            return name;
        }
        public static int NumberOfPlayers()
        {
            Console.Write("Enter number of players: ");
            var playerCount = Int32.Parse(Console.ReadLine());
            return playerCount;
        }
        public static int HouseBank()
        {
            Console.Clear();
            Console.Write("Enter house bankroll: ");
            var bankroll = Console.Read();
            return bankroll;
        }
    }
}