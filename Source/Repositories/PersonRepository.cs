using Home.Models.Entities.Other;
using Home.Source.Common;
using Home.Source.Database;
using System.Linq.Expressions;

namespace Home.Source.Repositories
{
    public class PersonRepository : BaseRepository
    {
        public PersonRepository(DatabaseContext context) : base(context)
        {
            
        }

        public IQueryable<Person> GetPerson(string userId, int id)
        {
            return context.People.Where(p => p.UserId == userId && p.Id == id);
        }

        public IQueryable<Person> GetPersonsByPage(string userId, string orderColumn, string order, int pageNumber, int pageSize, string term, string searchColumn, out int grandTotal)
        {
            var query = context.People.Where(p => p.UserId == userId).AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                Expression<Func<Person, bool>> exp = p => true;

                if (searchColumn == "firstname")
                {
                    exp = p => p.FirstName!.Contains(term);
                } 
                else if (searchColumn == "lastname")
                {
                    exp = p => p.LastName!.Contains(term);
                }

                query = query.Where(exp);
            }

            grandTotal = query.Count();

            IOrderedQueryable<Person> queryOrdered = null!;
            if (orderColumn == "firstname" && order == "asc")
                queryOrdered = query.OrderBy(p => p.FirstName);
            else if (orderColumn == "firstname" && order == "desc")
                queryOrdered = query.OrderByDescending(p => p.FirstName);
            else if (orderColumn == "lastname" && order == "asc")
                queryOrdered = query.OrderBy(p => p.LastName);
            else if (orderColumn == "lastname" && order == "desc")
                queryOrdered = query.OrderByDescending(p => p.LastName);    

            query = queryOrdered
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return query;
        }

        public async Task CreatePersonAsync(Person person)
        {
            await context.People.AddAsync(person);
        }

        public void DeletePerson(Person person)
        {
            context.People.Remove(person);
        }
    }
}
