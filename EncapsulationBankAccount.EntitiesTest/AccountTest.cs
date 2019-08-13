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
        public void IsfalseifBalanceIsOutsideGivenInterval()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
