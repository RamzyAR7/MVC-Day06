using Microsoft.EntityFrameworkCore;
using MVC_Day06.DbContexts;
using MVC_Day06.Models;

namespace MVC_Day06.Services
{
    public class CourseServices
    {
        AcademyDbContext db = new AcademyDbContext();

        public List<Course> GetAll()
        {
            return db.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return db.Courses.Include(c => c.Department)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
        }
        public Course Update(Course course)
        {
            db.Courses.Update(course);
            db.SaveChanges();
            return course;
        }

        public void Delete(int id)
        {
            var course = db.Courses.Find(id);
            if (course != null)
            {
                db.Courses.Remove(course);
                db.SaveChanges();
            }
        }
        public List<Course> GetFilteredCourses(string searchString, int? departmentId)
        {
            var query = db.Courses.Include(c => c.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.Name.Contains(searchString));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(c => c.DepartmentId == departmentId.Value);
            }

            return query.ToList();
        }
        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }
    }
}
