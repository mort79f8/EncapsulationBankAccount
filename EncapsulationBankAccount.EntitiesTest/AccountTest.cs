using EncapsulationBankAccount.Entities;
using System;
using Xunit;

namespace EncapsulationBankAccount.EntitiesTest
{
    public class AccountTest
    {
        [Fact]
        public void IdShouldBeZeroWhenCreatingNewAccount()
        {
            //Arrange
            Account account = new Account(5000);

            //Act
            int actualId = account.Id;

            //Assert
            Assert.Equal(0, actualId);
        }

        [Fact]
        public void IdShouldBeGreaterThanZeroWhenReadingFromPresistentDataStorage()
        {
            //Arrange
            Account account = new Account(5, 100, DateTime.Today);

            //Act
            int actualId = account.Id;

            //Assert
            Assert.NotEqual(0, actualId);
        }

        [Fact]
        public void BalanceCanBeNegative()
        {
            //Arrange
            Account account = new Account(4, -1034, DateTime.Today);

            //Act
            decimal actualBalance = account.Balance;

            //Assert
            Assert.True(actualBalance < 0); 
        }

        [Fact]
        public void IsfalseIfBalanceIsOutsideGivenInterval()
        {
            //Arrange
            var validResult = Account.ValidateBalance(1100000000);

            //Act

            //Assert
            Assert.False(validResult.isValid);
        }

        [Fact]
        public void IsTrueIfBalanceIsWithinGivenInterval()
        {
            //Arrange
            var validResult = Account.ValidateBalance(5000000);

            //Act

            //Assert
            Assert.True(validResult.isValid);
        }

        [Fact]
        public void IsFalseIfWithdrawOrDepositIsHigherOrLowerThanGivenInterval()
        {
            //Arrange
            var validResult = Account.ValidateAmount(30000);
            //Act

            //Assert
            Assert.False(validResult.isValid);
        }

        [Fact]
        public void IsTrueIfWithdrawOrDepositIsWithinGivenInterval()
        {
            //Arrange
            var validResult = Account.ValidateAmount(15000);
            //Act

            //Assert
            Assert.True(validResult.isValid);
        }

        [Fact]
        public void GetDaysSinceCreation_ReturnsTrueIfResultIsEqualToGivenNumberInAssert()
        {
            //Arrange
            Account account = new Account(30000);

            //Act
            int actualDaysSinceCreation = account.GetDaysSinceCreation();

            //Assert
            Assert.True(actualDaysSinceCreation == 0);
        }

    }
}
