using Blackjack.Enums;
using Blackjack.Helpers;

namespace Blackjack.Models
{
    public class Rank
    {
        public Rank() { Value = CardFactory.GenerateRankValue(); }
        public Rank(Ranks rank) { Value = rank; }

        public Ranks Value { get; set; }
    }
}
