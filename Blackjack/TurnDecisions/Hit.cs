using Blackjack.Interfaces;
using Blackjack.Models;

namespace Blackjack.TurnDecisions
{
    public class Hit : ITurnDecision
    {
        public void ExecuteOn(Hand hand)
        {
            hand.AddCard(Deck.NextCard());
        }
    }
}
