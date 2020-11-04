using System;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> Medical()
        {
            return View();
            //GET
        }

        public async Task<IActionResult> AddMedical()
        {
            throw new NotImplementedException();
            //POST
        }

        public async Task<IActionResult> EditMedicalReason()
        {
            throw new NotImplementedException();
            //PUT
        }

        public async Task<IActionResult> MarkMedicalNotActive()
        {
            throw new NotImplementedException();
            //DELETE
        }
    }
    
}