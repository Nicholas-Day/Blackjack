using Blackjack.Enums;
using Blackjack.Models;
using System;
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
        public void TurnOptions(Hand hand)
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
        public ITurnDecision GetTurnDecision()
        {
            throw new NotImplementedException();
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
