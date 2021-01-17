using Blackjack.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Models
{
    public class Dealer : Participant
    {
        public const int _insurancePayoutRate = 2;
        public const int _drawPayoutRate = 1;
        public const int _normalPayoutRate = 2; 

        public Hand Hand { get => Hands[0]; }
        public bool HasSoft17 { get => Hands.Any(hand => hand.HasAce && hand.Value == 17); }
        public bool HasAceShowing { get => AceShowing();}

        public void Initialize(int houseBank)
        {
            Bank = new Bank(houseBank);
            Hands = new List<Hand>() { new Hand() };
        }
        public void DealInitialCards()
        {
            for (int i = 0; i < 2; i++)
            {
                Game.Participants.ForEach(x => x.DrawCard());
            }
        }
        private bool AceShowing()
        {
            if (Hand.Cards[0].Rank.Value == Ranks.Ace)
            {
                return true;
            }
            return false;
        }
        public void CollectInsurance(Player player)
        {
            Bank.Deposit(player.InsuranceBet);
        }
        public void PayoutInsurance(Player player)
        {
            Bank.Withdraw(player.InsuranceBet * _insurancePayoutRate);
            player.Bank.Deposit(player.InsuranceBet * _insurancePayoutRate);
        }
        public void SettleInCaseOfDealerNatural()
        {
            foreach (var player in Game.Players)
            {
                if (player.HasNatural)
                {
                    player.Bank.Deposit(player.Hands[0].Wager);
                }
                else
                {
                    Bank.Deposit(player.Hands[0].Wager);
                }
            }
        }
        public override void TakeTurn()
        {
            if (HasSoft17)
            {
                Decision.Execute(TurnOptions.Hit);
            }
            while (Hand.Value < 17)
            {
                Decision.Execute(TurnOptions.Hit);
            }
        }
        public void SettleBets()
        {
            foreach (var player in Game.Players)
            {
                SettleAllHands(player);
            }
        }
        private void SettleAllHands(Player player)
        {
            foreach (var hand in player.Hands)
            {
                if (hand.IsBust)
                {
                    Bank.Deposit(hand.Wager);
                }
                else
                {
                    SettleHand(player, hand);
                }
            }
        }
        private void SettleHand(Player player, Hand hand)
        {
            if (Hand.IsBust)
            {
                player.Bank.Deposit(hand.Wager * _normalPayoutRate);
            }
            else if (hand.Value == Hand.Value)
            {
                player.Bank.Deposit(hand.Wager * _drawPayoutRate);
            }
            else if (hand.Value > Hand.Value)
            {
                player.Bank.Deposit(hand.Wager * _normalPayoutRate);
            }
            else
            {
                Bank.Deposit(hand.Wager);
            }
        }
    }
}
