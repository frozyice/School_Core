using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.ViewModels.Teacher;

namespace School_Core.Controllers
{

    public class TeacherController : Controller
    {
        private readonly TeacherViewModel.IProvider _teacherProvider;
        private readonly TeacherDetailsViewModel.IProvider _teacherDetailsProvider;
        public TeacherController(TeacherDetailsViewModel.IProvider teacherDetailsProvider, TeacherViewModel.IProvider teacherProvider)
        {
            _teacherProvider = teacherProvider;
            _teacherDetailsProvider = teacherDetailsProvider;
        }

        public IActionResult List()
        {
            return View(_teacherProvider.GetViewModel());
        }

        public IActionResult Details(Guid id)
        {
            return View(_teacherDetailsProvider.GetViewModel(id));
        }
    }
}
