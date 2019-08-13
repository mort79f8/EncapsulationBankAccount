using System;
using System.Collections.Generic;
using System.Text;

namespace EncapsulationBankAccount.Entities
{
    public class Account
    {
        #region fields
        private int id;
        private decimal balance;
        private DateTime created;
        #endregion
        #region properties
        public int Id { get => id; set => id = value; }

        public decimal Balance
        {
            get
            {
                return balance;
            }

            set
            {
                var validateResult = ValidateBalance(value);
                if (!validateResult.isValid)
                {
                    throw new ArgumentException(validateResult.errorMessage);
                }
                else
                {
                    balance = value;
                }
               
            }
        }
        public DateTime Created { get => created; set => created = value; }
        #endregion
        #region constructor
        public Account(int id, decimal balance, DateTime created)
        {
            if (id == 0)
            {
                throw new ArgumentException("Id is 0");
            }
            else
            {
                Id = id;
                Balance = balance;
                Created = created;
            }
        }

        public Account(decimal initalBalance)
        {
            Balance = initalBalance;
            Created = DateTime.Today;
        }
        #endregion

        public static (bool isValid, string errorMessage) ValidateBalance(decimal balance)
        {
            if (balance < -999999999 || balance > 999999999)
            {
                return (false, "balance is above 999999999 or below -999999999");
            }
            else
            {
                return (true, "balance is valid");
            }
        }



        public void Withdraw(decimal amount)
        {
            if (amount < 0 || amount > 25000)
            {
                throw new ArgumentException("Amount that is getting withdraw is more that 25000 or less than 0");
            }
            else
            {
                Balance -= amount;
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0 || amount > 25000)
            {
                throw new ArgumentException("Amount that is getting deposited is more that 25000 or less than 0");
            }
            else
            {
                Balance += amount;
            }
        }


        public int GetDaysSinceCreation()
        {
            return (DateTime.Today - Created.Date).Days;
        }
    }
}
