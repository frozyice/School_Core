using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands;
using School_Core.Contexts;
using School_Core.Repositories;
using School_Core.Util;
using School_Core.ViewModels.Lecture;

namespace School_Core.Controllers
{


    public class LectureController : Controller
    {
        private readonly SchoolCoreDbContext _context;
        private readonly ILectureRepository _lectureRepository;
        private readonly Messages _messages;
        private readonly EnrollStudentViewModel.IProvider _lectureAddStudentProvider;

        private readonly LectureListViewModel.IProvider _lectureListProvider;
        private readonly LectureDetailsViewModel.IProvider _lectureDetailsProvider;

        private readonly EnrollStudentViewModel.IMapper _lectureAddStudentMapper;
        

        public LectureController(SchoolCoreDbContext context,Messages messages, EnrollStudentViewModel.IProvider LectureAddStudentProvider,  ILectureRepository lectureRepository, LectureDetailsViewModel.IProvider lectureDetailProvider, EnrollStudentViewModel.IMapper lectureAddStudentMapper, LectureListViewModel.IProvider lectureListProvider)
        {
            _context = context;
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
        public IActionResult EnrollStudent(EnrollStudentViewModel enrollStudentViewModel)
        {
            // var errors = _lectureAddStudentMapper.Validate(lectureAddStudentViewModel.Id, lectureAddStudentViewModel.StudentName);
            // if (errors.Count() != 0)
            // {
            //     foreach(var error in errors)
            //     {
            //         ModelState.AddModelError("StudentName", error);
            //     }
            // }

            // if (!ModelState.IsValid)
            // {
            //     return View(lectureAddStudentViewModel);
            // }
             var command = new EnrollStudentCommand(enrollStudentViewModel.Id, enrollStudentViewModel.StudentName);
            _messages.Dispatch(command);
            // var lecture = _context.Lectures.Where(l => l.Id == lectureAddStudentViewModel.Id).Single();
            //
            // var student = _context.Students.FirstOrDefault(s => s.Name == lectureAddStudentViewModel.StudentName);
            //
            // var a = new Enrollment(student.Id);
            //  lecture.EnrollStudent(student.Id);
            // _context.SaveChanges();
            return RedirectToAction(nameof(Details), new { enrollStudentViewModel.Id});
        }
    }

}
