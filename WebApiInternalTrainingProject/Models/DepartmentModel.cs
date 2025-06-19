using System.ComponentModel.DataAnnotations;

namespace WebApiInternalTrainingProject.Models
{
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
