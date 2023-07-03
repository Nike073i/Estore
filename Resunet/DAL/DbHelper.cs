using Dapper;
using Npgsql;
using System.Diagnostics;
using System.Text.Json;

namespace Estore.DAL
{

    public class DbHelper : IDbHelper
    {
        public static string ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5444;Database=test";
        private readonly IDalMetric _dalMetric;

        public DbHelper(IDalMetric dalMetric)
        {
            _dalMetric = dalMetric;
        }

        public async Task ExecuteAsync(string sql, object model)
        {
            var timer = new Stopwatch();
            timer.Start();
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
            timer.Stop();
            _dalMetric.Add(new()
            {
                Elapsed = timer.Elapsed,
                Parameters = JsonSerializer.Serialize(model),
                Sql = sql
            });
        }

        public async Task<T?> QueryScalarAsync<T>(string sql, object model)
        {
            var timer = new Stopwatch();
            timer.Start();
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();
                var item = await connection.QueryFirstOrDefaultAsync<T>(sql, model);
                timer.Stop();
                _dalMetric.Add(new()
                {
                    Elapsed = timer.Elapsed,
                    Parameters = JsonSerializer.Serialize(model),
                    Sql = sql
                });
                return item;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            var timer = new Stopwatch();
            timer.Start();
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();
                var items = await connection.QueryAsync<T>(sql, model);
                timer.Stop();
                _dalMetric.Add(new()
                {
                    Elapsed = timer.Elapsed,
                    Parameters = JsonSerializer.Serialize(model),
                    Sql = sql
                });
                return items;
            }
        }
    }
}
