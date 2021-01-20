using Blackjack.Helpers;
using Blackjack.Models;
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
            while (GameHasPlayers())
            {
                if (Deck.CardsAreLow())
                {
                    Deck.Shuffle();
                }
                PlayRound();
                AskPlayersForAnotherRound();
            }
        }
        private static bool GameHasPlayers()
        {
            return Players.Any();
        }
        private static void AskPlayersForAnotherRound()
        {
            foreach (var player in Players)
            {
                PlayerIO.AskToPlayAgain(player);
            }
        }
        internal static void CloseOutPlayer(Player player)
        {
            // TODO: return players remianing bankroll
            Game.Players.Remove(player);
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
            foreach (var player in Players)
            {
                var wantsInsurance = PlayerIO.GetInsuranceResponse();
                if (wantsInsurance)
                {
                    player.PlaceInsuranceBet();
                }
            }
        }
        
        private static bool ParticipantHasNatural()
        {
            foreach (var participant in Participants)
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
            var playerCount = GetNumberOfPlayers();
            InitializePlayers(playerCount);
            InitializeDealer();
        }
        private static void InitializePlayers(int playerCount)
        {
            for (var i = 0; i < playerCount; i++)
            {
                var name = GetPlayerName(i);
                var bankroll = GetPlayerBankroll(name);
                Players.Add(new Player(name, bankroll));
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
            var bankroll = PlayerIO.GetBuyIn($"Enter {name}'s starting bankroll: ");
            return bankroll;
        }

        private static void InitializeDealer()
        {
            var houseBank = GetHouseBank();
            Dealer = new Dealer(houseBank);
            Participants.Add(Dealer);
        }
        private static int GetHouseBank()
        {
            return PlayerIO.HouseBank();
        }
    }
}
