using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands.Teachers;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;
using School_Core.ViewModels.Teachers;

[assembly:InternalsVisibleTo("School_Core_Tests")]
[assembly:InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace School_Core.Controllers
{
    public class TeacherController : Controller
    {
        private readonly Messages _messages;
        private readonly TeacherListViewModel.IProvider _teacherProvider;
        private readonly IQuery<Teacher> _teacherQuery;
        private readonly TeacherAssignToLectureViewModel.IProvider _teacherAssignToLectureProvider;
        private readonly IQuery<Lecture> _lectureQuery;

        public TeacherController(
            Messages messages, 
            TeacherListViewModel.IProvider teacherProvider, 
            IQuery<Teacher> teacherQuery,
            TeacherAssignToLectureViewModel.IProvider teacherAssignToLectureProvider, 
            IQuery<Lecture> lectureQuery)
        {
            _messages = messages;
            _teacherProvider = teacherProvider;
            _teacherQuery = teacherQuery;
            _teacherAssignToLectureProvider = teacherAssignToLectureProvider;
            _lectureQuery = lectureQuery;
        }

        public IActionResult List()
        {
            return View(_teacherProvider.Provide());
        }

        [HttpGet]
        public IActionResult AssignToLecture(Guid teacherId, string info = "")
        {
            var teacher = _teacherQuery.GetSingleOrDefault(new HasIdSpec<Teacher>(teacherId));
            if (teacher is null) return NotFound();

            var model = _teacherAssignToLectureProvider.Provide(teacher.Id);
            if (ShouldAddTempInfo(info))
            {
                model.TempDummyVal = "important thing can not do in provider for some stupid reason";
            }

            return View(model);
        }

        internal virtual bool ShouldAddTempInfo(string info)
        {
            return !string.IsNullOrWhiteSpace(info);
        }

        [HttpPost]
        public IActionResult AssignToLecture(TeacherAssignToLectureViewModel viewModel)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(viewModel.LectureId));
            if (lecture is null) return NotFound();
            
            var teacher = _teacherQuery.GetSingleOrDefault(new HasIdSpec<Teacher>(viewModel.TeacherId));
            if (teacher is null) return NotFound();
            
            
            var command = new AssignTeacherToLectureCommand()
            {
                TeacherId = teacher.Id,
                LectureId = lecture.Id,
            };

            _messages.Dispatch(command);
            return RedirectToAction(nameof(List));
        }
    }
}