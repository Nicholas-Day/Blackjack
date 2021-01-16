using Blackjack.Exceptions;

namespace Blackjack.Models
{
    public class Bank
    {
        public int Balance { get; private set; }

        public Bank()
        {
            Balance = 0;
        }
        public Bank(int balance)
        {
            if (balance < 0)
            {
                throw new NegativeBalanceDeclarationException();
            }
            Balance = balance;
        }

        public void Deposit(int depositAmount)
        {
            Balance += depositAmount;
        }
        public void Withdraw(int withdrawlAmount)
        {
            if (withdrawlAmount>Balance)
            {
                throw new InsufficientFundsException();
            }
            Balance -= withdrawlAmount;
        }
        public bool HasFunds(int amount)
        {
            if (amount > Balance)
            {
                return false;
            }
            return true;
        }
    }
}
