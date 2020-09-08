using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands;
using School_Core.Repositories;
using School_Core.Util;
using School_Core.ViewModels.Lecture;

namespace School_Core.Controllers
{


    public class LectureController : Controller
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly Messages _messages;
        private readonly LectureAddStudentViewModel.IProvider _lectureAddStudentProvider;

        private readonly LectureListViewModel.IProvider _lectureListProvider;
        private readonly LectureDetailsViewModel.IProvider _lectureDetailsProvider;

        private readonly LectureAddStudentViewModel.IMapper _lectureAddStudentMapper;
        

        public LectureController(Messages messages, LectureAddStudentViewModel.IProvider LectureAddStudentProvider,  ILectureRepository lectureRepository, LectureDetailsViewModel.IProvider lectureDetailProvider, LectureAddStudentViewModel.IMapper lectureAddStudentMapper, LectureListViewModel.IProvider lectureListProvider)
        {
            _lectureRepository = lectureRepository;
            _messages = messages;
            _lectureAddStudentProvider = LectureAddStudentProvider;

            _lectureListProvider = lectureListProvider;
            _lectureDetailsProvider = lectureDetailProvider;

            _lectureAddStudentMapper = lectureAddStudentMapper;

        }

        public IActionResult List()
        {
            return View(_lectureListProvider.Provide());
        }

        public IActionResult CloseEnrollment(Guid id)
        {
            var command = new CloseLectureEnrollmentCommand(id);
            _messages.Dispatch(command);
            return RedirectToAction(nameof(List));
        }

        public IActionResult ArchiveEnrollment(Guid id)
        {
            var command = new ArchiveLectureCommand(id);
            _messages.Dispatch(command);
            return RedirectToAction(nameof(List));
        }


        public IActionResult Details(Guid id)
        {
            return View(_lectureDetailsProvider.Provide(id));
        }

        

       
        public IActionResult EnrollStudent(Guid id)
        {
            return View(_lectureAddStudentProvider.Provide(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnrollStudent(LectureAddStudentViewModel lectureAddStudentViewModel)
        {
            var errors = _lectureAddStudentMapper.Validate(lectureAddStudentViewModel.Id, lectureAddStudentViewModel.StudentName);
            if (errors.Count() != 0)
            {
                foreach(var error in errors)
                {
                    ModelState.AddModelError("StudentName", error);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(lectureAddStudentViewModel);
            }

            _lectureAddStudentMapper.AddStudentToLecture(lectureAddStudentViewModel);
            
            return RedirectToAction(nameof(Details), new { lectureAddStudentViewModel.Id});
        }
    }

}
