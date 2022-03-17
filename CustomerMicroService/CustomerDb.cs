using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService
{
    class CustomerDb : DbContext
    {
        public CustomerDb(DbContextOptions<CustomerDb> options) : base(options)
        {

        }
        public DbSet<CustomerDataModel> Customers => Set<CustomerDataModel>();
    }
}
