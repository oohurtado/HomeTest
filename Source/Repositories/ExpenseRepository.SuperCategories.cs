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
        public IQueryable<SuperCategory> GetSuperCategory(string userId, int id)
        {
            return context.SuperCategories.Where(p => p.UserId == userId && p.Id == id);
        }

        public IQueryable<SuperCategory> GetSuperCategories(string userId, bool includeCategories)
        {
            if (includeCategories)
            {
                return context.SuperCategories.Include(p => p.Categories).Where(p => p.UserId == userId && p.Categories.Count > 0).AsQueryable();
            }

            return context.SuperCategories.Where(p => p.UserId == userId).AsQueryable();           
        }

        public async Task CreateSuperCategoryAsync(SuperCategory superCategory)
        {
            await context.SuperCategories.AddAsync(superCategory);
        }

        public void DeleteSuperCategoryAsync(SuperCategory superCategory)
        {
            context.SuperCategories.Remove(superCategory);
        }
    }
}
