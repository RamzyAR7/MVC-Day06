using System.ComponentModel.DataAnnotations;

namespace MVC_Day06.ViewModel
{
    public class StudentViewModel
    {
        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain alphabets")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(15, 60, ErrorMessage = "Age must be between 15 and 60")]
        public int Age { get; set; }

        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Department is required")]
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }

        [Display(Name = "Course")]
        [Range(1, int.MaxValue, ErrorMessage = "Course is required")]
        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }

        [Display(Name = "Degree")]
        [Required(ErrorMessage = "Degree is required")]
        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100")]
        public int Degree { get; set; }
    }
}
