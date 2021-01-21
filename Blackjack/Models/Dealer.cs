using Blackjack.Enums;
using Blackjack.TurnDecisions;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Models
{
    public class Dealer : Participant
    {
        public const int _insurancePayoutRate = 2;
        public const int _drawPayoutRate = 1;
        public const int _normalPayoutRate = 2;

        public Dealer()
        {
            Bank = new Bank(0);
            Hands = new List<Hand>() { new Hand() };
        }
        public Dealer(int houseBank)
        {
            Bank = new Bank(houseBank);
            Hands = new List<Hand>() { new Hand() };
        }

        public Hand Hand => Hands[0];
        public bool HasSoft17 => Hands.Any(hand => hand.HasAce && hand.Value == 17);
        public bool HasAceShowing => AceShowing();

        public void DealInitialCards()
        {
            for (var i = 0; i < 2; i++)
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
        public void SettleInsuranceBets()
        {
            foreach (var player in Game.Players.Where(player => player.HasInsuranceBet))
            {
                if (HasNatural)
                {
                    PayoutInsurance(player);
                }
                else
                {
                    CollectInsurance(player);
                }
            }
        }
        public void CollectInsurance(Player player)
        {
            Bank.Deposit(player.InsuranceBet);
            player.ClearInsuranceBet();
        }
        public void PayoutInsurance(Player player)
        {
            Bank.Withdraw(player.InsuranceBet);
            player.Bank.Deposit(player.InsuranceBet * _insurancePayoutRate);
            player.ClearInsuranceBet();
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
                player.ClearAllWagers();
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
                    hand.ClearWager();
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
                Bank.Withdraw(hand.Wager);
            }
            else if (hand.Value > Hand.Value)
            {
                player.Bank.Deposit(hand.Wager * _normalPayoutRate);
                Bank.Withdraw(hand.Wager);
            }
            else if (hand.Value == Hand.Value)
            {
                player.Bank.Deposit(hand.Wager * _drawPayoutRate);
            }
            else
            {
                Bank.Deposit(hand.Wager);
            }
            hand.ClearWager();
        }
        public override void TakeTurn()
        {
            PlayHand(Hand);
        }
        protected override void PlayHand(Hand hand)
        {
            var hit = new Hit();
            if (HasSoft17)
            {
                hit.ExecuteOn(Hand);
            }
            while (Hand.Value < 17)
            {
                hit.ExecuteOn(Hand);
            }
        }
    }
}
