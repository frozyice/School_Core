using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.ViewModels.Student;

namespace School_Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentListViewModel.IProvider _studentProvider;

        public StudentController(StudentListViewModel.IProvider studentProvider)
        {
            _studentProvider = studentProvider;
        }

        public IActionResult List()
        {
            // todo samad not found asjad 
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
