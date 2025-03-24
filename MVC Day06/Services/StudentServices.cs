using Microsoft.EntityFrameworkCore;
using MVC_Day06.DbContexts;
using MVC_Day06.Models;

namespace MVC_Day06.Services
{
    public class StudentServices
    {
        AcademyDbContext db = new AcademyDbContext();

        public List<Student> GetAll()
        {
            return db.Students
                    .Include(s => s.Department)
                    .Include(s => s.StuCrsRes)
                    .ThenInclude(scr => scr.Course)
                    .ToList();
        }
        public Student GetById(int id)
        {
            return db.Students
                    .Include(s => s.Department)
                    .Include(s => s.StuCrsRes)
                    .ThenInclude(scr => scr.Course)
                    .FirstOrDefault(s => s.Id == id);
        }
        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public Student Update(Student student)
        {
            db.Students.Update(student);
            db.SaveChanges();
            return student;
        }

        public void Delete(int id)
        {
            var student = db.Students.Include(s => s.StuCrsRes).FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                db.StuCrsRes.RemoveRange(student.StuCrsRes);
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }
        public List<Course> GetCoursesByDepartment(int departmentId)
        {
            return db.Courses.Where(c => c.DepartmentId == departmentId).ToList();
        }

        public List<Student> GetFilteredStudents(string searchString, int? departmentId)
        {
            var query = db.Students.Include(s => s.Department).Include(s => s.StuCrsRes).ThenInclude(scr => scr.Course).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(s => s.DepartmentId == departmentId.Value);
            }

            return query.ToList();
        }

        public void AddStuCrsRes(StuCrsRes stuCrsRes)
        {
            db.StuCrsRes.Add(stuCrsRes);
            db.SaveChanges();
        }
        public StuCrsRes ShowStudentCourseDetails(int studentId, int courseId)
        {
            return db.StuCrsRes
            .Include(scr => scr.Student)
            .Include(scr => scr.Course)
            .FirstOrDefault(scr => scr.StudentId == studentId && scr.CourseId == courseId);
        }
    }
}
