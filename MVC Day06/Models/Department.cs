namespace MVC_Day06.Models
{
    public class Department
    {
        public  int  Id { get; set; }
        public string Name { get; set; } 
        public string MgrName { get; set; }

        // navigation property
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }
    }
}
