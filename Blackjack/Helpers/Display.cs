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
                player.Bank.Deposit(GetBuyIn("Amount to buy back in for: "));
            }

        }
        private static bool WantToKeepPlaying()
        {
            Console.Write("1. Yes\n2. No\nWould you like continue playing: ");
            var response = Console.ReadLine();
            if (!YesNoInputValue.ValidValues.Contains(response))
            {
                throw new InvalidInputException();
            }
            if (YesNoInputValue.Yes.Contains(response))
            {
                return true;
            }
            return false;
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