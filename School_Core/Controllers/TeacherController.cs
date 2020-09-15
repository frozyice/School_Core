using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.ViewModels.Teacher;

namespace School_Core.Controllers
{

    public class TeacherController : Controller
    {
        private readonly TeacherListViewModel.IProvider _teacherProvider;
        private readonly TeacherDetailsViewModel.IProvider _teacherDetailsProvider;
        public TeacherController(TeacherDetailsViewModel.IProvider teacherDetailsProvider, TeacherListViewModel.IProvider teacherProvider)
        {
            _teacherProvider = teacherProvider;
            _teacherDetailsProvider = teacherDetailsProvider;
        }

        public IActionResult List()
        {
            return View(_teacherProvider.Provide());
        }

        public IActionResult AssignToLecture()
        {
            throw new NotImplementedException();
        }
    }
}
