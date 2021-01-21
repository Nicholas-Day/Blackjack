using Blackjack.Enums;
using Blackjack.Exceptions;
using Blackjack.Helpers;
using Blackjack.Interfaces;
using Blackjack.Models;
using Blackjack.TurnDecisions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Blackjack.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Card _aceOfDiamonds = new Card(Ranks.Ace, Suits.Diamonds);
        private readonly Card _aceOfSpades = new Card(Ranks.Ace, Suits.Spades);
        private readonly Card _kingOfSpades = new Card(Ranks.King, Suits.Spades);
        private readonly Card _fiveOfHearts = new Card(Ranks.Five, Suits.Hearts);
        private readonly Card _sevenOfClubs = new Card(Ranks.Seven, Suits.Clubs);
        private readonly Card _sixOfClubs = new Card(Ranks.Six, Suits.Clubs);

        [TestMethod]
        public void GetHandValue_RangeOfHands_CorrectValues()
        {
            var hand1 = new Hand(_aceOfDiamonds, _aceOfSpades);
            Assert.AreEqual(12, hand1.Value);

            var hand2 = new Hand(_fiveOfHearts, _sevenOfClubs);
            hand2.Cards.AddRange(hand1.Cards);
            Assert.AreEqual(14, hand2.Value);
        }

        [TestMethod]
        public void BankDeposit_Deposit100_BalanceIs100()
        {
            var bank = new Bank();

            bank.Deposit(100);

            Assert.AreEqual(100, bank.Balance);
        }

        [TestMethod]
        public void BankDeposit_DepositNegativeAmount_ExceptionThrown()
        {
            var bank = new Bank();
            var wasExceptionThrown = false;

            try
            {
                bank.Deposit(-100);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cannot deposit a negative amount", e.Message);
                wasExceptionThrown = true;
            }
            finally
            {
                Assert.IsTrue(wasExceptionThrown);
            }
            Assert.AreEqual(0, bank.Balance);
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
        public void BankWithdraw_WithdrawNegativeAmount_NegativeWithdrawExceptionThrown()
        {
            var bank = new Bank(25);
            var wasExceptionThrown = false;

            try
            {
                bank.Withdraw(-50);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cannot withdraw a negative amount", e.Message);
                wasExceptionThrown = true;
            }
            finally
            {
                Assert.IsTrue(wasExceptionThrown);
            }
        }

        [TestMethod]
        public void HasFunds_SufficientFunds_ReturnsTrue()
        {
            var bank = new Bank(100);

            Assert.IsTrue(bank.HasEnoughFunds(100));
        }

        [TestMethod]
        public void HasFunds_InsufficientFunds_ReturnsFalse()
        {
            var bank = new Bank(50);

            Assert.IsFalse(bank.HasEnoughFunds(100));
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

        [TestMethod]
        public void DealInitialCards()
        {
            var dealer = new Dealer();
            var player = new Player();
            Game.Participants.Add(dealer);
            Game.Participants.Add(player);

            dealer.DealInitialCards();

            Assert.AreEqual(1, player.Hands.Count);
            Assert.AreEqual(2, player.Hands[0].Cards.Count);
            Assert.AreEqual(1, dealer.Hands.Count);
            Assert.AreEqual(2, dealer.Hands[0].Cards.Count);
        }

        [TestMethod]
        public void HasAceShowing_IsTrue_ReturnsTrue()
        {
            var dealer = new Dealer(1000);
            dealer.Hand.AddCard(_aceOfDiamonds);

            Assert.IsTrue(dealer.HasAceShowing);
        }

        [TestMethod]
        public void HasAceShowing_IsFalse_ReturnsFalse()
        {
            var dealer = new Dealer(1000);
            dealer.Hand.AddCard(_fiveOfHearts);
            dealer.Hand.AddCard(_aceOfDiamonds);

            Assert.IsFalse(dealer.HasAceShowing);
        }

        [TestMethod]
        public void CollectInsurance_PlayerHasInsurance_PlayerCollectsInsurance()
        {
            var player = new Player(100);
            var dealer = new Dealer();
            player.PlaceBet(player.Hands[0], 40);
            player.PlaceInsuranceBet();

            dealer.CollectInsurance(player);

            Assert.AreEqual(0, player.InsuranceBet);
            Assert.AreEqual(20, dealer.Bank.Balance);
        }

        [TestMethod]
        public void CollectInsurance_PlayerDoesNotHaveInsurance_DoesNotCollectInsurance()
        {
            var player = new Player(100);
            var dealer = new Dealer();

            dealer.CollectInsurance(player);

            Assert.AreEqual(0, player.InsuranceBet);
            Assert.AreEqual(100, player.Bank.Balance);
            Assert.AreEqual(0, dealer.Bank.Balance);
        }

        [TestMethod]
        public void PayoutInsurance_HasInsuranceBet_CollectsPayout()
        {
            var player = new Player(100);
            var dealer = new Dealer(20);
            var expectedPlayerBalance = player.Bank.Balance;
            player.PlaceBet(player.Hands[0], 40);
            expectedPlayerBalance -= player.Hands[0].Wager;
            player.PlaceInsuranceBet();
            expectedPlayerBalance -= player.InsuranceBet;

            expectedPlayerBalance += player.InsuranceBet * 2;
            dealer.PayoutInsurance(player);

            Assert.AreEqual(0, player.InsuranceBet);
            Assert.AreEqual(expectedPlayerBalance, player.Bank.Balance);
            Assert.AreEqual(0, dealer.Bank.Balance);
        }

        [TestMethod]
        public void PayoutInsurance_NoInsuranceBet_NoPayout()
        {
            var player = new Player(100);
            var dealer = new Dealer(20);
            var expectedPlayerBalance = player.Bank.Balance;

            expectedPlayerBalance += player.InsuranceBet * 2;
            dealer.PayoutInsurance(player);

            Assert.AreEqual(0, player.InsuranceBet);
            Assert.AreEqual(expectedPlayerBalance, player.Bank.Balance);
            Assert.AreEqual(20, dealer.Bank.Balance);
        }

        [TestMethod]
        public void SettleInsuranceBet_PlayerHasInsuranceBet_CollectsPayout()
        {
            var player = new Player(100);
            Game.Players.Add(player);
            var dealer = new Dealer(20);
            dealer.Hand.AddCard(_aceOfDiamonds);
            dealer.Hand.AddCard(_kingOfSpades);
            var expectedPlayerBalance = player.Bank.Balance;
            player.PlaceBet(player.Hands[0], 40);
            expectedPlayerBalance -= player.Hands[0].Wager;
            player.PlaceInsuranceBet();
            expectedPlayerBalance -= player.InsuranceBet;

            expectedPlayerBalance += player.InsuranceBet * 2;
            dealer.SettleInsuranceBets();

            Assert.AreEqual(0, player.InsuranceBet);
            Assert.AreEqual(expectedPlayerBalance, player.Bank.Balance);
            Assert.AreEqual(0, dealer.Bank.Balance);
        }

        [TestMethod]
        public void SettleInsuranceBet_PlayerHasInsuranceBet_DealerCollectsBet()
        {
            var player = new Player(100);
            Game.Players.Add(player);
            var dealer = new Dealer(20);
            var expectedPlayerBalance = player.Bank.Balance;
            player.PlaceBet(player.Hands[0], 40);
            expectedPlayerBalance -= player.Hands[0].Wager;
            player.PlaceInsuranceBet();
            expectedPlayerBalance -= player.InsuranceBet;

            dealer.SettleInsuranceBets();

            Assert.AreEqual(0, player.InsuranceBet);
            Assert.AreEqual(expectedPlayerBalance, player.Bank.Balance);
            Assert.AreEqual(40, dealer.Bank.Balance);
        }

        [TestMethod]
        public void SettleInCaseOfDealerNatural_PlayerDoesNotHaveNatural_DealerColletsPlayerBet()
        {
            var player = new Player(100);
            Game.Players.Add(player);
            var dealer = new Dealer(100);
            player.PlaceBet(player.Hands[0], 40);

            dealer.SettleInCaseOfDealerNatural();

            Assert.AreEqual(60, player.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
            Assert.AreEqual(140, dealer.Bank.Balance);
        }

        [TestMethod]
        public void SettleInCaseOfDealerNatural_PlayerHasNatural_PlayerCollectsPayout()
        {
            var player = new Player(100);
            Game.Players.Add(player);
            var dealer = new Dealer(100);
            player.PlaceBet(player.Hands[0], 40);
            player.Hands[0].AddCard(_aceOfDiamonds);
            player.Hands[0].AddCard(_kingOfSpades);

            dealer.SettleInCaseOfDealerNatural();

            Assert.AreEqual(100, player.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
            Assert.AreEqual(100, dealer.Bank.Balance);
        }

        [TestMethod]
        public void SettleInCaseOfDealerNatural_Player1HasNatural_Player2DoesNot_CorrectPayout()
        {
            var player1 = new Player(100);
            var player2 = new Player(100);
            Game.Players.AddRange(new List<Player>() { player1, player2 });
            var dealer = new Dealer(100);
            player1.PlaceBet(player1.Hands[0], 40);
            player2.PlaceBet(player2.Hands[0], 40);
            player1.Hands[0].AddCard(_aceOfDiamonds);
            player1.Hands[0].AddCard(_kingOfSpades);

            dealer.SettleInCaseOfDealerNatural();

            Assert.AreEqual(100, player1.Bank.Balance);
            Assert.AreEqual(0, player1.Hands[0].Wager);
            Assert.AreEqual(60, player2.Bank.Balance);
            Assert.AreEqual(0, player2.Hands[0].Wager);
            Assert.AreEqual(140, dealer.Bank.Balance);
        }

        [TestMethod]
        public void DealerTakeTurn_DealerHasSoft17_DealerHits()
        {
            var dealer = new Dealer();
            dealer.Hand.AddCard(_aceOfDiamonds);
            dealer.Hand.AddCard(_sixOfClubs);

            dealer.TakeTurn();

            Assert.IsTrue(dealer.Hand.Value >= 17);
        }

        [TestMethod]
        public void DealerTakeTurn_DealerHasHard17_DealerStands()
        {
            var dealer = new Dealer();
            dealer.Hand.AddCard(_kingOfSpades);
            dealer.Hand.AddCard(_sevenOfClubs);

            dealer.TakeTurn();

            Assert.AreEqual(2, dealer.Hand.Cards.Count);
            Assert.AreEqual(17, dealer.Hand.Value);
        }

        [TestMethod]
        public void DealerTakeTurn_DealerHasUnder17_DealerHits()
        {
            var dealer = new Dealer();
            dealer.Hand.AddCard(_fiveOfHearts);
            dealer.Hand.AddCard(_sevenOfClubs);

            dealer.TakeTurn();

            Assert.IsTrue(dealer.Hand.Value >= 17);
        }

        [TestMethod]
        public void SettleBets_PlayerHandIsBust_DealerCollectsBet()
        {
            var dealer = new Dealer(100);
            var player = new Player(100);
            Game.Players.Add(player);
            player.Hands[0].AddCard(_sixOfClubs);
            player.Hands[0].AddCard(_sevenOfClubs);
            player.Hands[0].AddCard(_kingOfSpades);
            player.PlaceBet(player.Hands[0], 50);

            dealer.SettleBets();

            Assert.AreEqual(50, player.Bank.Balance);
            Assert.AreEqual(150, dealer.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
        }

        [TestMethod]
        public void SettleBets_DrawOf16_PlayerRetainsBet()
        {
            var dealer = new Dealer(100);
            var player = new Player(100);
            Game.Players.Add(player);
            player.Hands[0].AddCard(_sixOfClubs);
            player.Hands[0].AddCard(_kingOfSpades);
            dealer.Hand.AddCard(_sixOfClubs);
            dealer.Hand.AddCard(_kingOfSpades);
            player.PlaceBet(player.Hands[0], 50);

            dealer.SettleBets();

            Assert.AreEqual(100, player.Bank.Balance);
            Assert.AreEqual(100, dealer.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
        }

        [TestMethod]
        public void SettleBets_DrawOf21_PlayerRetainsBet()
        {
            var dealer = new Dealer(100);
            var player = new Player(100);
            Game.Players.Add(player);
            player.Hands[0].AddCard(_aceOfDiamonds);
            player.Hands[0].AddCard(_kingOfSpades);
            dealer.Hand.AddCard(_aceOfDiamonds);
            dealer.Hand.AddCard(_kingOfSpades);
            player.PlaceBet(player.Hands[0], 50);

            dealer.SettleBets();

            Assert.AreEqual(100, player.Bank.Balance);
            Assert.AreEqual(100, dealer.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
        }

        [TestMethod]
        public void SettleBets_DealerHandIsBust_DealerPaysBet()
        {
            var dealer = new Dealer(100);
            var player = new Player(100);
            Game.Players.Add(player);
            dealer.Hands[0].AddCard(_sixOfClubs);
            dealer.Hands[0].AddCard(_sevenOfClubs);
            dealer.Hands[0].AddCard(_kingOfSpades);
            player.PlaceBet(player.Hands[0], 50);

            dealer.SettleBets();

            Assert.AreEqual(150, player.Bank.Balance);
            Assert.AreEqual(50, dealer.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
        }

        [TestMethod]
        public void SettleBets_PlayerBeatsDealer_PlayerGetsPayout()
        {
            var dealer = new Dealer(100);
            var player = new Player(100);
            Game.Players.Add(player);
            dealer.Hands[0].AddCard(_sixOfClubs);
            dealer.Hands[0].AddCard(_sixOfClubs);
            player.Hands[0].AddCard(_sevenOfClubs);
            player.Hands[0].AddCard(_kingOfSpades);
            player.PlaceBet(player.Hands[0], 50);

            dealer.SettleBets();

            Assert.AreEqual(150, player.Bank.Balance);
            Assert.AreEqual(50, dealer.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
        }

        [TestMethod]
        public void SettleBets_DealerBeatsPlayer_DealerCollectsBet()
        {
            var dealer = new Dealer(100);
            var player = new Player(100);
            Game.Players.Add(player);
            player.Hands[0].AddCard(_sixOfClubs);
            player.Hands[0].AddCard(_sixOfClubs);
            dealer.Hands[0].AddCard(_sevenOfClubs);
            dealer.Hands[0].AddCard(_kingOfSpades);
            player.PlaceBet(player.Hands[0], 50);

            dealer.SettleBets();

            Assert.AreEqual(50, player.Bank.Balance);
            Assert.AreEqual(150, dealer.Bank.Balance);
            Assert.AreEqual(0, player.Hands[0].Wager);
        }

        [TestMethod]
        public void Deck_NextCard()
        {
            var expectedNextCard = Deck.Cards[0];

            var actualNextCard = Deck.NextCard();

            Assert.AreEqual(expectedNextCard, actualNextCard);
        }

        [TestMethod]
        public void Deck_CardsAreLow()
        {
            var numOfCards = Deck.Cards.Count;
            Deck.Cards.RemoveRange(0, numOfCards - 20);

            var areCardsLow = Deck.CardsAreLow();

            Assert.IsTrue(areCardsLow);
            Assert.AreEqual(20, Deck.Cards.Count);
        }

        [TestMethod]
        public void Participant_DiscardHand()
        {
            Deck.DiscardPile.Clear();
            Game.Participants.Clear();
            var dealer = new Dealer();
            var player = new Player();
            Game.Participants.Add(dealer);
            Game.Participants.Add(player);
            player.Hands[0] = new Hand() { Cards = CardFactory.Generate2CardHand() };
            dealer.Hands[0] = new Hand() { Cards = CardFactory.Generate2CardHand() };

            Game.Participants.ForEach(part => part.Discard());

            Assert.AreEqual(0, player.Hands[0].Cards.Count);
            Assert.AreEqual(0, dealer.Hands[0].Cards.Count);
            Assert.AreEqual(4, Deck.DiscardPile.Count);
        }

        [TestMethod]
        public void Player_PlayRound()
        {
            var playerIO = new Mock<IPlayerIO>();
            playerIO.Setup(x => x.GetTurnDecision(It.IsAny<List<TurnOptions>>())).Returns(new Hit());
            var player = new Player(playerIO.Object);

            player.TakeTurn();

            Assert.IsTrue(player.Hands[0].Cards.Count > 1);
        }
    }
}