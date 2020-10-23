using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using file_uploader.Models;
using System.Linq;

namespace file_uploader.DAL
{
    public class PaymentsModelRepository : IPaymentsModelRepository
    {
        private const string connectionString = "Data Source=localhost;Initial Catalog=Rabota;Integrated Security=True";

        public async Task<int> insertAsync(List<PaymentsModel> paymentsModelsList)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                var sql = @"USE [Rabota]     
                            INSERT INTO [dbo].[RABOTA]
                               ([Number],
                                [Summ],
                                [Date])
                            VALUES
                                (@Number, 
                                @Summ, 
                                @Date)";

                var affectedRows = await connection.ExecuteAsync(sql, paymentsModelsList)
                    .ConfigureAwait(false);

                return affectedRows;
            }
        }

        public async Task<PaymentsModel> GetByNumberAsync(int number)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var sql = @" SELECT [Number],
                                    [Summ], 
                                    [Date]
                             FROM [Rabota].[dbo].[RABOTA]
                             WHERE [Number] = @Number";
                var result = await connection.QueryAsync<PaymentsModel>(sql, new {number})
                    .ConfigureAwait(false);

                return result.FirstOrDefault();
            }
        }
    }
}