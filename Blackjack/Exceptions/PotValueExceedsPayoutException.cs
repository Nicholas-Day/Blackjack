using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.Exceptions
{
    public class PotValueExceedsPayoutException : Exception
    {
        public PotValueExceedsPayoutException() : base("Pot size is greater than total prize payout.") { }
        public PotValueExceedsPayoutException(string message) : base(message) { }
    }
}
