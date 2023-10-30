using Home.Models.Entities.Other;
using Home.Source.Common;
using Home.Source.Database;
using System.Linq;
using System.Linq.Expressions;

namespace Home.Source.Repositories
{
    public class ActivityRepository : BaseRepository
    {
        public ActivityRepository(DatabaseContext context) : base(context)
        {
        }

        public IQueryable<Activity> GetActivity(string userId, int id)
        {
            return context.Activities.Where(p => p.UserId == userId && p.Id == id);
        }

        public IQueryable<Activity> GetActivitiesByDate(string userId, string orderColumn, string order, string term, string searchColumn, DateTime dateMin, DateTime dateMax)
        {
            var query = context.Activities.Where(p => p.UserId == userId && p.Date >= dateMin && p.Date <= dateMax);
            IOrderedQueryable<Activity> queryOrdered = null!;

            if (!string.IsNullOrEmpty(term))
            {
                Expression<Func<Activity, bool>> exp = p => true;

                if (searchColumn.ToLower() == "description")
                {
                    exp = p => p.Description!.Contains(term);
                }

                query = query.Where(exp);
            }

            if (orderColumn.ToLower() == "date")
            {
                if (order.ToLower() == "asc")
                {
                    queryOrdered = query.OrderBy(p => p.Date).ThenByDescending(p => p.IsDone);
                }
                else
                {
                    queryOrdered = query.OrderByDescending(p => p.Date).ThenBy(p => p.IsDone);
                }
            }

            return queryOrdered.AsQueryable();
        }

        public async Task CreateActivityAsync(Activity activity)
        {
            await context.Activities.AddAsync(activity);
        }

        public void DeleteActivity(Activity activity)
        {
            context.Activities.Remove(activity);
        }

        public IQueryable<string> GetTags(string userId)
        {
            var query = context.Activities.Where(p => p.UserId == userId).Select(p => p.Tag).Distinct().OrderBy(p => p);
            return query;
        }
    }
}
