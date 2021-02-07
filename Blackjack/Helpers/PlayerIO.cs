using Blackjack.Enums;
using Blackjack.Interfaces;
using Blackjack.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Helpers
{
    public class PlayerIO : IPlayerIO
    {
        private static bool YesNoDialog(string message)
        {
            Console.Clear();
            bool inputIsValid = false;
            Console.Write($"1. Yes\n2. No\n{message}");
            while (!inputIsValid)
            {
                try
                {
                    ConsoleKeyInfo response = Console.ReadKey();
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
        public static ITurnDecision GetTurnDecision(List<TurnOptions> validOptions)
        {
            TurnOptions chosenOption = GetDecision(validOptions);
            if (chosenOption == null)
            {
                throw new InvalidInputException();
            }
            return chosenOption.Convert();
        }
        private static TurnOptions GetPlayerDecision(List<TurnOptions> validOptions)
        {
            Console.Clear();
            DisplayOptions(validOptions);
            return GetDecision(validOptions);
        }
        private static TurnOptions GetDecision(List<TurnOptions> validOptions)
        {
            Console.Write("Enter your decision: ");
            Console.ReadLine();
            int decision = Console.Read();
            IEnumerable<int> validDecisions = validOptions.Select(option => option.Value);
            TurnOptions chosenOption = TurnOptions.AllTurnOptions.FirstOrDefault(option => option.Value == decision);
            return chosenOption;
        }
        public static void InvalidInputTryAgain(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        private static void DisplayOptions(List<TurnOptions> validOptions)
        {
            foreach (TurnOptions option in validOptions)
            {
                Console.WriteLine($"{option.Value}. {option.DisplayName}");
            }
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
            // TODO: try/catch all int32.parse methods
            int buyInAmount = 0;
            while (buyInAmount <= 0)
            {
                Console.Write(message);
                try
                {
                    buyInAmount = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    InvalidInputTryAgain(e);
                }
            }
            return buyInAmount;
        }
        public static string PlayerName(int i)
        {
            Console.Clear();
            Console.Write($"Enter name for player {i + 1}: ");
            string name = Console.ReadLine();
            return name;
        }
        public static int NumberOfPlayers()
        {
            Console.Write("Enter number of players: ");
            int playerCount = int.Parse(Console.ReadLine());
            return playerCount;
        }
        public static int HouseBank()
        {
            Console.Clear();
            Console.Write("Enter house bankroll: ");
            int bankroll = 0;
            while (bankroll <= 0)
            {
                try
                {
                    bankroll = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    InvalidInputTryAgain(e);
                }
            }
            return bankroll;
        }
    }
}