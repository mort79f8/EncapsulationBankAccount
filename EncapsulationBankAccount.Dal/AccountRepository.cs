using EncapsulationBankAccount.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EncapsulationBankAccount.Dal
{
    public class AccountRepository : BaseRepository
    {

        /// <summary>
        /// Get all Accounts from the Database
        /// </summary>
        /// <returns>A <list type="Account"> List of accounts</returns>
        public List<Account> GetAll()
        {
            string sql = "SELECT * FROM Accounts";
            DataTable dataTable = ExecuteQuery(sql);
            if (dataTable == null)
                throw new InvalidOperationException($"Datatable was null. SQL string is: {sql}");
            return HandleData(dataTable);
        }

        /// <summary>
        /// Get a single account
        /// </summary>
        /// <param name="accountId">The Id that is gettign retrived</param>
        /// <returns>An Account</returns>
        public Account GetAccount(int accountId)
        {
            string sql = $"SELECT * FROM Accounts WHERE Id={accountId}";
            DataTable dataTable = ExecuteQuery(sql);
            if (dataTable == null)
                throw new InvalidOperationException($"DataTable was null. SQL string is: {sql}");
            return HandleData(dataTable).FirstOrDefault();
            
        }

        /// <summary>
        /// Add an account to the Database.
        /// </summary>
        /// <param name="account">the account that is being added.</param>
        /// <returns>Affected rows</returns>
        public int Insert(Account account)
        {
            string sql = $"INSERT INTO Accounts VALUES ({account.Balance}, '{account.Created.Date:yyyy-MM-dd}')";
            return ExecuteNonQuery(sql);
        }

        /// <summary>
        /// Delete account
        /// </summary>
        /// <param name="accountId">Account id that is getting deleted</param>
        /// <returns>Affected rows</returns>
        public int Delete(int accountId)
        {
            string sql = $"DELETE FROM Accounts WHERE Id={accountId}";
            return ExecuteNonQuery(sql);
        }

        /// <summary>
        /// Updates an account
        /// </summary>
        /// <param name="account">account that is being updated</param>
        /// <returns>Affected rows</returns>
        public int Update(Account account)
        {
            string sql = $"UPDATE Accounts SET Balance={account.Balance} WHERE Id={account.Id}";
            return ExecuteNonQuery(sql);
        }
    
        /// <summary>
        /// Handles the Data from the Database.
        /// </summary>
        /// <param name="dataTable">The datatable retrived from the database</param>
        /// <returns>A <list type="Account"/> List of accounts</returns>
        public List<Account> HandleData(DataTable dataTable)
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

    }
}
