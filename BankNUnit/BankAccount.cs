using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNUnit
{
    public class BankAccount
    {
        public BankAccount()
        {
        }

        public BankAccount(double balance)
        {
            _balance = balance;
        }

        private double _balance;

        public double Balance
        {
            get { return _balance; }
        }
        public void Deposit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            _balance += amount;
        }
        public void Withdraw(double amount)
        {
            if (amount > _balance)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            _balance -= amount;
        }
        public void Transfer(BankAccount toAccount, double amount)
        {
            if (toAccount is null)
            {
                throw new ArgumentNullException(nameof(toAccount));
            }
            Withdraw(amount);
            toAccount.Deposit(amount);
        }
        public ReturnStatus DepositEnum(double amount)
        {
            var status = new ReturnStatus();
            if (amount < 0)
            {
                return status = ReturnStatus.DepositLessThanZero;
            }

            _balance += amount;
            status = ReturnStatus.Success;
            return status;
        }
        public ReturnStatus WithdrawEnum(double amount)
        {
            var status = new ReturnStatus();
            if (amount < 0)
            {
                return status = ReturnStatus.NegativeAmount;
            }
            if (amount > _balance)
            {
                return status = ReturnStatus.NotEnoughBalance;
            }

            _balance -= amount;
            status = ReturnStatus.Success;
            return status;
        }

        public ReturnStatus TransferEnum(BankAccount toAccount, double amount)
        {
            var status = new ReturnStatus();
            if (toAccount == null)
            {
                return status = ReturnStatus.AccountDoesNotExist;
            }
            if (amount < 0)
            {
                return status = ReturnStatus.NegativeAmount;
            }
            if (amount > _balance)
            {
                return status = ReturnStatus.NotEnoughBalance;
            }

            _balance -= amount;
            toAccount.Deposit(amount);

            status = ReturnStatus.Success;
            return status;
        }
    }
}
