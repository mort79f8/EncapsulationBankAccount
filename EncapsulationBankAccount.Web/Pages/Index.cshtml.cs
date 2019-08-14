using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncapsulationBankAccount.Dal;
using EncapsulationBankAccount.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EncapsulationBankAccount.Web.Pages
{
    public class IndexModel : PageModel
    {
        public Account account { get; set; }
        public List<Account> accounts { get; set; } = new List<Account>();

        public IActionResult OnGet()
        {
            return InitializeData();
        }

        public IActionResult InitializeData()
        {
            AccountRepository accountRepository = new AccountRepository();
            try
            {
                accounts = accountRepository.GetAll();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return Page();
        }

        public decimal CalcTotalBalance(List<Account> accounts)
        {
            decimal total = 0;
            foreach (var account in accounts)
            {
                total += account.Balance;
            }
            return total;
        }
    }
}