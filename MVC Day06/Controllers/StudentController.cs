using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Day06.Models;
using MVC_Day06.Services;
using MVC_Day06.ViewModel;

namespace MVC_Day06.Controllers
{
    public class StudentController : Controller
    {
        private StudentServices _services;
        private readonly CourseServices _crsService;

        public StudentController(StudentServices services,CourseServices crsService)
        {
            _services = services;
            _crsService = crsService;
        }
        public IActionResult GetAll(string searchString, int? departmentId)
        {
            var students = _services.GetFilteredStudents(searchString, departmentId);

            var viewModel = new StudentListViewModel
            {
                Students = students,
                SearchString = searchString,
                DepartmentId = departmentId,
            };
            ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
            return View(viewModel);
        }
        public IActionResult GetById(int id)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return View("404");
            }
            ViewBag.StudentId = id;
            return View(student);
        }
        /// <summary>
        /// Add student
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add(int? departmentId)
        {
            ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
            if (departmentId.HasValue)
            {
                ViewData["CourseList"] = new SelectList(_services.GetCoursesByDepartment(departmentId.Value), "Id", "Name");
            }
            else
            {
                ViewData["CourseList"] = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
                ViewData["CourseList"] = new SelectList(_services.GetCoursesByDepartment(viewModel.DepartmentId), "Id", "Name");
                return View(viewModel);
            }

            var student = new Student
            {
                Name = viewModel.Name,
                Age = viewModel.Age,
                DepartmentId = viewModel.DepartmentId
            };
             _services.Add(student);
            var stuCrsRes = new StuCrsRes
            {
                StudentId = student.Id,
                CourseId = viewModel.CourseId,
                Grade = viewModel.Degree
            };
            _services.AddStuCrsRes(stuCrsRes);
            return RedirectToAction("GetAll");
        }

        /// <summary>
        /// Edit student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return View("404");
            }
            var viewModel = new StudentViewModel
            {
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId
            };

            ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(int id, StudentViewModel viewModel)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return View("404");
            }
            if (!ModelState.IsValid)
            {
                ViewData["DeptList"] = new SelectList(_services.GetDepartments(), "Id", "Name");
                return View(viewModel);
            }
            student.Name = viewModel.Name;
            student.Age = viewModel.Age;
            student.DepartmentId = viewModel.DepartmentId;
            var status = _services.Update(student);
            if (status == null)
            {
                return BadRequest();
            }
            return RedirectToAction("GetAll");
        }
        /// <summary>
        /// Delete student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _services.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View("Warning", student);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _services.Delete(id);
            return RedirectToAction("GetAll");
        }


        /// <summary>
        /// GetStudentCourseDetails
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStudentCourseDetails(int studentId, int courseId)
        {
            var studentCourse = _services.ShowStudentCourseDetails(studentId, courseId);

            if (studentCourse == null)
            {
                return NotFound();
            }

            var viewModel = new StudentCourseDetailsViewModel
            {
                StudentName = studentCourse.Student.Name,
                CourseName = studentCourse.Course.Name,
                Grade = studentCourse.Grade.ToString(),
                MinDegree = studentCourse.Course.MinDegree
            };

            return View(viewModel);
        }


    }
}
