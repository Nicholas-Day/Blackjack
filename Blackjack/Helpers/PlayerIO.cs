using Blackjack.Enums;
using Blackjack.Models;
using System;

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
        internal static void AskToPlayAgain(Player player)
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
        internal static void TurnOptions()
        {
            foreach (var option in Enums.TurnOptions.AllOptions)
            {
                Console.WriteLine($"{option.Value}. {option.DisplayName}");
            }
        }
        internal static TurnOptions GetTurnDecision()
        {
            throw new NotImplementedException();
        }
        private static bool WantToKeepPlaying()
        {
            return YesNoDialog("Would you like continue playing: ");
        }
        internal static bool GetInsuranceResponse()
        {
            return YesNoDialog("Would you like to place an insurance bet: ");
        }
        internal static int GetBuyIn(string message)
        {
            Console.Write(message);
            // TODO: try/catch all int32.parse methods
            var buyInAmount = Int32.Parse(Console.ReadLine());
            return buyInAmount;
        }
        internal static string PlayerName(int i)
        {
            Console.Clear();
            Console.Write($"Enter name for player {i + 1}: ");
            var name = Console.ReadLine();
            return name;
        }
        internal static int NumberOfPlayers()
        {
            Console.Write("Enter number of players: ");
            var playerCount = Int32.Parse(Console.ReadLine());
            return playerCount;
        }
        internal static int HouseBank()
        {
            Console.Clear();
            Console.Write("Enter house bankroll: ");
            var bankroll = Console.Read();
            return bankroll;
        }
    }
}