using Home.Models.Entities.Other;
using Home.Source.Common;
using Home.Source.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;

namespace Home.Source.Repositories
{
    public class EventRepository : BaseRepository
    {
        public EventRepository(DatabaseContext context) : base(context)
        {
        }

        public IQueryable<Event> GetEvent(string userId, int personId, int eventId)
        {
            var query = from e in context.Events
                        join p in context.People
                        on e.PersonId equals p.Id
                        where p.UserId == userId && e.Id == eventId && e.PersonId == personId
                        select e;

            return query;
        }

        public IQueryable<Event> GetEventsByPage(string userId, int personId, string orderColumn, string order, int pageNumber, int pageSize, string term, string searchColumn, out int grandTotal)
        {
            var query = from e in context.Events
                        join p in context.People
                        on e.PersonId equals p.Id
                        where p.UserId == userId && e.PersonId == personId
                        select e;

            if (!string.IsNullOrEmpty(term))
            {
                Expression<Func<Event, bool>> exp = p => true;

                if (searchColumn == "description")
                {
                    exp = p => p.Description!.Contains(term);
                }

                query = query.Where(exp);
            }

            grandTotal = query.Count();

            IOrderedQueryable<Event> queryOrdered = null!;
            if (orderColumn == "date" && order == "asc")
                queryOrdered = query.OrderBy(p => p.Date).ThenBy(p => p.Time);
            else if (orderColumn == "date" && order == "desc")
                queryOrdered = query.OrderByDescending(p => p.Date).ThenBy(p => p.Time);         

            query = queryOrdered
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return query;
        }

        public async Task CreateEventAsync(Event @event)
        {
            await context.Events.AddAsync(@event);
        }
      
        public void DeleteEvent(Event @event)
        {
            context.Events.Remove(@event);
        }
    }
}
