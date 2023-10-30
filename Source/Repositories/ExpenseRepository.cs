using Home.Models.Entities.Money;
using Home.Source.Common;
using Home.Source.Database;

namespace Home.Source.Repositories
{
    public partial class ExpenseRepository : BaseRepository
    {
        public ExpenseRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
