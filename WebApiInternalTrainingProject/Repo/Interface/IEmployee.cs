using WebApiInternalTrainingProject.Data;
using WebApiInternalTrainingProject.Models;

namespace WebApiInternalTrainingProject.Repo.Interface
{
    public interface IEmployee
    {
        Task<List<EmployeeDTO>> GetEmployees();
        Task<bool> CreateEmployee(EmployeeModel emp);
        Task<bool> DeleteEmployee(int Id);
        Task<EmployeeDTO> GetEmployeeById(int Id);
        Task<bool> UpdateEmployeeById(int Id,EmployeeModel emp);
        Task<bool> CreateBulkEmployee(List<EmployeeModel> employees);
    }
}
