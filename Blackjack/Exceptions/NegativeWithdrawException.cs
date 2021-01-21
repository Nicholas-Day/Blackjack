using System;
using System.Runtime.Serialization;

namespace Blackjack.Exceptions
{
    [Serializable]
    internal class NegativeWithdrawException : ArgumentOutOfRangeException
    {
        public NegativeWithdrawException() : base("Cannot withdraw a negative amount") { }
        public NegativeWithdrawException(string message) : base(message) { }
        public NegativeWithdrawException(string message, Exception innerException) : base(message, innerException) { }
        protected NegativeWithdrawException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}