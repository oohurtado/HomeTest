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
        public IQueryable<Entry> GetEntry(string userId, int id)
        {
            return context.Entries.Where(p => p.UserId == userId && p.Id == id);
        }

        public async Task CreateEntryAsync(Entry entry)
        {
            await context.Entries.AddAsync(entry);
        }

        public void DeleteEntry(Entry entry)
        {
            context.Entries.Remove(entry);
        }
    }
}
