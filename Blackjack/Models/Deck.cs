using Blackjack.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Models
{
    public static class Deck
    {
        private const int _numOfDecks = 4;
        private static Random _random = new Random();
        public static List<Card> Cards { get; private set; } = CardFactory.GenerateDeckOfCards(_numOfDecks);
        public static List<Card> DiscardPile { get; set; } = new List<Card>();

        public static Card Next()
        {
            var nextCard = Cards[0];
            Cards.Remove(nextCard);
            return nextCard;
        }
        public static bool CardsAreLow()
        {
            if (Cards.Count < 40)
            {
                return true;
            }
            return false;
        }
        public static void Shuffle()
        {
            Cards = Cards.OrderBy(x => _random.Next()).ToList();
        }
    }
}
