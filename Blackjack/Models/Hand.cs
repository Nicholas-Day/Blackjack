using Blackjack.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Models
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int Value => GetValue();
        public int Wager { get; private set; }
        public bool HasPlayed => HasHandBeenPlayed();
        public bool HasAce => Cards.Any(card => card.Rank.Value == Ranks.Ace);
        public bool CanSplit => HasPair();
        public bool IsBust => IsHandBust();
        public bool IsBlackJack => Is21();
        public bool Has5Cards => Cards.Count == 5;
        public bool IsNatural => IsHandNatural();

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
        public void Discard()
        {
            Deck.DiscardPile.AddRange(Cards);
            Cards.RemoveAll(x => x.GetType() == typeof(Card));
        }
        public void PlaceWager(int amount)
        {
            Wager = amount;
        }
        public void ClearWager()
        {
            Wager = 0;
        }
        private int GetValue()
        {
            var value = 0;
            Cards.ForEach(card => value += card.Value);
            if (value > 21 && HasAce)
            {
                var adjustedValue = value - ( 10 * (NumOfAces() - 1) ) > 21 ? value - (10 * NumOfAces())  : value - (10 * (NumOfAces() - 1));
                return adjustedValue;
            }
            return value;
        }
        private int NumOfAces()
        {
            var count = 0;
            foreach (var card in Cards)
            {
                if (card.Rank.Value == Ranks.Ace)
                {
                    count += 1;
                }
            }
            return count;
        }
        private bool IsHandBust()
        {
            return Value > 21;
        }
        private bool IsHandNatural()
        {
            return Cards.Count == 2 && IsBlackJack;
        }
        private bool Is21()
        {
            return Value == 21;
        }
        private bool HasPair()
        {
            if (Cards.Count != 2)
            {
                return false;
            }
            return Cards[0].Value == Cards[1].Value;
        }
        private bool HasHandBeenPlayed()
        {
            return Has5Cards || IsBlackJack || IsBust || IsNatural;
        }
    }
}
