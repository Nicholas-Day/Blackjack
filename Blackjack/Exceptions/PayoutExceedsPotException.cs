using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Exceptions
{
    public class PayoutExceedsPotException : Exception
    {
        public PayoutExceedsPotException() : base("Total prize amount of payouts exceeds pot size.") { }
        public PayoutExceedsPotException(string message) : base(message) { }
    }
}
