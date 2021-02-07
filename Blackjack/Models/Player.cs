using Blackjack.Enums;
using Blackjack.Helpers;
using Blackjack.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Player : Participant
    {
        private readonly IPlayerIO _playerIO = new PlayerIO();

        public Player(int amount = 0, string name = "Player")
        {
            Name = name;
            Bank = new Bank(amount);
            Hands = new List<Hand>() { new Hand() };
        }
        public Player(IPlayerIO playerIO, int amount = 0, string name = "Player")
        {
            Name = name;
            Bank = new Bank(amount);
            Hands = new List<Hand>() { new Hand() };
            _playerIO = playerIO;
        }

        public string Name { get; private set; }
        public bool HasInsuranceBet => HasInsurance();
        public int InsuranceBet { get; private set; }

        private bool HasInsurance()
        {
            return InsuranceBet != 0;
        }
        public void PlaceBet(Hand hand, int amount)
        {
            Bank.Withdraw(amount);
            hand.PlaceWager(amount);
        }
        public void PlaceInsuranceBet()
        {
            var insuranceBet = Hands[0].Wager / 2;
            Bank.Withdraw(insuranceBet);
            InsuranceBet = insuranceBet;
        }
        internal void ClearInsuranceBet()
        {
            InsuranceBet = 0;
        }
        internal void ClearAllWagers()
        {
            Hands.ForEach(hand => hand.ClearWager());
        }
        protected async override void PlayHandAsync(Hand hand)
        {
            ITurnDecision decision = null;
            while (!hand.HasPlayed)
            {
                var validOptions = ValidTurnOptions(hand);
                try
                {
                    decision = _playerIO.GetTurnDecision(validOptions);
                }
                catch (Exception ex)
                {
                    var pause = Task.Delay(3000);
                    PlayerIO.InvalidInputTryAgain(ex);
                    await pause;
                }
                if (decision != null) { decision.ExecuteOn(hand); }
            }
        }
        private List<TurnOptions> ValidTurnOptions(Hand hand)
        {
            var validOptions = TurnOptions.AllTurnOptions;
            if (!hand.CanSplit)
            {
                validOptions = validOptions.Where(option => option.Value != TurnOptions.Split.Value).ToList();
            }
            if (hand.Cards.Count > 2)
            {
                validOptions = validOptions.Where(option => option.IsExclusiveToNewHands != true).ToList();
            }
            return validOptions;
        }
    }
}
