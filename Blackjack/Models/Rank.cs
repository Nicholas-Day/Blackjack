using Blackjack.Enums;
using Blackjack.Helpers;

namespace Blackjack.Models
{
    public class Rank
    {
        public Ranks Value { get; set; }

        public Rank()
        {
            Value = CardFactory.GenerateRankValue();
        }
        public Rank(Ranks rank)
        {
            Value = rank;
        }
    }
}
