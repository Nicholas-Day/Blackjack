using System;
using System.Runtime.Serialization;

namespace Blackjack.Exceptions
{
    [Serializable]
    public class NegativeDepositException : ArgumentOutOfRangeException
    {
        public NegativeDepositException() : base("Cannot deposit a negative amount", new ArgumentOutOfRangeException()) { }
        public NegativeDepositException(string message) : base(message) { }
        public NegativeDepositException(string message, Exception innerException) : base(message, innerException) { }
        protected NegativeDepositException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}