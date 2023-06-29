using Dapper;
using Npgsql;

namespace Estore.DAL
{
	public class DbHelper
	{
		public const string ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5444;Database=test";
		
		public static async Task ExecuteAsync(string sql, object model)
		{
			using (var connection = new NpgsqlConnection(ConnString))
			{
				await connection.OpenAsync();
				await connection.ExecuteAsync(sql, model);
			}
		}

		public static async Task<T?> QueryScalarAsync<T>(string sql, object model)
		{
			using (var connection = new NpgsqlConnection(ConnString))
			{
				await connection.OpenAsync();
				return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
			}
		}

		public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
		{
			using (var connection = new NpgsqlConnection(ConnString))
			{
				await connection.OpenAsync();
				return await connection.QueryAsync<T>(sql, model);
			}
		}
	}
}
