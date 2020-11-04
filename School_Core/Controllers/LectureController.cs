using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands.Lectures;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;
using School_Core.Util.ModelState;
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

        [ImportModelState]
        public IActionResult List()
        {
            var isRedirectedWithSuccess = false;
            if (TempData.ContainsKey("redirectWithSuccess"))
            {
                isRedirectedWithSuccess = (bool) TempData["redirectWithSuccess"];
            }
            
            var model = _lectureListProvider.Provide(isRedirectedWithSuccess);
            return View(model);
        }

        [ExportModelState]
        public IActionResult CloseLecture(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();

            var command = new CloseLectureCommand(id);
            var result = _messages.Dispatch(command);
            if (!result.isSuccess)
            {
                result.Errors.ForEach(x => ModelState.AddModelError(x.Key, x.Error));
                return RedirectToAction(nameof(List));
            }
            
            TempData.Add("redirectWithSuccess", true);
            return RedirectToAction(nameof(List));
        }


        [ExportModelState]
        public IActionResult ArchiveLecture(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();

            var command = new ArchiveLectureCommand(id);
            var result = _messages.Dispatch(command);
            if (!result.isSuccess)
            {
                result.Errors.ForEach(x => ModelState.AddModelError(x.Key, x.Error));
                return RedirectToAction(nameof(List));
            }
            
            TempData.Add("redirectWithSuccess", true);
            return RedirectToAction(nameof(List));
        }


        [ImportModelState]
        public IActionResult Details(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();
            
            var isRedirectedWithSuccess = false;
            if (TempData.ContainsKey("redirectWithSuccess"))
            {
                isRedirectedWithSuccess = (bool) TempData["redirectWithSuccess"];
            }
            
            var model = _lectureDetailsProvider.Provide(id, isRedirectedWithSuccess);

            return View(model);
        }

        public IActionResult EnrollStudent(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();

            return View(_lectureAddStudentProvider.Provide(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelState]
        public IActionResult EnrollStudent(EnrollStudentViewModel viewModel)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(viewModel.LectureId));
            if (lecture is null) return NotFound();

            var student = _studentQuery.GetSingleOrDefault(new HasNameSpec<Student>(viewModel.StudentName));
            if (student is null) return NotFound();


            var command = new EnrollStudentCommand(lecture.Id, student.Name);
            var result = _messages.Dispatch(command);
            if (!result.isSuccess)
            {
                result.Errors.ForEach(x => ModelState.AddModelError(x.Key, x.Error));
                if (ModelState.ContainsKey("alert"))
                {
                    return RedirectToAction(nameof(Details), new {id = lecture.Id});
                }
                else
                {
                    return View(_lectureAddStudentProvider.Provide(viewModel.LectureId));
                }
            }

            TempData.Add("redirectWithSuccess", true);
            return RedirectToAction(nameof(Details), new {id = lecture.Id});
        }

        public IActionResult GradeStudent()
        {
            throw new NotImplementedException();
        }
    }
}