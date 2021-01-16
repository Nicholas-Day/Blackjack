using Blackjack.Enums;
using Blackjack.Helpers;

namespace Blackjack.Models
{
    // TODO: replace class with custom enum Suit
    public class Suit
    {
        public Suits Value { get; set; }

        public Suit()
        {
            Value = CardFactory.GenerateSuitValue();
        }
        public Suit(Suits suit)
        {
            Value = suit;
        }
    }
}