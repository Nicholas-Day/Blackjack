using System;

namespace Blackjack.Exceptions
{
    public class NegativeBalanceDeclarationException : ArgumentOutOfRangeException
    {
        public NegativeBalanceDeclarationException() : base("Cannot declare negative balance") { }
        public NegativeBalanceDeclarationException(string message) : base(message) { }
    }
}
