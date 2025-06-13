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

            return StatusCode(500,"employee not found!");
        }

    }
}
