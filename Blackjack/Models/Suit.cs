using Blackjack.Enums;
using Blackjack.Helpers;

namespace Blackjack.Models
{
    public class Suit
    {
        // TODO: replace with custom enum Suit
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