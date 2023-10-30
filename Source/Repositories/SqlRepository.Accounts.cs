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

        //public async Task<Account?> GetAccountAsync(string userId, int id)
        //{
        //    var parameters = new { UserId = userId, AccountId = id };
        //    var sql =
        //        @"
        //        SELECT a.AccountId as Id, a.* FROM Accounts a 
        //        WHERE a.UserId = @UserId AND a.AccountId = @AccountId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryFirstOrDefaultAsync<Account>(sql, parameters));
        //}

        //public async Task<bool> CreateAccountAsync(Account account)
        //{
        //    var sql =
        //        @"
        //        INSERT INTO Accounts (Name, Amount, Description, Owner, UserId)
        //        VALUES(@Name, @Amount, @Description, @Owner, @UserId)
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, account)) == 1;
        //}

        //public async Task<bool> UpdateAccountAsync(Account account)
        //{
        //    var sql =
        //        @"
        //        UPDATE Accounts 
        //        SET
        //            Name = @Name,
        //            Amount = @Amount,
        //            Description = @Description,
        //            Owner = @Owner
        //        WHERE AccountId = @Id AND UserId = @UserId
        //        ";
        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.ExecuteAsync(sql, account)) == 1;
        //}

        //public async Task<List<string>> GetAccountOwnersAsync(string userId)
        //{
        //    var parameters = new { UserId = userId };
        //    var sql =
        //        @"
        //        SELECT DISTINCT a.Owner FROM Accounts a
        //        WHERE a.UserId = @UserId
        //        ";

        //    using var connection = new SqlConnection(connectionString);
        //    return (await connection.QueryAsync<string>(sql, parameters)).ToList();
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
