using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.ViewModels.Students;

namespace School_Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentListViewModel.IProvider _studentProvider;

        public StudentController(StudentListViewModel.IProvider studentProvider)
        {
            _studentProvider = studentProvider;
        }

        public IActionResult List(bool filterFirstYearStudents, bool filterLawStudents)
        {
            return View(_studentProvider.Provide(filterFirstYearStudents, filterLawStudents));
        }

        public IActionResult RegisterNew()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterNew(StudentAddNewViewModel studentAddNewViewModel)
        {
            throw new NotImplementedException();
        }

        public IActionResult EditInfo()
        {
            throw new NotImplementedException();
        }
    }
}