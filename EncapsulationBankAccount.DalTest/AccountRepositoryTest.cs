using EncapsulationBankAccount.Dal;
using EncapsulationBankAccount.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        #region Database Connection
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
        #endregion
    }


}
