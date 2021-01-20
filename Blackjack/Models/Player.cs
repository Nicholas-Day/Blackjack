using Blackjack.Helpers;
using Blackjack.Interfaces;
using System.Collections.Generic;

namespace Blackjack.Models
{
    public class Player : Participant
    {
        private readonly IPlayerIO _playerIO = new PlayerIO();

        public string Name { get; set; }
        public bool HasInsuranceBet => HasInsurance();
        public int InsuranceBet { get; private set; }

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

        private bool HasInsurance()
        {
            if (InsuranceBet != 0)
            {
                return true;
            }
            return false;
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
        protected override void PlayHand(Hand hand)
        {
            while (!hand.HasPlayed)
            {
                _playerIO.TurnOptions(hand);
                var decision = _playerIO.GetTurnDecision();
                decision.ExecuteOn(hand);
            }
        }
    }
}
