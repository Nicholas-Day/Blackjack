using Blackjack.Enums;

namespace Blackjack.Models
{
    public class Card
    {
        public Card()
        {
            Rank = new Rank();
            Suit = new Suit();
        }
        public Card(Ranks rank, Suits suit)
        {
            Rank = new Rank(rank);
            Suit = new Suit(suit);
        }
        public Card(int index)
        {
            Rank = new Rank(Enumeration.FromValue<Ranks>(index % Ranks.Count));
            Suit = new Suit(Enumeration.FromValue<Suits>(index % Suits.Count));
        }

        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }
        public int Value => Rank.Value.NumericValue;
    }
}