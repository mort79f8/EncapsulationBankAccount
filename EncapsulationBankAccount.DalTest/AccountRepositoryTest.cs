using EncapsulationBankAccount.Dal;
using EncapsulationBankAccount.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Xunit;

namespace EncapsulationBankAccount.DalTest
{
    public class AccountRepositoryTest
    {
        [Fact]
        public void GetAll_IsTrueIfNumbersIsEqual()
        {
            //Arrange
            AccountRepository accountRepository = new AccountRepository();
            List<Account> accounts = accountRepository.GetAll();

            //Act
            int actualAmountOfAccounts = NumberOfAccounts();

            //Assert
            Assert.Equal(accounts.Count, actualAmountOfAccounts);
        }

        //public void GetAccount_IsEqualWhenBothAccountIdIsTheSame()
        //{
        //    //Arrange

        //    //Act


        //    //Assert
        //}

        [Fact]
        public void Update_IsEqualIfAccountGetsUpdated()
        {
            //Arrange
            Account newAccount = new Account(1500);
            AccountRepository accountRepository = new AccountRepository();
            accountRepository.Insert(newAccount);
            Account account = accountRepository.GetAccount(SelectLatestAccount().Id);

            //Act
            account.Balance = 5000;
            accountRepository.Update(account);
            Account updatedAccount = accountRepository.GetAccount(account.Id);
            

            //Assert
            Assert.Equal(account, updatedAccount);
        }

        [Fact]
        public void Insert_IsEqualWhenAnAccountIsInserted()
        {
            //Arrange
            Account account = new Account(30000);
            AccountRepository accountRepository = new AccountRepository();
            int currentNumberOfAccounts = NumberOfAccounts();

            //Act
            accountRepository.Insert(account);
            int numberOfAccountsAfterInsert = NumberOfAccounts();

            //Assert
            Assert.Equal(currentNumberOfAccounts + 1, numberOfAccountsAfterInsert);
        }

        [Fact]
        public void Delete_NumberOfAccountIsReduceByOne()
        {
            //Arrange
            int currentNumberOfAccount = NumberOfAccounts();
            Account account = SelectLatestAccount();
            AccountRepository accountRepository = new AccountRepository();

            //Act
            accountRepository.Delete(account.Id);
            int NumberOfAccountsAfterDelete = NumberOfAccounts();

            //Assert
            Assert.Equal(currentNumberOfAccount - 1, NumberOfAccountsAfterDelete);
        }


        #region Database Connection and Data Handling
        private string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=EncapsulationDatabase;Integrated Security=true;";
        protected DataTable ExecuteQuery(string sql)
        {
            DataTable dataTable = new DataTable(); //The dataset to be returned

            //Uses the connection and command
            //The using makes sure to close and dispose the objects in the end
            using (var con = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, con))
            {
                //Open the connection
                con.Open();
                //Get the data from the database
                SqlDataAdapter da = new SqlDataAdapter(command);

                da.Fill(dataTable);
            }
            return dataTable;
        }

        private int NumberOfAccounts()
        {
            int count = 0;
            string sql = "SELECT COUNT(Id) FROM Accounts";
            DataTable dataTable = ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                count = (int)row[0];
            }


            return count;
        }

        private Account SelectLatestAccount()
        {
            string sql = "SELECT TOP 1 * FROM Accounts ORDER BY ID DESC";
            DataTable dataTable = ExecuteQuery(sql);
            if (dataTable == null)
                throw new InvalidOperationException($"DataTable was null. SQL string is: {sql}");
            return HandleData(dataTable).FirstOrDefault();

        }

        private List<Account> HandleData(DataTable dataTable)
        {
            if (dataTable == null)
                return null;

            List<Account> accounts = new List<Account>();

            foreach (DataRow row in dataTable.Rows)
            {
                accounts.Add(new Account()
                {
                    Id = (int)row["Id"],
                    Balance = (decimal)row["Balance"],
                    Created = (DateTime)row["Created"]
                });
            }
            return accounts;
        }
        #endregion
    }


}
