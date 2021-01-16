using System;

namespace Blackjack.Enums
{
    public class Suits : Enumeration
    {
        public const int Count = 4;
        public readonly ConsoleColor Color;

        public static readonly Suits Clubs = new Suits(0, "♣", ConsoleColor.Black);
        public static readonly Suits Diamonds = new Suits(1, "♦", ConsoleColor.Red);
        public static readonly Suits Hearts = new Suits(2, "♥", ConsoleColor.Red);
        public static readonly Suits Spades = new Suits(3, "♠", ConsoleColor.Black);
        public static readonly Suits Unknown = new Suits(4, "◘", ConsoleColor.DarkGray);

        public Suits() { }
        private Suits(int value, string displayName, ConsoleColor color) : base(value, displayName) { Color = color; }
    }
}
