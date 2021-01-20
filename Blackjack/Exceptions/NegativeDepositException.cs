using System;
using System.Runtime.Serialization;

namespace Blackjack.Exceptions
{
    [Serializable]
    internal class NegativeDepositException : Exception
    {
        public NegativeDepositException() : base("Cannot deposit a negative amount")
        {
        }

        public NegativeDepositException(string message) : base(message)
        {
        }

        public NegativeDepositException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NegativeDepositException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}