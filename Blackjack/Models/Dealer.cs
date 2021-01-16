using System.Collections.Generic;

namespace Blackjack.Models
{
    public class Dealer : Participant
    {
        public void Initialize(int houseBank)
        {
            Bank = new Bank(houseBank);
            Hands = new List<Hand>() { new Hand() };
        }
        public void DealInitialCards()
        {
            for (int i = 0; i < 2; i++)
            {
                Game.Participants.ForEach(x => x.DrawCard());
            }
        }
    }
}
