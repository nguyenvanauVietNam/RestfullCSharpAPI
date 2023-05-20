using Microsoft.EntityFrameworkCore;
using Restful.Models;

namespace Restful.Data
{
    public class DepartmentEmployeeAPIDbContext : DbContext
    {
        public DepartmentEmployeeAPIDbContext(DbContextOptions<DepartmentEmployeeAPIDbContext> _option) : base(_option) { }

        public DbSet<Department_EmployeeModel> DepartmentEmployee { get; set; }
    }
}
