using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiInternalTrainingProject.Data;
using WebApiInternalTrainingProject.Models;
using WebApiInternalTrainingProject.Repo.Interface;

namespace WebApiInternalTrainingProject.Repo.Service
{
    public class EmployeeService : IEmployee
    {
        private readonly AppDbContext _dbContext;

        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateEmployee(EmployeeModel emp)
        {
            EmployeeModel newemp = new EmployeeModel();
            newemp.name = emp.name;
            newemp.DepartmentId = emp.DepartmentId;
            newemp.yearsOfExperience = emp.yearsOfExperience;
            newemp.martialStatus = emp.martialStatus;
            newemp.age = emp.age;

            await _dbContext.Employees.AddAsync(newemp);
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> DeleteEmployee(int Id)
        {
            var result = await _dbContext.Employees.FindAsync(Id);
            if (result != null)
            {
                _dbContext.Employees.Remove(result);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
            
        }

        public async Task<EmployeeDTO> GetEmployeeById(int Id)
        {
            var targetEmp = await _dbContext.Employees.Where(e => e.Id == Id).Select(e => new EmployeeDTO
            {
                Id = e.Id,
                name = e.name,
                age = e.age,
                martialStatus = e.martialStatus,
                yearsOfExperience = e.yearsOfExperience,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department.Name
            }).FirstOrDefaultAsync();

            if (targetEmp != null)
            {
                return targetEmp;
            }

            return null;
        }

        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            return await _dbContext.Employees
        .Select(e => new EmployeeDTO
        {
            Id = e.Id,
            name = e.name,
            age = e.age,
            martialStatus = e.martialStatus,
            yearsOfExperience = e.yearsOfExperience,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department.Name
        })
        .ToListAsync();
        }

        public async Task<bool> UpdateEmployeeById(int Id, EmployeeModel emp)
        {
            var result = await GetEmployeeById(Id);
            if (result != null)
            {
                result.name = emp.name;
                result.DepartmentId = emp.DepartmentId;
                result.yearsOfExperience = emp.yearsOfExperience;
                result.martialStatus = emp.martialStatus;
                result.age = emp.age;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CreateBulkEmployee(List<EmployeeModel> employees)
        {
            if (employees == null || employees.Count() == 0)
            {
                return false;
            }
            await _dbContext.Employees.AddRangeAsync(employees);
            var rowsaffected = await _dbContext.SaveChangesAsync();

            if(rowsaffected > 0)
            {
                return true;
            }
            return false;
        }

    }
}
