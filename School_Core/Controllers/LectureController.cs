using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands.Lecture;
using School_Core.Querys;
using School_Core.Util;
using School_Core.ViewModels.Lecture;

namespace School_Core.Controllers
{
    public class LectureController : Controller
    {
        private readonly Messages _messages;
        private readonly EnrollStudentViewModel.IProvider _lectureAddStudentProvider;
        private readonly LectureListViewModel.IProvider _lectureListProvider;
        private readonly ILectureQuery _lectureQuery;
        private readonly LectureDetailsViewModel.IProvider _lectureDetailsProvider;

        public LectureController(Messages messages, EnrollStudentViewModel.IProvider LectureAddStudentProvider, LectureDetailsViewModel.IProvider lectureDetailProvider,
            LectureListViewModel.IProvider lectureListProvider, ILectureQuery lectureQuery)
        {
            _messages = messages;
            _lectureAddStudentProvider = LectureAddStudentProvider;
            _lectureListProvider = lectureListProvider;
            _lectureQuery = lectureQuery;
            _lectureDetailsProvider = lectureDetailProvider;
        }

        public IActionResult List()
        {
            return View(_lectureListProvider.Provide());
        }

        public IActionResult CloseLecture(Guid id)
        {
            var lecture = _lectureQuery.GetLecture(id);
            if (lecture == null)
            {
                return NotFound();
            }

            var command = new CloseLectureCommand(id);
            _messages.Dispatch(command);

            return RedirectToAction(nameof(List));
        }

        public IActionResult ArchiveLecture(Guid id)
        {
            var lecture = _lectureQuery.GetLecture(id);
            if (lecture == null)
            {
                return NotFound();
            }

            var command = new ArchiveLectureCommand(id);
            _messages.Dispatch(command);
            return RedirectToAction(nameof(List));
        }


        public IActionResult Details(Guid id)
        {
            var lecture = _lectureQuery.GetLecture(id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(_lectureDetailsProvider.Provide(id));
        }

        public IActionResult EnrollStudent(Guid id)
        {
            var lecture = _lectureQuery.GetLecture(id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(_lectureAddStudentProvider.Provide(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnrollStudent(EnrollStudentViewModel enrollStudentViewModel)
        {
            var lecture = _lectureQuery.GetLecture(enrollStudentViewModel.LectureId);
            if (lecture == null)
            {
                throw new ArgumentException(); // või peaks olema NotFound ikka ? 
            }

            var command = new EnrollStudentCommand(enrollStudentViewModel.LectureId, enrollStudentViewModel.StudentName);
            var isSuccess = _messages.Dispatch(command);
            if (isSuccess == false)
            {
                // võiks veateade olla, kas commandist võiks juba Result tulla näiteks 
            }

            return RedirectToAction(nameof(Details), new {Id = enrollStudentViewModel.LectureId});
        }

        public IActionResult GradeStudent()
        {
            throw new NotImplementedException();
        }
    }
}