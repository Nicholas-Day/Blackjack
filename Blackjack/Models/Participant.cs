using System.Collections.Generic;

namespace Blackjack.Models
{
    public class Participant
    {
        public Bank Bank { get; set; }
        public List<Hand> Hands { get; set; }

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

        internal bool HasNatural()
        {
            if (Hands[0].Value == 21)
            {
                return true;
            }
            return false;
        }
    }
}