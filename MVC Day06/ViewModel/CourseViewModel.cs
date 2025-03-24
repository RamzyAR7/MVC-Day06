using System.ComponentModel.DataAnnotations;

namespace MVC_Day06.ViewModel
{
    public class CourseViewModel
    {
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(130, ErrorMessage = "Name cannot be longer than 130 characters")]
        public string Name { get; set; }

        [Display(Name ="Max Degree")]
        [Required(ErrorMessage = "Degree is required")]
        [StringLength(50, ErrorMessage = "Degree cannot be longer than 50 characters")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Minimum Degree is required")]
        [StringLength(50, ErrorMessage = "Minimum Degree cannot be longer than 50 characters")]
        public string MinDegree { get; set; }

        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Department is required")]
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
    }
}
