using System;
using System.Runtime.Serialization;

namespace Blackjack
{
    [Serializable]
    internal class InvalidInputException : ArgumentOutOfRangeException
    {
        public InvalidInputException() : base("Invalid Input") { }

        public InvalidInputException(string message) : base(message) { }

        public InvalidInputException(string message, Exception innerException) : base(message, innerException) { }

        protected InvalidInputException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}