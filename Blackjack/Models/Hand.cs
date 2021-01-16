using Blackjack.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Models
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int Wager { get; set; }
        public int Value { get => GetValue(); }
        public bool HasPlayed { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
        }
        public Hand(Card card1, Card card2)
        {
            Cards = new List<Card>() { card1, card2 };
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
        private int GetValue()
        {
            var value = 0;
            foreach (var card in Cards)
            {
                value = value + card.Value;
            }
            if (value > 21 && HasAce())
            {
                var adjustedValue = value - ( 10 * (NumOfAces() - 1) ) > 21 ? value - (10 * NumOfAces())  : value - (10 * (NumOfAces() - 1));
                return adjustedValue;
            }
            return value;
        }
        private int NumOfAces()
        {
            int count = 0;
            foreach (var card in Cards)
            {
                if (card.Rank.Value == Ranks.Ace)
                {
                    count += 1;
                }
            }
            return count;
        }
        private bool HasAce()
        {
            return Cards.Any(x => x.Rank.Value == Ranks.Ace);
        }
    }
}
