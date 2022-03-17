using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("Getme")]
        public async Task<CustomerDataModel> GetCustomerDataModelAsync(int id)
        {
            DatabaseService databaseService = new DatabaseService();
            return await databaseService.GetCustomerById(id);
        }
    }
}
