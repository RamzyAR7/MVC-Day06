namespace MVC_Day06.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string? Address { get; set; }
        // foreign key
        public int CourseId { get; set; }
        // foreign key
        public int DepartmentId { get; set; }


        // navigation property
        public Department Department { get; set; }
        public Course Course { get; set; }
    }
}
