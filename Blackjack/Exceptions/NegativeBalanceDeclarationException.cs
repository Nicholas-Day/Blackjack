using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Exceptions
{
    public class NegativeBalanceDeclarationException : Exception
    {
        public NegativeBalanceDeclarationException() : base("Cannot declare negative balance.") { }
        public NegativeBalanceDeclarationException(string message) : base(message) { }
    }
}
