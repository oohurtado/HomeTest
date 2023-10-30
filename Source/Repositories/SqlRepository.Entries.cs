using Dapper;
using Home.Models.Entities.Money;
using Microsoft.Data.SqlClient;

namespace Home.Source.Repositories
{
    public partial class SqlRepository
    {
        //public async Task<List<Account>> GetAccountsAsync(string userId)
        //{
        //    var parameters = new { UserId = userId };
        //    var sql =
        //        @"
        //            SELECT a.AccountId as Id, a.* FROM Accounts a
        //            WHERE a.UserId = @UserId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryAsync<Account>(sql, parameters)).ToList();
        //}

        //public async Task<Entry?> GetEntryAsync(string userId, int id)
        //{
        //    var parameters = new { UserId = userId, EntryId = id };
        //    var sql =
        //        @"
        //        SELECT e.EntryId as Id, e.* FROM Entries e
        //        WHERE e.UserId = @UserId AND e.EntryId = @EntryId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryFirstOrDefaultAsync<Entry>(sql, parameters));
        //}

        //public async Task<bool> CreateEntryAsync(Entry entry)
        //{
        //    var sql =
        //        @"
        //        INSERT INTO Entries (Amount, Date, Description, UserId, CategoryId, AccountId)
        //        VALUES(@Amount, @Date, @Description, @UserId, @CategoryId, @AccountId)
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, entry)) == 1;
        //}

        //public async Task<bool> UpdateEntryAsync(Entry entry)
        //{
        //    var sql =
        //        @"
        //        UPDATE Entry 
        //        SET
        //            Amount = @Amount, 
        //            Date = @Date, 
        //            Description = @Description,
        //            CategoryId = @CategoryId, 
        //            AccountId = @AccountId
        //        WHERE EntryId = @Id AND UserId = @UserId
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, entry)) == 1;
        //}

        //public async Task<bool> DeleteAccountAsync(string userId, int id)
        //{
        //    var parameters = new { UserId = userId, AccountId = id };
        //    var sql =
        //         @"
        //        DELETE FROM Accounts 
        //        WHERE AccountId = @AccountId AND UserId = @UserId
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, parameters)) == 1;
        //}
    }
}
