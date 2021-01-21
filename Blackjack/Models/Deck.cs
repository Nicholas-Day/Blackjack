using Blackjack.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Models
{
    public static class Deck
    {
        private const int _numOfDecks = 6;
        private static readonly Random _random = new Random();

        public static List<Card> Cards { get; private set; } = CardFactory.GenerateDeckOfCards(_numOfDecks);
        public static List<Card> DiscardPile { get; set; } = new List<Card>();

        public static Card NextCard()
        {
            var nextCard = Cards[0];
            Cards.Remove(nextCard);
            return nextCard;
        }
        public static bool CardsAreLow()
        {
            return Cards.Count < 40;
        }
        public static void Shuffle()
        {
            Cards = Cards.OrderBy(x => _random.Next()).ToList();
        }
    }
}
