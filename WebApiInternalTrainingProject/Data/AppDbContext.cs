using Microsoft.EntityFrameworkCore;
using WebApiInternalTrainingProject.Models;

namespace WebApiInternalTrainingProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        public DbSet<EmployeeModel> Employees {  get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }


    }
}
