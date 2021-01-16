namespace Blackjack.Enums
{
    public class Ranks : Enumeration
    {
        public const int Count = 13;
        public readonly int NumericValue;

        public static readonly Ranks Two = new Ranks(0, " 2", 2);
        public static readonly Ranks Three = new Ranks(1, " 3", 3);
        public static readonly Ranks Four = new Ranks(2, " 4", 4);
        public static readonly Ranks Five = new Ranks(3, " 5", 5);
        public static readonly Ranks Six = new Ranks(4, " 6", 6);
        public static readonly Ranks Seven = new Ranks(5, " 7", 7);
        public static readonly Ranks Eight = new Ranks(6, " 8", 8);
        public static readonly Ranks Nine = new Ranks(7, " 9", 9);
        public static readonly Ranks Ten = new Ranks(8, "10", 10);
        public static readonly Ranks Jack = new Ranks(9, "JJ", 10);
        public static readonly Ranks Queen = new Ranks(10, "QQ", 10);
        public static readonly Ranks King = new Ranks(11, "KK", 10);
        public static readonly Ranks Ace = new Ranks(12, "AA", 11);
        public static readonly Ranks Unknown = new Ranks(13, "XX", 0);

        public Ranks() { }
        private Ranks(int value, string displayName, int numericValue) : base(value, displayName) { NumericValue = numericValue; } 
    }
}
