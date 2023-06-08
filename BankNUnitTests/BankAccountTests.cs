using BankNUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNUnitTests
{
    public class BankAccountTests
    {
        private BankAccount _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new BankAccount(1000);
        }

        [Test]
        public void Depositing_Funds_Updates_Balance()
        {
            // ARRANGE
            var _sut = new BankAccount(1000);
            var amountToDeposit = 500;
            var expectedBalance = 1500; // 1000 + 500

            // ACT
            _sut.Deposit(amountToDeposit);
            var result = _sut.Balance;

            // ASSERT
            Assert.AreEqual(expectedBalance, result);
        }
        [Test]
        public void Withdrawing_Funds_Updates_Balance()
        {
            // ARRANGE
            var _sut = new BankAccount(1000);
            var amountToWithdraw = 250;
            var expectedBalance = 750; // 1000 - 250

            // ACT
            _sut.Withdraw(amountToWithdraw);
            var result = _sut.Balance;

            // ASSERT
            Assert.AreEqual(expectedBalance, result);
        }
        [Test]
        public void Transfering_Funds_Updates_Both_Accounts()
        {
            // ARRANGE
            var _sut = new BankAccount(1000);
            var otherAccount = new BankAccount(250);
            var amountToTransfer = 1000;
            var expectedAccountBalance = 0; // 1000 - 1000 
            var expectedOtherAccountBalance = 1250; // 250 + 1000

            // ACT
            _sut.Transfer(otherAccount, amountToTransfer);
            var resultAccount = _sut.Balance;
            var resultOtherAccount = otherAccount.Balance;

            // ASSERT
            Assert.AreEqual(expectedAccountBalance, resultAccount);
            Assert.AreEqual(expectedOtherAccountBalance, resultOtherAccount);
        }
        [Test]
        public void Depositing_Negative_Throws_Error()
        {
            // ARRANGE
            var _sut = new BankAccount(1000);
            var amountToDeposit = -100;

            // ACT + ASSERT
            Assert.Throws<ArgumentOutOfRangeException>
                (() => _sut.Deposit(amountToDeposit));
        }
        [Test]
        public void Withdrawing_Negative_Throws_Error()
        {
            // ARRANGE
            var _sut = new BankAccount(1000);
            var amountToWithdraw = -100;

            // ACT + ASSERT
            Assert.Throws<ArgumentOutOfRangeException>
                (() => _sut.Withdraw(amountToWithdraw));
        }
        [Test]
        public void Withdrawing_More_Than_Balance_Throws_Error()
        {
            // ARRANGE
            var _sut = new BankAccount(1000);
            var amountToWithdraw = 1100;

            // ACT + ASSERT
            Assert.Throws<ArgumentOutOfRangeException>
                (() => _sut.Withdraw(amountToWithdraw));
        }
        [Test]
        public void Transfer_To_Nonexisting_Account_Throws_Error()
        {
            // ARRANGE
            var _sut = new BankAccount(1000);
            var amountToTransfer = 500;

            // ACT + ASSERT
            Assert.Throws<ArgumentNullException>
                (() => _sut.Transfer(null, amountToTransfer));
        }
        [Test]
        public void Depositing_Funds_Updates_Balance_Enum()
        {
            // ARRANGE
            // var _sut = new BankAccount(1000);
            var amountToDeposit = 500;
            var expectedBalance = 1500; // 1000 + 500

            // ACT
            var returnEnum = _sut.DepositEnum(amountToDeposit);
            var result = _sut.Balance;

            // ASSERT
            Assert.AreEqual(expectedBalance, result);
            Assert.That(returnEnum, Is.EqualTo(ReturnStatus.Success));
        }
        [Test]
        public void Depositing_Negative_Throws_Enum_Error()
        {
            // ARRANGE
            // var _sut = new BankAccount(1000);
            var amountToDeposit = -500; // Not allowed!
            var expectedBalance = 1000; // Balance ska inte ändras

            // ACT
            var returnEnum = _sut.DepositEnum(amountToDeposit);
            var result = _sut.Balance;

            // ASSERT
            Assert.AreEqual(expectedBalance, result);
            Assert.That(returnEnum, Is.EqualTo(ReturnStatus.DepositLessThanZero));
        }
        [Test]
        public void Withdrawing_Funds_Updates_Balance_Enum()
        {
            // ARRANGE
            var amountToWithdraw = 500;
            var expectedBalance = 500; // 1000 - 500

            // ACT
            var returnEnum = _sut.WithdrawEnum(amountToWithdraw);
            var result = _sut.Balance;

            // ASSERT
            Assert.AreEqual(expectedBalance, result);
            Assert.That(returnEnum, Is.EqualTo(ReturnStatus.Success));
        }
        [Test]
        public void Withdrawing_More_Than_Balance_Throws_Enum_Error()
        {
            // ARRANGE
            var amountToWithdraw = 1500;
            var expectedBalance = 1000;

            // ACT
            var returnEnum = _sut.WithdrawEnum(amountToWithdraw);
            var result = _sut.Balance;

            // ASSERT
            Assert.AreEqual(expectedBalance, result);
            Assert.That(returnEnum, Is.EqualTo(ReturnStatus.NotEnoughBalance));
        }
        [Test]
        public void Transfering_Funds_Updates_Both_Accounts_Enum()
        {
            // ARRANGE
            var toAccount = new BankAccount(1000);
            var amountToDeposit = 500;
            var expectedFromAccountBalance = 500;
            var expectedToAccountBalance = 1500;

            // ACT
            var returnEnum = _sut.TransferEnum(toAccount, amountToDeposit);
            var fromAccountBalance = _sut.Balance;
            var toAccountBalance = toAccount.Balance;

            // ASSERT
            Assert.AreEqual(expectedFromAccountBalance, fromAccountBalance);
            Assert.AreEqual(expectedToAccountBalance, toAccountBalance);
            Assert.That(returnEnum, Is.EqualTo(ReturnStatus.Success));
        }
        [Test]
        public void Transfer_To_Nonexisting_Account_Throws_Enum_Error()
        {
            // ARRANGE
            var amountToDeposit = 500;
            var expectedFromAccountBalance = 1000;

            // ACT
            var returnEnum = _sut.TransferEnum(null, amountToDeposit);
            var fromAccountBalance = _sut.Balance;

            // ASSERT
            Assert.AreEqual(expectedFromAccountBalance, fromAccountBalance);
            Assert.That(returnEnum, Is.EqualTo(ReturnStatus.AccountDoesNotExist));
        } 

    }
}
