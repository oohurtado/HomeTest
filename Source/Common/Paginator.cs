using System.Reflection;

namespace Home.Source.Common
{
    public class Paginator
    {
        public string OrderColumn { get; set; }
        public string Order { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Term { get; set; }
        public string SearchColumn { get; set; }

        public Paginator(string orderColumn, string order, int pageNumber, int pageSize, string term, string searchColumn)
        {
            OrderColumn = orderColumn;
            Order = order;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Term = term;
            SearchColumn = searchColumn;
        }

        public void Validate()
        {
            var attributes = GetAttributes();
            ValidateOrder(attributes);
            ValidateSearch(attributes);
            ValidatePage();      
        }  

        private void ValidateOrder(PageConfigurationAttribute attributes)
        {
            OrderColumn = OrderColumn.ToLower();
            Order = Order.ToLower();

            var orderColumns = attributes?.OrderColumns?.Split(',').ToList();
            if (!orderColumns?.Any(p => p == OrderColumn) ?? false)
            {
                throw new Exception($"SERVER_ERROR - OrderColumn is not found, expected columns: {string.Join(", ", orderColumns?.ToArray() ?? Array.Empty<string>())}");
            }
        }

        private void ValidateSearch(PageConfigurationAttribute attributes)
        {
            SearchColumn = SearchColumn?.ToLower() ?? string.Empty;

            var searchColumns = attributes?.SearchColumns?.Split(',').ToList();
            if (!string.IsNullOrEmpty(Term))
            {
                if (string.IsNullOrEmpty(SearchColumn))
                {
                    throw new Exception($"SERVER_ERROR - SearchColumn is empty, expected columns: {string.Join(", ", searchColumns?.ToArray() ?? Array.Empty<string>())}");
                }

                if (!searchColumns?.Any(p => p == SearchColumn) ?? false)
                {
                    throw new Exception($"SERVER_ERROR - SearchColumn is not found, expected columns: {string.Join(", ", searchColumns?.ToArray() ?? Array.Empty<string>())}");
                }
            }
        }

        private void ValidatePage()
        {
            if (PageNumber <= 0)
            {
                PageNumber = 1;
            }
        }

        private PageConfigurationAttribute GetAttributes()
        {
            var attributes = GetType().GetTypeInfo().GetCustomAttributes<PageConfigurationAttribute>();

            return new PageConfigurationAttribute()
            {
                OrderColumns = attributes.FirstOrDefault()?.OrderColumns!,
                SearchColumns = attributes.FirstOrDefault()?.SearchColumns!,
            };
        }
    }
}
