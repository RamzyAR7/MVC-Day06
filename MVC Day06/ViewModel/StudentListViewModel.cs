using MVC_Day06.Models;

namespace MVC_Day06.ViewModel
{
    public class StudentListViewModel
    {
        public List<Student> Students { get; set; }
        public string SearchString { get; set; }
        public int? DepartmentId { get; set; }
    }
}
