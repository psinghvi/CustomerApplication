using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<CustomerDataModel> GetCustomerDataModelAsync(int id)
        {
            DatabaseService databaseService = new DatabaseService();
            return await databaseService.GetCustomerById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDataModel>> GetAllCustomersAsync()
        {
            DatabaseService databaseService = new DatabaseService();
            return await databaseService.GetAllCustomers();
        }

        [HttpPut]
        public async Task<CustomerDataModel> AddCustomerAsync(CustomerDataModel customer)
        {
            DatabaseService databaseService = new DatabaseService();

            return await databaseService.AddCustomer(customer);
        }

        [HttpPost]
        public async Task<CustomerDataModel> UpdateCustomerAsync(CustomerDataModel customer)
        {
            DatabaseService databaseService = new DatabaseService();

            return await databaseService.UpdateCustomer(customer);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            DatabaseService databaseService = new DatabaseService();

            return await databaseService.DeleteCustomer(id);
        }
    }
}
