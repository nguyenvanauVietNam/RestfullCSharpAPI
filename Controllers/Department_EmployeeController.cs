using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful.Models;
using Restful.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Restful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Department_EmployeeController : ControllerBase
    {
        private readonly DepartmentEmployeeAPIDbContext _dbContext;

        public Department_EmployeeController(DepartmentEmployeeAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //View List DepartmentEmployee
        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentEmployee_Employee()
        {
            return Ok(await _dbContext.DepartmentEmployee.ToListAsync());
        }

        //Create DepartmentEmployee
        [HttpPost]
        public async Task<ActionResult<List<Department_EmployeeModel>>> CreateDepartmentEmployee(Department_EmployeeModel addDepartmentEmployee)
        {
            if(addDepartmentEmployee == null)
            {
                return BadRequest();
            }
            _dbContext.DepartmentEmployee.Add(addDepartmentEmployee);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.DepartmentEmployee.ToListAsync());
        }

        //Update DepartmentEmployee
        [HttpPut]
        public async Task<ActionResult<List<Department_EmployeeModel>>> UpdateDepartmentEmployee(Department_EmployeeModel updateDepartmentEmployee)
        {
            if (updateDepartmentEmployee != null)
            {
                var DepartmentEmployee = await _dbContext.DepartmentEmployee.FirstOrDefaultAsync(e => e.Id == updateDepartmentEmployee.Id);
                DepartmentEmployee!.Employee_Id = updateDepartmentEmployee.Employee_Id;
                DepartmentEmployee!.Department_Id = updateDepartmentEmployee.Department_Id;
                await _dbContext.SaveChangesAsync();
                return Ok(DepartmentEmployee);
            }
            return BadRequest();
        }

        //Delete DepartmentEmployee
        [HttpDelete]
        public async Task<ActionResult<List<Department_EmployeeModel>>> DeleteDepartmentEmployee(int id)
        {
            var DepartmentEmployee = await _dbContext.DepartmentEmployee.FirstOrDefaultAsync(e => e.Id == id);
            if (DepartmentEmployee != null)
            {
                _dbContext.DepartmentEmployee.Remove(DepartmentEmployee);
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.DepartmentEmployee.ToListAsync());
            }
            return BadRequest();
        }
    }
}
