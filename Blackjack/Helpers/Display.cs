using Blackjack.Enums;
using Blackjack.Models;
using System;

namespace Blackjack.Helpers
{
    public static class Display
    {
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
        private static bool WantToKeepPlaying()
        {
            var inputIsValid = false;
            Console.Write("1. Yes\n2. No\nWould you like continue playing: ");
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

        internal static object GetTurnDecision()
        {
            throw new NotImplementedException();
        }

        internal static void TurnOptions()
        {
            throw new NotImplementedException();
        }

        internal static int GetBuyIn(string message)
        {
            Console.Write(message);
            // TODO: try/catch all int32.parse methods
            var buyInAmount = Int32.Parse(Console.ReadLine());
            return buyInAmount;
        }
    }
}