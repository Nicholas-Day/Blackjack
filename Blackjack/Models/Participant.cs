using System.Collections.Generic;

namespace Blackjack.Models
{
    public abstract class Participant
    {
        public Bank Bank { get; set; }
        public List<Hand> Hands { get; set; }
        public bool HasNatural => CheckForNatural();

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
            Hands.ForEach(hand => hand.Discard());
        }
        private bool CheckForNatural()
        {
            return (Hands[0].Value == 21 && Hands[0].Cards.Count == 2);
        }
        public virtual void TakeTurn()
        {
            Hands.ForEach(hand => PlayHand(hand));
        }
        protected abstract void PlayHand(Hand hand);
    }
}