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
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentAPIDbContext _dbContext;

        public DepartmentController(DepartmentAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //View List Department
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            return Ok(await _dbContext.Department.ToListAsync());
        }

        //Create Department
        [HttpPost]
        public async Task<ActionResult<List<DepartmentModel>>> CreateDepartment(DepartmentModel addDepartment)
        {
            if(addDepartment == null)
            {
                return BadRequest();
            }
            _dbContext.Department.Add(addDepartment);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Department.ToListAsync());
        }

        //Update Department
        [HttpPut]
        public async Task<ActionResult<List<DepartmentModel>>> UpdateDepartment(DepartmentModel updateDepartment)
        {
            if (updateDepartment != null)
            {
                var department = await _dbContext.Department.FirstOrDefaultAsync(e => e.Id == updateDepartment.Id);
                department!.Name = updateDepartment.Name;
                department!.Code= updateDepartment.Code;
                await _dbContext.SaveChangesAsync();
                return Ok(department);
            }
            return BadRequest();
        }

        //Delete Department
        [HttpDelete]
        public async Task<ActionResult<List<DepartmentModel>>> DeleteDepartment(int id)
        {
            var department = await _dbContext.Department.FirstOrDefaultAsync(e => e.Id == id);
            if (department != null)
            {
                _dbContext.Department.Remove(department);
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.Department.ToListAsync());
            }
            return BadRequest();
        }
    }
}
