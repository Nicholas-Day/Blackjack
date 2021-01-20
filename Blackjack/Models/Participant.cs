using Blackjack.Helpers;
using System.Collections.Generic;

namespace Blackjack.Models
{
    public class Participant
    {
        public Bank Bank { get; set; }
        public List<Hand> Hands { get; set; }
        public bool HasNatural { get => CheckForNatural();}

        public void DrawCard()
        {
            Hands[0].AddCard(Deck.NextCard());
        }
        public void DrawCard(int hand)
        {
            Hands[hand].AddCard(Deck.NextCard());
        }
        public void DrawCard(Hand hand)
        {
            hand.AddCard(Deck.NextCard());
        }
        public void Discard()
        {
            foreach (var hand in Hands)
            {
                hand.Discard();
            }
        }
        private bool CheckForNatural()
        {
            if (Hands[0].Value == 21 && Hands[0].Cards.Count == 2)
            {
                return true;
            }
            return false;
        }
        public virtual void TakeTurn()
        {
            foreach (var hand in Hands)
            {
                PlayHand(hand);
            }
        }
        private static void PlayHand(Hand hand)
        {
            while (!hand.HasPlayed)
            {
                PlayerIO.TurnOptions(hand);
                var decision = PlayerIO.GetTurnDecision();
                decision.ExecuteOn(hand);
            }
        }
    }
}