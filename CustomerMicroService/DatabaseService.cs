using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroService
{
    public class DatabaseService
    {
        private static readonly string connectionString = "Server=database-1.cn75wgbk3h1b.us-east-1.rds.amazonaws.com; port=5432; user Id = postgres; password=admin123;";

        public async Task<CustomerDataModel> GetCustomerById(int id)
        {
            var customer = new CustomerDataModel();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                NpgsqlCommand npgsqlCommand = conn.CreateCommand();
                npgsqlCommand.CommandText = "Select * from \"Customer\" where id = @id";
                npgsqlCommand.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                using (NpgsqlDataReader npgsqlReader = await npgsqlCommand.ExecuteReaderAsync())
                {
                    while (npgsqlReader.Read())
                    {
                        customer.Id = (int)npgsqlReader["Id"];
                        customer.Name = (string)npgsqlReader["Name"];
                    }
                }
                conn.Close();
                conn.Dispose();
            }
            return customer;

        } 
    }
}
