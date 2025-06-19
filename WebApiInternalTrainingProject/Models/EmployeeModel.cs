using System.ComponentModel.DataAnnotations;

namespace WebApiInternalTrainingProject.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string martialStatus { get; set; }
        public int yearsOfExperience { get; set; }
        public int DepartmentId{ get; set; }
        public DepartmentModel Department { get; set; }
    }
}
