using WebApiInternalTrainingProject.Models;

namespace WebApiInternalTrainingProject.Repo.Interface
{
    public interface IEmployee
    {
        Task<List<EmployeeModel>> GetEmployees();
        Task<bool> CreateEmployee(EmployeeModel emp);
        Task<bool> DeleteEmployee(int Id);
        Task<EmployeeModel> GetEmployeeById(int Id);
           
    }
}
