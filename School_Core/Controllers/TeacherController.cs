using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands.Teacher;
using School_Core.Queries;
using School_Core.Util;
using School_Core.ViewModels.Teacher;

[assembly:InternalsVisibleTo("School_Core_Tests")]
[assembly:InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace School_Core.Controllers
{
    public class TeacherController : Controller
    {
        private readonly Messages _messages;
        private readonly TeacherListViewModel.IProvider _teacherProvider;
        private readonly ITeacherQuery _teacherQuery;
        private readonly TeacherAssignToLectureViewModel.IProvider _teacherAssignToLectureProvider;
        private readonly ILectureQuery _lectureQuery;

        public TeacherController(Messages messages, TeacherListViewModel.IProvider teacherProvider, ITeacherQuery teacherQuery,
            TeacherAssignToLectureViewModel.IProvider teacherAssignToLectureProvider, ILectureQuery lectureQuery)
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
            var teacher = _teacherQuery.Get(teacherId);
            if (teacher == null)
            {
                return NotFound();
            }

            var model = _teacherAssignToLectureProvider.Provide(teacher.Id);
            if (ShouldAddTempInfo(info)) // vaata kas saad testida, kui sa mockid terve controlleri #SEOTUD 
            {
                model.TempDummyVal = "important thing can not do in provider for some stupid reason";
            }

            return View(model);
        }

        internal virtual bool ShouldAddTempInfo(string info) 
            // #SEOTUD siia meetodi sisse
            // 1. ei taha tulla
            // 2. tuleme
            // 3. testime selle otse 
            // ( 2 erinevat mokki ) 
        {
            return !string.IsNullOrWhiteSpace(info);
        }

        [HttpPost]
        public IActionResult AssignToLecture(TeacherAssignToLectureViewModel viewModel)
        {
            var lecture = _lectureQuery.Get(viewModel.LectureId);
            var teacher = _teacherQuery.Get(viewModel.TeacherId);
            if (lecture == null || teacher == null)
            {
                return NotFound();
            }
            
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