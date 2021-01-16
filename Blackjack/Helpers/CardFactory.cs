using Blackjack.Enums;
using Blackjack.Models;
using System;
using System.Collections.Generic;

namespace Blackjack.Helpers
{
    public static class CardFactory
    {
        public static Random _generator = new Random();

        public static List<Card> GenerateDeckOfCards(int numDecks)
        {
            var numCards = numDecks * 52;
            var cards = new List<Card>();
            for (int i = 0; i < numCards; i++)
            {
                cards.Add(new Card(i));
            }
            return cards;
        }
        public static List<Card> Generate2CardHand()
        {
            // TODO: make sure 2 cards are not the same
            var card1 = CardFactory.GenerateCard();
            var card2 = CardFactory.GenerateCard();
            return new List<Card>() { card1, card2 };
        }
        private static Card GenerateCard()
        {
            var card = new Card(GenerateRankValue(), GenerateSuitValue());
            return card;
        }
        public static Ranks GenerateRankValue()
        {
            var rank = Enumeration.FromValue<Ranks>(_generator.Next(Ranks.Count));
            return rank;
        }
        public static Suits GenerateSuitValue()
        {
            var suit = Enumeration.FromValue<Suits>(_generator.Next(Suits.Count));
            return suit;
        }
    }
}
