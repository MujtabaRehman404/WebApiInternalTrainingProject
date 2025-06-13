using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiInternalTrainingProject.Models;
using WebApiInternalTrainingProject.Repo.Interface;
using WebApiInternalTrainingProject.Repo.Service;

namespace WebApiInternalTrainingProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _emp;
        public EmployeeController(IEmployee emp)
        {
            _emp = emp;
        }

        [HttpGet("Employee/GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _emp.GetEmployees();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Employee/CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeModel newemp)
        {
            var result = await _emp.CreateEmployee(newemp);
            if (result)
            {
                return Ok("Employee Created Successfully");
            }

            return StatusCode(500,"Issue Occured");
        }

        [HttpDelete("Employee/DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _emp.DeleteEmployee(id);
            if (result)
            {
                return Ok("deleted Successfully!");
            }

            return StatusCode(500, "employee not found!");
        }

        [HttpPut("Employee/UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeModel targetEmp)
        {
            var result = await _emp.UpdateEmployeeById(id,targetEmp);
            if (result)
            {
                return Ok("updated Successfully!");
            }

            return StatusCode(500, "employee not found!");
        }


        [HttpPost("Employee/CreateBulkEmployee")]
        public async Task<IActionResult> CreateBulkEmployee([FromBody] List<EmployeeModel> newemp)
        {
            var result = await _emp.CreateBulkEmployee(newemp);
            if (result)
            {
                return Ok("Employees Created Successfully");
            }

            return StatusCode(500, "Issue Occured");
        }

    }
}
