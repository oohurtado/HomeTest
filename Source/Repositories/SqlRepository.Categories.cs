using Dapper;
using Home.Models.Entities.Money;
using Microsoft.Data.SqlClient;

namespace Home.Source.Repositories
{
    public partial class SqlRepository
    {
        //public async Task<List<Category>> GetCategoriesAsync(string userId, int superCategoryId)
        //{
        //    var parameters = new { UserId = userId, SuperCategoryId = superCategoryId };
        //    var sql =
        //        @"
        //        SELECT c.CategoryId as Id, c.* FROM SuperCategories sc
        //        JOIN Categories c ON sc.SuperCategoryId = c.SuperCategoryId
        //        WHERE sc.UserId = @UserId AND sc.SuperCategoryId = @SuperCategoryId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryAsync<Category>(sql, parameters)).ToList();
        //}

        //public async Task<Category?> GetCategoryAsync(string userId, int id)
        //{
        //    var parameters = new { UserId = userId, CategoryId = id };
        //    var sql =
        //        @"
        //        SELECT c.CategoryId as Id, c.* FROM SuperCategories sc
        //        JOIN Categories c ON sc.SuperCategoryId = c.SuperCategoryId
        //        WHERE sc.UserId = @UserId AND c.CategoryId = @CategoryId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryFirstOrDefaultAsync<Category>(sql, parameters));
        //}

        //public async Task<bool> CreateCategoryAsync(Category category, string userId)
        //{
        //    var parameters = new { UserId = userId, SuperCategoryId = category.SuperCategoryId, Budget = category.Budget, Name = category.Name };
        //    var sql =
        //        @"
        //        IF EXISTS 
        //        (
        //            SELECT TOP 1 sc.SuperCategoryId 
        //            FROM SuperCategories sc
        //            WHERE sc.SuperCategoryId = @SuperCategoryId AND sc.UserId = @UserId
        //        )
        //        BEGIN
        //            INSERT INTO Categories (Name, Budget, SuperCategoryId)
        //            VALUES(@Name, @Budget, @SuperCategoryId)
        //        END
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, parameters)) == 1;
        //}

        //public async Task<bool> UpdateCategoryAsync(Category category, string userId)
        //{
        //    var parameters = new { UserId = userId, CategoryId = category.Id, SuperCategoryId = category.SuperCategoryId, Budget = category.Budget, Name = category.Name };
        //    var sql =
        //        @"
        //        IF EXISTS
        //        (
        //            SELECT TOP 1 sc.SuperCategoryId
        //            FROM SuperCategories sc
        //            WHERE sc.SuperCategoryId = @SuperCategoryId AND sc.UserId = @UserId
        //        )
        //        BEGIN
        //            UPDATE Categories
        //            SET
        //                SuperCategoryId = @SuperCategoryId, 
        //                Budget = @Budget, 
        //                Name = @Name
        //            WHERE CategoryId = @CategoryId
        //        END                   
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, parameters)) == 1;
        //}

        //public async Task<bool> DeleteCategoryAsync(string userId, int superCategoryId, int id)
        //{
        //    var parameters = new { UserId = userId, SuperCategoryId = superCategoryId, CategoryId = id };
        //    var sql =
        //        @"
        //        IF EXISTS
        //        (
        //            SELECT TOP 1 sc.SuperCategoryId
        //            FROM SuperCategories sc
        //            WHERE sc.SuperCategoryId = @SuperCategoryId AND sc.UserId = @UserId
        //        )
        //        BEGIN
        //            DELETE FROM Categories 
        //            WHERE SuperCategoryId = @SuperCategoryId AND CategoryId = @CategoryId
        //        END
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, parameters)) == 1;
        //}
    }
}
