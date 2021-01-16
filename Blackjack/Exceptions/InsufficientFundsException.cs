using System;

namespace Blackjack.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Insufficient Funds") { }
        public InsufficientFundsException(string message) : base(message) { }
    }
}
