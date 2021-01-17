using Blackjack.Enums;
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
            Hands[0].AddCard(Deck.Next());
        }
        public void DrawCard(int hand)
        {
            Hands[hand].AddCard(Deck.Next());
        }
        public void DrawCard(Hand hand)
        {
            hand.AddCard(Deck.Next());
        }
        internal void Discard()
        {
            foreach (var hand in Hands)
            {
                hand.Discard();
            }
        }
        private bool CheckForNatural()
        {
            if (Hands[0].Value == 21)
            {
                return true;
            }
            return false;
        }
        public virtual void TakeTurn()
        {
            foreach (var hand in Hands)
            {
                if (!hand.HasPlayed)
                {
                    PlayerIO.TurnOptions(hand);
                    var decision = PlayerIO.GetTurnDecision();
                    hand.HasPlayed = true; // TODO: this only allows a single decision to be made per turn
                    Decision.Execute(decision);
                    if (decision == TurnOptions.Split) { TakeTurn(); }
                }
            }
        }
    }
}