using Blackjack.Enums;
using Blackjack.Exceptions;
using Blackjack.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Test
{
    [TestClass]
    public class Constructors
    {
        readonly Card AceOfDiamonds = new Card(Ranks.Ace, Suits.Diamonds);
        readonly Card AceOfSpades = new Card(Ranks.Ace, Suits.Spades);

        [TestMethod]
        public void RankConstructor_RandomRank_RankIsValid()
        {
            var rank = new Rank();

            Assert.IsInstanceOfType(rank.Value, typeof(Ranks));
        }
        [TestMethod]
        public void RankConstructor_RankIsAce_SybmolIsAA()
        {
            var rank = new Rank(Ranks.Ace);

            Assert.AreEqual(Ranks.Ace, rank.Value);
            Assert.AreEqual(Ranks.Ace.DisplayName, rank.Value.DisplayName);
        }
        [TestMethod]
        public void SuitConstructor_RandomSuit_SuitIsValid()
        {
            var suit = new Suit();

            Assert.IsInstanceOfType(suit.Value, typeof(Suits));
        }
        [TestMethod]
        public void SuitConstructor_SuitIsClubs_GetValidSuit()
        {
            var suit = new Suit(Suits.Clubs);

            Assert.AreEqual(Suits.Clubs, suit.Value);
            Assert.AreEqual(Suits.Clubs.Color, suit.Value.Color);
            Assert.AreEqual(Suits.Clubs.DisplayName, suit.Value.DisplayName);
        }
        [TestMethod]
        public void CardConstructor_RandomCard_CardIsValid()
        {
            var card = new Card();

            Assert.IsInstanceOfType(card.Rank, typeof(Rank));
            Assert.IsInstanceOfType(card.Rank.Value, typeof(Ranks));
            Assert.IsInstanceOfType(card.Suit, typeof(Suit));
            Assert.IsInstanceOfType(card.Suit.Value, typeof(Suits));
        }
        [TestMethod]
        public void CardConstructor_AceOfSpades_CorrectCardConstructed()
        {
            var card = new Card(Ranks.Ace, Suits.Spades);

            Assert.IsInstanceOfType(card.Rank, typeof(Rank));
            Assert.IsInstanceOfType(card.Rank.Value, typeof(Ranks));
            Assert.AreEqual(Ranks.Ace, card.Rank.Value);
            Assert.IsInstanceOfType(card.Suit, typeof(Suit));
            Assert.IsInstanceOfType(card.Suit.Value, typeof(Suits));
            Assert.AreEqual(Suits.Spades, card.Suit.Value);
        }
        [TestMethod]
        public void HandConstructor_RandomHand_DealtValidHand()
        {
            var hand = new Hand();

            foreach (var card in hand.Cards)
            {
                Assert.IsInstanceOfType(card, typeof(Card));
                var temp = hand.Cards.Where(x => x != card);
                Assert.IsFalse(temp.Any(x => x == card));
            }
        }
        [TestMethod]
        public void HandConstructor_AceOfDiamondsAceOfSpades_CorrectHandConstructed()
        {
            var hand = new Hand(AceOfDiamonds, AceOfSpades);

            Assert.AreEqual(Ranks.Ace, hand.Cards[0].Rank.Value);
            Assert.AreEqual(Suits.Diamonds, hand.Cards[0].Suit.Value);
            Assert.AreEqual(Ranks.Ace, hand.Cards[1].Rank.Value);
            Assert.AreEqual(Suits.Spades, hand.Cards[1].Suit.Value);
        }
        [TestMethod]
        public void PlayerConstructor_DefaultPlayer()
        {
            var player1 = new Player();

            Assert.AreEqual("Player", player1.Name);
            Assert.AreEqual(0, player1.Bank.Balance);
            Assert.AreEqual(1, player1.Hands.Count);
            Assert.AreEqual(0, player1.Hands[0].Cards.Count);
        }
        [TestMethod]
        public void PlayerConstructor_Bank100_CorrectValues()
        {
            var bob = new Player(100);

            Assert.AreEqual("Player", bob.Name);
            Assert.AreEqual(100, bob.Bank.Balance);
        }
        [TestMethod]
        public void PlayerConstructor_NameBob_CorrectValues()
        {
            var bob = new Player(name: "Bob");

            Assert.AreEqual("Bob", bob.Name);
            Assert.AreEqual(0, bob.Bank.Balance);
        }
        [TestMethod]
        public void PlayerConstructor_NameBobBank100_CorrectValues()
        {
            var bob = new Player(100, "Bob");

            Assert.AreEqual("Bob", bob.Name);
            Assert.AreEqual(100, bob.Bank.Balance);
        }
        [TestMethod]
        public void DealerConstructor()
        {
            var dealer = new Dealer(10);

            Assert.AreEqual(10, dealer.Bank.Balance);
            Assert.AreEqual(1, dealer.Hands.Count);
            Assert.AreEqual(0, dealer.Hands[0].Cards.Count);
        }
        [TestMethod]
        public void DealerConstructor_NegativeBankroll_NegativeBalanceDeclarationExceptionThrown()
        {
            var wasExceptionThrown = false;

            try
            {
                var dealer = new Dealer(-10);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cannot declare negative balance", e.Message);
                wasExceptionThrown = true;
            }
            finally
            {
                Assert.IsTrue(wasExceptionThrown);
            }
        }
        [TestMethod]
        public void BankConstructor_DefaultBank_BalanceIs0()
        {
            var bank = new Bank();

            Assert.AreEqual(0, bank.Balance);
        }
        [TestMethod]
        public void BankConstructor_InitTo50_BalanceIs50()
        {
            var bank = new Bank(50);

            Assert.AreEqual(50, bank.Balance);
        }
        [TestMethod]
        public void BankConstructor_InitToNegative50_ExceptionThrown()
        {
            try
            {
                var bank = new Bank(-50);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cannot declare negative balance", e.Message);
            }
        }
        [TestMethod]
        public void BankConstructor_InitToNegative50_ExceptionTwn()
        {
            Assert.ThrowsException<NegativeBalanceDeclarationException>(() => new Bank(-50));
        }
        [TestMethod]
        public void DeckConstructor_Default_GetValidDeck()
        {
            var checkedCards = new List<Card>();

            foreach (var card in Deck.Cards)
            {
                Assert.IsFalse(checkedCards.Any(x => x == card));
                checkedCards.Add(card);
            }
        }
    }
}
