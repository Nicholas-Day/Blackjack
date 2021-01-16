using Blackjack.Enums;
using Blackjack.Exceptions;
using Blackjack.Helpers;
using Blackjack.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Blackjack.Test
{
    [TestClass]
    public class UnitTest1
    {
        Card AceOfDiamonds = new Card(Ranks.Ace, Suits.Diamonds);
        Card AceOfSpades = new Card(Ranks.Ace, Suits.Spades);
        Card FiveOfHearts = new Card(Ranks.Five, Suits.Hearts);
        Card SevenOfClubs = new Card(Ranks.Seven, Suits.Clubs);

        [TestMethod]
        public void GetHandValue_RangeOfHands_CorrectValues()
        {
            var hand1 = new Hand(AceOfDiamonds, AceOfSpades);
            Assert.AreEqual(12, hand1.Value);

            var hand2 = new Hand(FiveOfHearts, SevenOfClubs);
            hand2.Cards.AddRange(hand1.Cards);
            Assert.AreEqual(14, hand2.Value);
        }
        [TestMethod]
        public void BankDeposit_DefaultBankDeposit100_BalanceIs100()
        {
            var bank = new Bank();

            bank.Deposit(100);

            Assert.AreEqual(100, bank.Balance);
        }
        [TestMethod]
        public void BankWithdraw_Withdraw50from100_BalanceIs50()
        {
            var bank = new Bank(100);

            bank.Withdraw(50);

            Assert.AreEqual(50, bank.Balance);
        }
        [TestMethod]
        public void BankWithdraw_Withdraw50From25_Balance25ExceptionThrown()
        {
            var bank = new Bank(25);
            try
            {
                bank.Withdraw(50);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Insufficient Funds", e.Message);
            }
            Assert.AreEqual(25, bank.Balance);
        }
        [TestMethod]
        public void HasFunds_SufficientFunds_ReturnsTrue()
        {
            var bank = new Bank(100);

            Assert.IsTrue(bank.HasFunds(100));
        }
        [TestMethod]
        public void HasFunds_InsufficientFunds_ReturnsFalse()
        {
            var bank = new Bank(50);

            Assert.IsFalse(bank.HasFunds(100));
        }
        [TestMethod]
        public void DrawCard_GetNextCardInTheDeck()
        {
            var nextCard = Deck.Cards[0];
            var player = new Player();

            player.DrawCard();

            Assert.AreEqual(nextCard, player.Hands[0].Cards[0]);
        }
        [TestMethod]
        public void DrawCard_DrawByHandIndex_GetCardToCorrectHand()
        {
            var nextCard = Deck.Cards[0];
            var player = new Player();
            player.Hands.Add(new Hand());

            player.DrawCard(1);

            Assert.AreEqual(nextCard, player.Hands[1].Cards[0]);
        }
        [TestMethod]
        public void DrawCard_DrawByHandInstance_GetCardToCorrectHand()
        {
            var nextCard = Deck.Cards[0];
            var player = new Player();
            player.Hands.Add(new Hand());

            player.DrawCard(player.Hands[1]);

            Assert.AreEqual(nextCard, player.Hands[1].Cards[0]);
        }
        [TestMethod]
        public void PlaceBetOf50_50RemovedFromBank_50AddedToPot()
        {
            var player = new Player(100);

            player.PlaceBet(player.Hands[0], 50);

            Assert.AreEqual(50, player.Bank.Balance);
            Assert.AreEqual(50, player.Hands[0].Wager);
        }
        [TestMethod]
        public void PlaceBetOf50_InsufficientFunds_ExceptionThrown()
        {
            var player = new Player();
            var wasExceptionThrown = false;

            try
            {
                player.PlaceBet(player.Hands[0], 50);
            }
            catch (Exception e)
            {
                wasExceptionThrown = true;
                Assert.IsInstanceOfType(e, typeof(InsufficientFundsException));
            }
            finally
            {
                Assert.IsTrue(wasExceptionThrown);
            }
        }
        [TestMethod]
        [Ignore("need to mock console methods")]
        public void DealStartingCards_()
        {
            Game.InitializeParticipants();
            var numOfCardsDealt = Game.Participants.Count * 2;

            Game.Dealer.DealInitialCards();

            Assert.AreEqual(2, Game.Players.Count);
            Assert.AreEqual(3, Game.Participants.Count);
            Assert.AreEqual(52 - numOfCardsDealt, Deck.Cards.Count);
            foreach (var participant in Game.Participants)
            {
                Assert.AreEqual(1, participant.Hands.Count);
                Assert.AreEqual(2, participant.Hands[0].Cards.Count);
            }
        }
        [TestMethod]
        public void ShuffleDeck_()
        {
            var preShuffleDeck = Deck.Cards;

            Deck.Shuffle();

            Assert.AreNotEqual(preShuffleDeck, Deck.Cards);
        }
        [TestMethod]
        [Ignore("need to mock console methods")]
        public void InitializeParticipants_()
        {
            
            Game.InitializeParticipants();
        }
        [TestMethod]
        public void DiscardHand()
        {
            var hand = new Hand() { Cards = CardFactory.Generate2CardHand() };

            hand.Discard();

            Assert.AreEqual(0, hand.Cards.Count);
        }
    }
}