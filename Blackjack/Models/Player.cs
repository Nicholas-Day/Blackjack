using System.Collections.Generic;

namespace Blackjack.Models
{
    public class Player : Participant
    {
        public string Name { get; set; }

        public Player()
        {
            Name = "Player";
            Bank = new Bank();
            Hands = new List<Hand>() { new Hand() };
        }
        public Player(int amount = 0)
        {
            Name = "Player";
            Bank = new Bank(amount);
            Hands = new List<Hand>(){ new Hand() };
        }
        public Player(string name = "Player")
        {
            Name = name;
            Bank = new Bank();
            Hands = new List<Hand>() { new Hand() };
        }
        public Player(string name, int amount)
        {
            Name = name;
            Bank = new Bank(amount);
            Hands = new List<Hand>() { new Hand() };
        }

        public void PlaceBet(Hand hand, int amount)
        {
            Bank.Withdraw(amount);
            hand.Wager = amount;
        }
    }
}
