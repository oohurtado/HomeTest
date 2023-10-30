using Dapper;
using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Microsoft.Data.SqlClient;

namespace Home.Source.Repositories
{
    public partial class SqlRepository
    {    
        public async Task<List<Expenses>?> GetExpenses_ByYearMonth_Async(string userId, int year, int month, bool isRegular)
        {		
            var parameters = new { UserId = userId, Month = month, Year = year, IsRegular = isRegular };
            string sql;

            if (isRegular)
            {
                sql = GetSql1();
            }
            else
            {
                sql = GetSql2();
            }

            using var connection = new SqlConnection(connectionString);
            var data = (await connection.QueryAsync<Expenses>(sql, parameters)).ToList();
			return data;
        }

        private string GetSql1()
        {
            var sql = @"
                SELECT *
                FROM
                    (SELECT 
                        s.SuperCategoryId,
                        s.Name AS SuperCategoryName,
                        s.IsRegular AS SuperCategoryIsRegular,
                        s.IsService AS SuperCategoryIsService,
                        c.CategoryId,
                        c.Name AS CategoryName,
                        c.Budget AS CategoryBudget
                    FROM SuperCategories s
                    LEFT JOIN Categories c ON s.SuperCategoryId = c.SuperCategoryId
                    WHERE 
                        s.UserId = @UserId 
                        AND s.IsRegular = 1) 
                    AS r1
                FULL OUTER JOIN
                    (SELECT e.EntryId,
                        e.Amount AS EntryAmount,
                        e.Date AS EntryDate,
                        e.Description AS EntryDescription,
                        e.CategoryId AS EntryCategoryId,
                        e.AccountId AS EntryAccountId
                    FROM Entries e
                    WHERE 
                        DATEPART(MONTH, e.Date) = @Month
                        AND DATEPART(YEAR, e.Date) = @Year
                        AND e.UserId = @UserId) 
                    AS r2 
                ON r1.CategoryId = r2.EntryCategoryId 
                WHERE SuperCategoryId is not null
                ";
            return sql;
        }

        private string GetSql2()
        {
            var sql = @"
                SELECT e.EntryId,
                    e.Amount AS EntryAmount,
                    e.Date AS EntryDate,
                    e.Description AS EntryDescription,
                    e.CategoryId AS EntryCategoryId,
                    e.AccountId AS EntryAccountId,
                    s.SuperCategoryId,
                    s.Name AS SuperCategoryName,
                    s.IsRegular AS SuperCategoryIsRegular,
                    s.IsService AS SuperCategoryIsService,
                    c.CategoryId,
                    c.Name AS CategoryName,
                    c.Budget AS CategoryBudget
                FROM Entries e
                INNER JOIN Categories c ON e.CategoryId = c.CategoryId
                INNER JOIN SuperCategories s ON c.SuperCategoryId = s.SuperCategoryId
                WHERE 
                    DATEPART(MONTH, e.Date) = @Month
                    AND DATEPART(YEAR, e.Date) = @Year
                    AND s.IsRegular = 0
                    AND e.UserId = @UserId
                ";
            return sql;
        }
    }
}
