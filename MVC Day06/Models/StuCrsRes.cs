namespace MVC_Day06.Models
{
    public class StuCrsRes
    {
        // foreign key
        public int StudentId { get; set; }
        // foreign key
        public int CourseId { get; set; }
        public int Grade { get; set; }

        // navigation property
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
