using Home.Source.Common;

namespace Home.Source.Factories
{
    public static class PaginatorFactory
    {
        public static Paginator CreatePage(PaginatorPage page, string orderColumn, string order, string term, string searchColumn)
        {
            return CreatePage(page, orderColumn, order, pageNumber: 0, pageSize: 0, term, searchColumn);
        }

        public static Paginator CreatePage(PaginatorPage page, string orderColumn, string order, int pageNumber, int pageSize, string term, string searchColumn)
        {
            Paginator? paginator;

            switch (page)
            {
                case PaginatorPage.People:
                    paginator = new PersonPage(orderColumn, order, pageNumber, pageSize, term, searchColumn);
                    break;
                case PaginatorPage.Events:
                    paginator = new EventPage(orderColumn, order, pageNumber, pageSize, term, searchColumn);
                    break;
                case PaginatorPage.Activities:
                    paginator = new ActivityPage(orderColumn, order, term, searchColumn);
                    break;
                default:
                    throw new Exception($"SERVER_ERROR - PaginatorFactory.CreatePage: No se encontro la página {page}");
            }

            paginator.Validate();

            return paginator;
        }

    }

    [PageConfiguration(OrderColumns = "firstname,lastname", SearchColumns = "firstname,lastname")]
    public class PersonPage : Paginator
    {
        public PersonPage(string orderColumn, string order, int pageNumber, int pageSize, string term, string searchColumn)
            : base(orderColumn, order, pageNumber, pageSize, term, searchColumn)
        {
        }
    }

    [PageConfiguration(OrderColumns = "date", SearchColumns = "description")]
    public class EventPage : Paginator
    {
        public EventPage(string orderColumn, string order, int pageNumber, int pageSize, string term, string searchColumn)
            : base(orderColumn, order, pageNumber, pageSize, term, searchColumn)
        {
        }
    }

    [PageConfiguration(OrderColumns = "date", SearchColumns = "description")]
    public class ActivityPage : Paginator
    {
        public ActivityPage(string orderColumn, string order, string term, string searchColumn)
            : base(orderColumn, order, pageNumber: 0, pageSize: 0, term, searchColumn)
        {
        }
    }
}
