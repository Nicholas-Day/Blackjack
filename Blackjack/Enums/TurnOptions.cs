using Blackjack.Interfaces;
using Blackjack.TurnDecisions;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Enums
{
    public class TurnOptions : Enumeration
    {
        public readonly bool IsExclusiveToNewHands;
        public static List<TurnOptions> AllTurnOptions => GetAllTurnOptions();
        public string AllOptions => GetAllOptions();

        private string GetAllOptions()
        {
            var temp = string.Join(',', AllTurnOptions.Select(x => x.DisplayName).ToArray());
            return temp;
        }

        public static TurnOptions Hit = new TurnOptions(1, "Hit", false);
        public static TurnOptions Stand = new TurnOptions(2, "Stand", false);
        public static TurnOptions Surrender = new TurnOptions(3, "Surrender", false);
        public static TurnOptions DoubleDown = new TurnOptions(4, "Double down", true);
        public static TurnOptions Split = new TurnOptions(5, "Split", true);

        public TurnOptions() { }
        private TurnOptions(int value, string displayName, bool isExclusiveToInitialHand) : base(value, displayName) { IsExclusiveToNewHands = isExclusiveToInitialHand; }

        private static List<TurnOptions> GetAllTurnOptions()
        {
            return new List<TurnOptions>() { Hit, Stand, Surrender, Split, DoubleDown };
        }
        public ITurnDecision Convert()
        {
            switch (DisplayName)
            {
                case "Hit":
                    return new Hit();
                default:
                    return new Hit();
            }
        }
    }
}
