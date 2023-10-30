using Home.Models.DTOs;

namespace Home.Source.BusinessLayer
{
    public partial class ExpensesLayer
    {
        public async Task<List<SuperCategoryDTO>?> GetExpenses_ByYearMonth_Async(string userId, int year, int month, bool isRegular)
        {
            var data = await sqlRepository.GetExpenses_ByYearMonth_Async(userId, year, month, isRegular);

            var tmp = from d in data
                      group d by new { d.SuperCategoryId, d.SuperCategoryName } into g1
                      orderby g1.Key.SuperCategoryName
                      select new SuperCategoryDTO()
                      {
                          Id = g1.Key.SuperCategoryId,
                          Name = g1.Key.SuperCategoryName,
                          Total = g1.Sum(p => p.EntryAmount ?? 0),
                          Budget = g1.Sum(p => p.CategoryBudget ?? 0),
                          IsRegular = g1.Max(x => x.SuperCategoryIsRegular ?? false),
                          IsService = g1.Max(x => x.SuperCategoryIsService ?? false),
                          Categories = (from dd in g1.Where(x => x.CategoryId != null)
                                        group dd by new { dd.CategoryId, dd.CategoryName } into g2
                                        select new CategoryDTO()
                                        {
                                            Id = g2.Key.CategoryId,
                                            Name = g2.Key.CategoryName,
                                            Total = g2.Sum(q => q.EntryAmount ?? 0),
                                            Budget = g2.Max(x => x.CategoryBudget ?? 0),
                                            SuperCategoryId = g2.Max(x => x.SuperCategoryId),
                                            Entries = (from ddd in g2.Where(x => x.EntryId != null)
                                                       select new EntryDTO()
                                                       {
                                                           Id = ddd.EntryId,
                                                           Amount = ddd.EntryAmount ?? 0,
                                                           Description = ddd.EntryDescription!,
                                                           Date = ddd.EntryDate,
                                                           AccountId = ddd.EntryAccountId,
                                                           CategoryId = ddd.CategoryId,                                                           
                                                       }).ToList()
                                        }).ToList()
                      };

            var list = tmp.ToList();
            return list;
        }        
    }
}
