using Blackjack.Enums;
using Blackjack.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Interfaces
{
    public interface IPlayerIO
    {
        private bool YesNoDialog(string message)
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
        public void AskToPlayAgain(Player player)
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
        public ITurnDecision GetTurnDecision(List<TurnOptions> validOptions)
        {
            var chosenOption = GetPlayerDecision(validOptions);
            if (chosenOption == null)
            {
                throw new InvalidInputException("Invalid input, please try again");
            }
            return chosenOption.Convert();
        }
        private TurnOptions GetPlayerDecision(List<TurnOptions> validOptions)
        {
            Console.Clear();
            DisplayOptions(validOptions);
            return GetDecision(validOptions);
        }
        private TurnOptions GetDecision(List<TurnOptions> validOptions)
        {
            Console.Write("Enter your decision: ");
            Console.ReadLine();
            var decision = int.Parse(Console.ReadLine());
            var validDecisions = validOptions.Select(option => option.Value);
            var chosenOption = TurnOptions.AllTurnOptions.FirstOrDefault(option => option.Value == decision);
            return chosenOption;
        }
        private void DisplayOptions(List<TurnOptions> validOptions)
        {
            foreach (var option in validOptions)
            {
                Console.WriteLine($"{option.Value}. {option.DisplayName}");
            }
        }

        private bool WantToKeepPlaying()
        {
            return YesNoDialog("Would you like continue playing: ");
        }
        public bool GetInsuranceResponse()
        {
            return YesNoDialog("Would you like to place an insurance bet: ");
        }
        public int GetBuyIn(string message)
        {
            Console.Write(message);
            // TODO: try/catch all int32.parse methods
            var buyInAmount = int.Parse(Console.ReadLine());
            return buyInAmount;
        }
        public string PlayerName(int i)
        {
            Console.Clear();
            Console.Write($"Enter name for player {i + 1}: ");
            var name = Console.ReadLine();
            return name;
        }
        public int NumberOfPlayers()
        {
            Console.Write("Enter number of players: ");
            var playerCount = int.Parse(Console.ReadLine());
            return playerCount;
        }
        public int HouseBank()
        {
            Console.Clear();
            Console.Write("Enter house bankroll: ");
            var bankroll = Console.Read();
            return bankroll;
        }
    }
}
