using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.Repositories;
using School_Core.ViewModels.Student;

namespace School_Core.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentRepository _studentRepository;

        private readonly StudentViewModel.IProvider _studentProvider;
        private readonly StudentDetailsViewModel.IProvider _studentDetailsProvider;
        private readonly StudentAddNewViewModel.IMapper _studentAddNewMapper;


        public StudentController(IStudentRepository studentRepository, StudentDetailsViewModel.IProvider studentDetailsProvider, StudentAddNewViewModel.IMapper studentAddNewMapper, StudentViewModel.IProvider studentProvider)
        {
            _studentRepository = studentRepository;

            _studentProvider = studentProvider;
            _studentDetailsProvider = studentDetailsProvider;
            _studentAddNewMapper = studentAddNewMapper;
        }

        public IActionResult List()
        {
            return View(_studentProvider.GetViewModel());
        }

        public IActionResult Details(Guid id)
        {
            return View(_studentDetailsProvider.GetViewModel(id));
        }

        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNew(StudentAddNewViewModel studentAddNewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(studentAddNewViewModel);
            }
            var error = _studentAddNewMapper.Validate(studentAddNewViewModel.Name);
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError("Name", error);
                return View(studentAddNewViewModel);
            }
            _studentAddNewMapper.AddNewStudent(studentAddNewViewModel);
            return Redirect("List"); 
        }
    }
}
