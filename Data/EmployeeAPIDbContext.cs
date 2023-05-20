using Microsoft.EntityFrameworkCore;
using Restful.Models;

namespace Restful.Data
{
    public class EmployeeAPIDbContext : DbContext
    {
        public EmployeeAPIDbContext(DbContextOptions<EmployeeAPIDbContext> _option):base (_option) { }

        public DbSet<EmployeeModel> Employee { get; set; }
    }
}
