using Blackjack.Exceptions;
using Blackjack.Helpers;
using Blackjack.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public static class Game
    {
        public static List<Participant> Participants { get; private set; } = new List<Participant>();
        public static List<Player> Players { get; private set; } = new List<Player>();
        public static Dealer Dealer { get; private set; }

        public static void Start()
        {
            while (Players.Any())
            {
                if (Deck.CardsAreLow())
                {
                    Deck.Shuffle();
                }
                PlayRound();
                AskPlayersForAnotherRound();
            }
        }
        private static void AskPlayersForAnotherRound()
        {
            foreach (Player player in Players.ToList())
            {
                PlayerIO.AskToPlayAgain(player);
            }
        }
        internal static void CloseOutPlayer(Player player)
        {
            // TODO: return players remaining bankroll
            Players.Remove(player);
        }

        private static void PlayRound()
        {
            Dealer.DealInitialCards();

            if (Dealer.HasAceShowing)
            {
                OfferInsurance();
                Dealer.SettleInsuranceBets();
            }
            if (ParticipantHasNatural())
            {
                PayoutOnNaturals();
            }
            else
            {
                Players.ForEach(player => player.TakeTurn());
                Dealer.TakeTurn();
                Dealer.SettleBets();
            }
            Participants.ForEach(participant => participant.Discard());
        }
        private static void OfferInsurance()
        {
            foreach (Player player in Players)
            {
                bool wantsInsurance = PlayerIO.GetInsuranceResponse();
                if (wantsInsurance)
                {
                    player.PlaceInsuranceBet();
                }
            }
        }

        private static bool ParticipantHasNatural()
        {
            foreach (Participant participant in Participants)
            {
                if (participant.HasNatural)
                {
                    return true;
                }
            }
            return false;
        }
        private static void PayoutOnNaturals()
        {
            if (Dealer.HasNatural)
            {
                Dealer.SettleInCaseOfDealerNatural();
            }
            else
            {
                Dealer.SettleBets();
            }
        }

        public static void InitializeParticipants()
        {
            int playerCount = GetNumberOfPlayers();
            InitializePlayers(playerCount);
            InitializeDealer();
        }
        private static void InitializePlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                string name = GetPlayerName(i);
                int bankroll = GetPlayerBankroll(name);
                try
                {
                    Players.Add(new Player(bankroll, name));
                }
                catch (NegativeBalanceDeclarationException)
                {

                }
                catch (Exception)
                {
                    throw;
                }
            }
            Participants.AddRange(Players);
        }
        private static int GetNumberOfPlayers()
        {
            return PlayerIO.NumberOfPlayers();
        }
        private static string GetPlayerName(int i)
        {
            return PlayerIO.PlayerName(i);
        }
        private static int GetPlayerBankroll(string name)
        {
            int bankroll = PlayerIO.GetBuyIn($"Enter {name}'s starting bankroll: ");
            return bankroll;
        }

        private static void InitializeDealer()
        {
            int houseBank = GetHouseBank();
            Dealer = new Dealer(houseBank);
            Participants.Add(Dealer);
        }
        private static int GetHouseBank()
        {
            return PlayerIO.HouseBank();
        }
    }
}
