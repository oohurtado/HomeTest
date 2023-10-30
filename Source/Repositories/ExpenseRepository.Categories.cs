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
        public IQueryable<Category> GetCategory(string userId, int id)
        {
            var query = from sc in context.SuperCategories
                        join c in context.Categories on sc.Id equals c.SuperCategoryId
                        where sc.UserId == userId && c.Id == id
                        select c;

            return query;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await context.Categories.AddAsync(category);
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
        }
    }
}
