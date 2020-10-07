using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands.Lectures;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;
using School_Core.ViewModels.Lectures;

namespace School_Core.Controllers
{
    public class LectureController : Controller
    {
        private readonly Messages _messages;
        private readonly IQuery<Lecture> _lectureQuery;
        private readonly IQuery<Student> _studentQuery;
        private readonly LectureListViewModel.IProvider _lectureListProvider;
        private readonly LectureDetailsViewModel.IProvider _lectureDetailsProvider;
        private readonly EnrollStudentViewModel.IProvider _lectureAddStudentProvider;

        public LectureController(
            Messages messages,
            IQuery<Lecture> lectureQuery,
            IQuery<Student> studentQuery,
            LectureListViewModel.IProvider lectureListProvider,
            LectureDetailsViewModel.IProvider lectureDetailProvider,
            EnrollStudentViewModel.IProvider lectureAddStudentProvider)
        {
            _messages = messages;
            _lectureQuery = lectureQuery;
            _studentQuery = studentQuery;
            _lectureListProvider = lectureListProvider;
            _lectureDetailsProvider = lectureDetailProvider;
            _lectureAddStudentProvider = lectureAddStudentProvider;
        }

        public IActionResult List()
        {
            return View(_lectureListProvider.Provide());
        }

        public IActionResult CloseLecture(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();

            var command = new CloseLectureCommand(id);
            _messages.Dispatch(command);

            return RedirectToAction(nameof(List));
            //todo: Võiks kasutajale ka kuidagi teada anda, et õnnestusime 
        }

        public IActionResult ArchiveLecture(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();

            var command = new ArchiveLectureCommand(id);
            _messages.Dispatch(command);
            return RedirectToAction(nameof(List));
        }


        public IActionResult Details(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();

            return View(_lectureDetailsProvider.Provide(id));
        }

        public IActionResult EnrollStudent(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();

            return View(_lectureAddStudentProvider.Provide(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnrollStudent(EnrollStudentViewModel viewModel)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(viewModel.LectureId));
            if (lecture is null) return NotFound();

            var student = _studentQuery.GetSingleOrDefault(new HasNameSpec<Student>(viewModel.StudentName));
            if (student is null) return NotFound();

            var command = new EnrollStudentCommand(lecture.Id, student.Name);
            var isSuccess = _messages.Dispatch(command);
            if (!isSuccess)
            {
                //todo võiks veateade olla, kas commandist võiks juba Result tulla näiteks 
            }

            return RedirectToAction(nameof(Details), new {lecture.Id});
        }

        public IActionResult GradeStudent()
        {
            throw new NotImplementedException();
        }
    }
}