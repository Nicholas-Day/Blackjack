using Blackjack.Models;

namespace Blackjack.Interfaces
{
    public interface ITurnDecision
    {
        public abstract void ExecuteOn(Hand hand);
    }
}
