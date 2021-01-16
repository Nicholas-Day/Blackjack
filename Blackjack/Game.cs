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

        internal static void CloseOutPlayer(Player player)
        {
            // return players remianing bankroll
            Game.Players.Remove(player);
        }

        public static Dealer Dealer { get; private set; } = new Dealer();

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
        private static void AskPlayersForAnotherRound()
        {
            var playersWithNoMoney = Players.Where(player => player.Bank.Balance == 0);
            foreach (var player in Players)
            {
                Display.AskToPlayAgain(player);
            }
        }
        private static bool GameHasPlayers()
        {
            return Players.Any();
        }

        private static void PlayRound()
        {
            Dealer.DealInitialCards();

            if (Dealer.HasAceShowing())
            {
                OfferInsurance();
                SettleInsuranceBets();
            }
            if (ParticipantHasNatural())
            {
                PayoutOnNaturals();
            }
            else
            {
                Players.ForEach(player => player.TakeTurn());
            }
        }

        private static void SettleInsuranceBets()
        {
            throw new NotImplementedException();
        }

        private static void OfferInsurance()
        {
            throw new NotImplementedException();
        }

        private static void PayoutOnNaturals()
        {
            throw new NotImplementedException();
        }

        private static bool ParticipantHasNatural()
        {
            foreach (var participant in Participants)
            {
                if (participant.HasNatural())
                {
                    return true;
                }
            }
            return false;
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
                Console.Clear();
                var name = GetPlayerName(i);
                var bankroll = GetPlayerBankroll(name);
                Players.Add(new Player(name, bankroll));
            }
            Participants.AddRange(Players);
        }
        private static int GetNumberOfPlayers()
        {
            Console.Write("Enter number of players: ");
            var playerCount = Int32.Parse(Console.ReadLine());
            return playerCount;
        }
        private static string GetPlayerName(int i)
        {
            Console.Write($"Enter name for player {i + 1}: ");
            var name = Console.ReadLine();
            return name;
        }
        private static int GetPlayerBankroll(string name)
        {
            var bankroll = Display.GetBuyIn($"Enter {name}'s starting bankroll: ");
            return bankroll;
        }

        private static void InitializeDealer()
        {
            var houseBank = GetHouseBank();
            Dealer.Initialize(houseBank);
            Participants.Add(Dealer);
        }
        private static int GetHouseBank()
        {
            Console.Clear();
            Console.Write("Enter house bankroll: ");
            var bankroll = Console.Read();
            return bankroll;
        }
    }
}
