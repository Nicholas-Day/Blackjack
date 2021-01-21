using Blackjack.Exceptions;

namespace Blackjack.Models
{
    public class Bank
    {
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

        public int Balance { get; private set; }

        public void Deposit(int depositAmount)
        {
            if (depositAmount < 0)
            {
                throw new NegativeDepositException();
            }
            Balance += depositAmount;
        }
        public void Withdraw(int withdrawlAmount)
        {
            if (withdrawlAmount < 0)
            {
                throw new NegativeWithdrawException();
            }
            if (withdrawlAmount > Balance)
            {
                throw new InsufficientFundsException();
            }
            Balance -= withdrawlAmount;
        }
        public bool HasEnoughFunds(int amount)
        {
            if (amount > Balance)
            {
                return false;
            }
            return true;
        }
    }
}
