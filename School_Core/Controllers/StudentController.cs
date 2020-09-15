using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.Querys;
using School_Core.ViewModels.Student;

namespace School_Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentListViewModel.IProvider _studentProvider;
        private readonly IStudentQuery _studentQuery;

        public StudentController(StudentListViewModel.IProvider studentProvider, IStudentQuery studentQuery)
        {
            _studentProvider = studentProvider;
            _studentQuery = studentQuery;
        }

        public IActionResult List()
        {
            return View(_studentProvider.Provide());
        }

        public IActionResult RegisterNew()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterNew(StudentAddNewViewModel studentAddNewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(studentAddNewViewModel);
            }
            throw new NotImplementedException();
            return Redirect("List"); 
        }

        public IActionResult EditInfo()
        {
            throw new NotImplementedException();
        }
        
    }
}
