using System;
using System.Collections.Generic;
using System.Text;

namespace EncapsulationBankAccount.Entities
{
    public class Account
    {
        private int id;
        private decimal balance;
        private DateTime created;

        public int Id { get => id; set => id = value; }
        public decimal Balance { get => balance; set => balance = value; }
        public DateTime Created { get => created; set => created = value; }

        public Account(int id, decimal balance, DateTime created)
        {
            Id = id;
            Balance = balance;
            Created = created;
        }

        public Account (decimal initalBalance)
        {
            Balance = initalBalance;
        }

        public void Withdraw(decimal amount)
        {
            throw NotImplementedException;
        }

        public void Deposit(decimal amount)
        {
            throw NotImplementedException;
        }

        public int GetDaysSinceCreation()
        {
            throw NotImplementedException;
        }
    }
}
