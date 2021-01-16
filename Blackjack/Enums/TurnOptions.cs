namespace Blackjack.Enums
{
    public class TurnOptions : Enumeration
    {
        public readonly bool IsExclusiveToNewHands;

        public static TurnOptions Hit = new TurnOptions(1, "Hit", false);
        public static TurnOptions Stand = new TurnOptions(2, "Stand", false);
        public static TurnOptions Split = new TurnOptions(3, "Split", true);
        public static TurnOptions DoubleDown = new TurnOptions(4, "Double down", true);
        public static TurnOptions Insurance = new TurnOptions(5, "Insurance", true);
        public static TurnOptions Surrender = new TurnOptions(6, "Surrender", false);

        public TurnOptions() { }
        private TurnOptions(int value, string displayName, bool isExclusiveToInitialHand) : base(value, displayName) { IsExclusiveToNewHands = isExclusiveToInitialHand; }
    }
}
