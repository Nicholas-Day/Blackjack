using Blackjack.Enums;

namespace Blackjack.Models
{
    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public int Value { get => Rank.Value.NumericValue; }

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
        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }
        public Card(int index)
        {
            Rank = new Rank(Enumeration.FromValue<Ranks>(index%Ranks.Count));
            Suit = new Suit(Enumeration.FromValue<Suits>(index%Suits.Count));
        }
    }
}