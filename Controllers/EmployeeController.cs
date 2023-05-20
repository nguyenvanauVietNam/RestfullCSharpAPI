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
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeAPIDbContext _dbContext;

        public EmployeeController(EmployeeAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //View List user
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            return Ok(await _dbContext.Employee.ToListAsync());
        }

        //Create Employee
        [HttpPost]
        public async Task<ActionResult<List<EmployeeModel>>> CreateEmployee(EmployeeModel employee)
        {

            var EmployeeInDb = _dbContext.Employee.Count();

            //Check Employee of database
            if (EmployeeInDb == 0)
            {
                employee.Code = "00001-Emp";
            }
            else 
            {
                //Code is null or blank
                if (employee.Code == "" || employee.Code == null)
                {
                    //search list employee
                    var listEmployeeCodePre = _dbContext.Employee.ToList();

                    //get lastest Employee
                    var EmployeeCodePre = listEmployeeCodePre[EmployeeInDb - 1];

                    if (EmployeeCodePre != null)
                    {
                        //get Code employee before
                        string employeCodeTemp = EmployeeCodePre.Code;

                        if(employeCodeTemp == string.Empty)
                        {
                            return NotFound(await _dbContext.Employee.ToListAsync());
                        }

                        //separate numbers and letters
                        string[] tempCode = employeCodeTemp.Split('-');
                        string formatNumber = "";
                        string formatEmp = "";
                        int possitionEmp = 0;

                        foreach (var word in tempCode)
                        {
                            if(word == "Emp")
                            {
                                formatEmp = word;
                            }
                            else
                            {
                                formatNumber = word;
                            }
                        }

                        //check possition Emp
                        if(formatEmp == tempCode[0])
                        {
                            possitionEmp = 0;
                        }
                        else
                        {
                            possitionEmp = 1;
                        }
                        //Check lengh Number
                        int lengtNumber = formatNumber.Length;

                        //generate code according to the previous format
                        formatNumber = (EmployeeInDb + 1).ToString();
                        if (possitionEmp ==0)
                        {
                            employee.Code = formatEmp + '-' + formatNumber.PadLeft(lengtNumber, '0');
                        }
                        else
                        {
                            employee.Code = formatNumber.PadLeft(lengtNumber, '0') + '-' + formatEmp;
                        }
                    }

                }

            }

            _dbContext.Employee.Add(employee);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Employee.ToListAsync());
        }

        //Update Employee
        [HttpPut]
        public async Task<ActionResult<List<EmployeeModel>>> UpdateEmployee(EmployeeModel updateEmployee)
        {
            if(updateEmployee != null)
            {
                var employee = await _dbContext.Employee.FirstOrDefaultAsync(e =>e.Id == updateEmployee.Id);
                employee!.Name = updateEmployee.Name;
                employee!.Code = updateEmployee.Code;
                employee.Phone = updateEmployee.Phone;
                employee!.Email = updateEmployee.Email;
                employee!.Sex= updateEmployee.Sex;
                employee!.Avatar = updateEmployee.Avatar;
                await _dbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return BadRequest();
        }

        //Deleted Employee
        [HttpDelete]
        public async Task<ActionResult<List<EmployeeModel>>> DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employee.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                _dbContext.Employee.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.Employee.ToListAsync());
            }
            return BadRequest();
        }
    }
}
