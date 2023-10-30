using Dapper;
using Home.Models.Entities.Money;
using Microsoft.Data.SqlClient;

namespace Home.Source.Repositories
{
    public partial class SqlRepository
    {
        //public async Task<List<SuperCategory>> GetSuperCategoriesAsync(string userId)
        //{
        //    var parameters = new { UserId = userId };
        //    var sql =
        //        @"
        //            SELECT sc.SuperCategoryId as Id, sc.* FROM SuperCategories sc 
        //            WHERE sc.UserId = @UserId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryAsync<SuperCategory>(sql, parameters)).ToList();
        //}

        //public async Task<SuperCategory?> GetSuperCategoryAsync(string userId, int id)
        //{
        //    var parameters = new { UserId = userId, SuperCategoryId = id };
        //    var sql =
        //        @"
        //        SELECT sc.SuperCategoryId as Id, sc.* FROM SuperCategories sc 
        //        WHERE sc.UserId = @UserId AND sc.SuperCategoryId = @SuperCategoryId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryFirstOrDefaultAsync<SuperCategory>(sql, parameters));
        //}

        //public async Task<bool> CreateSuperCategoryAsync(SuperCategory superCategory)
        //{
        //    var sql =
        //        @"
        //        INSERT INTO SuperCategories (Name, IsRegular, IsService, UserId)
        //        VALUES(@Name, @IsRegular, @IsService, @UserId)
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, superCategory)) == 1;
        //}

        //public async Task<bool> UpdateSuperCategoryAsync(SuperCategory superCategory)
        //{
        //    var sql =
        //        @"
        //        UPDATE SuperCategories 
        //        SET
        //            Name = @Name,
        //            IsRegular = @IsRegular,
        //            IsService = @IsService
        //        WHERE SuperCategoryId = @Id AND UserId = @UserId
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, superCategory)) == 1;
        //}

        //public async Task<bool> DeleteSuperCategoryAsync(string userId, int id)
        //{
        //    var parameters = new { UserId = userId, SuperCategoryId = id };
        //    var sql =
        //         @"
        //        DELETE FROM SuperCategories 
        //        WHERE SuperCategoryId = @SuperCategoryId AND UserId = @UserId
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, parameters)) == 1;
        //}
    }
}
