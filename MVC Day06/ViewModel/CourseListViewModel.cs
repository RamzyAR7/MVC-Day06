using MVC_Day06.Models;

namespace MVC_Day06.ViewModel
{
    public class CourseListViewModel
    {
        public List<Course> Courses { get; set; }
        public string SearchString { get; set; }
        public int? DepartmentId { get; set; }
    }
}
