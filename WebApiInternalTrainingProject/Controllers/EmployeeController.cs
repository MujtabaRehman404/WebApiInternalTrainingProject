using CsvHelper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebApiInternalTrainingProject.Data;
using WebApiInternalTrainingProject.Models;
using WebApiInternalTrainingProject.Repo.Interface;
using WebApiInternalTrainingProject.Repo.Service;

namespace WebApiInternalTrainingProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _emp;
        private readonly AppDbContext _dbContext;
        public EmployeeController(IEmployee emp, AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

        [HttpGet("Employee/GetEmployeesById/{id}")]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            var result = await _emp.GetEmployeeById(id);
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

            return StatusCode(500, "Employee not found!");
        }


        [HttpPost("Employee/UpdateBulkEmployeeCsv")]
        public async Task<IActionResult> CreateBulkEmployee(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<EmployeeCsvDTO>().ToList();

            foreach (var record in records)
            {
                var currentEmployee = new EmployeeModel();
                currentEmployee.name = record.Name;
                currentEmployee.age = record.Age;
                currentEmployee.martialStatus = record.martialStatus;
                currentEmployee.DepartmentId = record.Department;

                _dbContext.Employees.Add(currentEmployee);
                await _dbContext.SaveChangesAsync();
            }

            return Ok("CSV data inserted successfully.");

        }



        //[HttpGet("Employee/hitApi")]
        //public async Task<IActionResult> hitApi()
        //{
        //    await Task.Delay(2000);
        //    return Ok("api is working");
        //}

        [HttpGet("Employee/hitApi")]
        public void hitApi()
        {
            Thread.Sleep(4000);
        }

    }
}
