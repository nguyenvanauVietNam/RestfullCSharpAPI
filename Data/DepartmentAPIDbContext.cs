using Microsoft.EntityFrameworkCore;
using Restful.Models;

namespace Restful.Data
{
    public class DepartmentAPIDbContext : DbContext
    {
        public DepartmentAPIDbContext(DbContextOptions<DepartmentAPIDbContext> _option) : base(_option) { }

        public DbSet<DepartmentModel> Department { get; set; }
    }
}
