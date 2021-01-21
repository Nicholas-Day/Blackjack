using System;
using System.Runtime.Serialization;

namespace Blackjack.Exceptions
{
    [Serializable]
    public class NegativeWithdrawException : ArgumentOutOfRangeException
    {
        public NegativeWithdrawException() : base(message: "Cannot withdraw a negative amount", new ArgumentOutOfRangeException()) { }
        public NegativeWithdrawException(string message) : base(message) { }
        public NegativeWithdrawException(string message, Exception innerException) : base(message, innerException) { }
        protected NegativeWithdrawException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}