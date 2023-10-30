using Home.Models.Entities.Money;
using Home.Models.Entities.Other;
using Home.Source.Database;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Home.Source.Common;

namespace Home.Source.Repositories
{
    public partial class ExpenseRepository : BaseRepository
    {
        public IQueryable<Account> GetAccount(string userId, int id)
        {
            return context.Accounts.Where(p => p.UserId == userId && p.Id == id);
        }

        public IQueryable<Account> GetAccounts(string userId)
        {         
            return context.Accounts.Where(p => p.UserId == userId).AsQueryable();
        }

        public async Task CreateAccountAsync(Account account)
        {
            await context.Accounts.AddAsync(account);            
        }

        public void DeleteAccount(Account account)
        {
            context.Accounts.Remove(account);
        }

        public async Task CreateAccountActivityAsync(AccountActivity accountActivity)
        {
            await context.AccountActivities.AddAsync(accountActivity);
        }

        public async Task CreateAccountActivityAsync(int accountId, decimal accountAmount, DateTime accountDate)
        {
            var accountActivity = new AccountActivity()
            {
                AccountId = accountId,
                Amount = accountAmount,
                Date = accountDate,
            };

            await CreateAccountActivityAsync(accountActivity);
        }
    }
}
