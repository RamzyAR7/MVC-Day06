using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Day06.Models;
using MVC_Day06.Services;
using MVC_Day06.ViewModel;

namespace MVC_Day06.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseServices _service;

        public CourseController(CourseServices service)
        {
            _service = service;
        }

        public IActionResult GetAll(string searchString, int? departmentId)
        {
            var courses = _service.GetFilteredCourses(searchString, departmentId);

            var viewModel = new CourseListViewModel
            {
                Courses = courses,
                SearchString = searchString,
                DepartmentId = departmentId,
            };
            ViewData["DeptList"] = new SelectList(_service.GetDepartments(), "Id", "Name");
            return View(viewModel);
        }

        public IActionResult GetById(int id)
        {
            var course = _service.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            if (course.Department == null)
            {
                course.Department = new Department { Name = "No Department" };
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["DeptList"] = new SelectList(_service.GetDepartments(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["DeptList"] = new SelectList(_service.GetDepartments(), "Id", "Name");
                return View(viewModel);
            }

            var course = new Course
            {
                Name = viewModel.Name,
                Degree = viewModel.Degree,
                MinDegree = viewModel.MinDegree,
                DepartmentId = viewModel.DepartmentId
            };
            _service.Add(course);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _service.GetById(id);
            if (course == null)
            {
                return View("404");
            }
            var viewModel = new CourseViewModel
            {
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                DepartmentId = course.DepartmentId
            };

            ViewData["DeptList"] = new SelectList(_service.GetDepartments(), "Id", "Name");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, CourseViewModel viewModel)
        {
            var course = _service.GetById(id);
            if (course == null)
            {
                return View("404");
            }
            if (!ModelState.IsValid)
            {
                ViewData["DeptList"] = new SelectList(_service.GetDepartments(), "Id", "Name");
                return View(viewModel);
            }
            course.Name = viewModel.Name;
            course.Degree = viewModel.Degree;
            course.MinDegree = viewModel.MinDegree;
            course.DepartmentId = viewModel.DepartmentId;
            var status = _service.Update(course);
            if (status == null)
            {
                return BadRequest();
            }
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _service.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View("Warning", course);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}
