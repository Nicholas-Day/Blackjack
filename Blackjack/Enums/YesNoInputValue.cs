using System.Collections.Generic;

namespace Blackjack.Enums
{
    public static class YesNoInputValue
    {
        public static readonly List<string> Yes = new List<string>() { "Yes", "Y", "1" };
        public static readonly List<string> No = new List<string>() { "No", "N", "2" };
        public static readonly List<string> ValidValues = new List<string>() { "Yes", "Y", "1", "No", "N", "2" };
    }
}
