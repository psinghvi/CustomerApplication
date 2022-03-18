using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
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

        public async Task<IEnumerable<CustomerDataModel>> GetAllCustomers()
        {
            var customers = new List<CustomerDataModel>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                NpgsqlCommand npgsqlCommand = conn.CreateCommand();
                npgsqlCommand.CommandText = "Select * from \"Customer\"";
                using (NpgsqlDataReader npgsqlReader = await npgsqlCommand.ExecuteReaderAsync())
                {
                    while (npgsqlReader.Read())
                    {
                        var customer = new CustomerDataModel();
                        customer.Id = (int)npgsqlReader["Id"];
                        customer.Name = (string)npgsqlReader["Name"];
                        customers.Add(customer);
                    }
                }
                conn.Close();
                conn.Dispose();
            }
            return customers;

        }

        public async Task<CustomerDataModel> AddCustomer(CustomerDataModel customer)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                NpgsqlCommand npgsqlCommand = conn.CreateCommand();
                npgsqlCommand.CommandText = "INSERT INTO \"Customer\" (id, name) VALUES (@id, @name);";
                npgsqlCommand.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = customer.Id;
                npgsqlCommand.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = customer.Name;
                await npgsqlCommand.ExecuteNonQueryAsync();
                conn.Close();
                conn.Dispose();
            }
            return customer;

        }

        public async Task<CustomerDataModel> UpdateCustomer(CustomerDataModel customer)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                NpgsqlCommand npgsqlCommand = conn.CreateCommand();
                npgsqlCommand.CommandText = "UPDATE \"Customer\" SET id =@id, name =@name;";
                npgsqlCommand.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = customer.Id;
                npgsqlCommand.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = customer.Name;
                await npgsqlCommand.ExecuteNonQueryAsync();
                conn.Close();
                conn.Dispose();
            }
            return customer;

        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var rowsAffected = 0;
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                NpgsqlCommand npgsqlCommand = conn.CreateCommand();
                npgsqlCommand.CommandText = "DELETE FROM \"Customer\" WHERE id =@id;";
                npgsqlCommand.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                rowsAffected = await npgsqlCommand.ExecuteNonQueryAsync();         
                conn.Close();
                conn.Dispose();
            }
            return rowsAffected > 0;

        }
    }
}
