using System;
using System.Collections.Generic;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Queries;


namespace School_Core.ViewModels.Lecture
{
    public class StudentNotEnrolledViewModel
    {
        public string Name { get; set; }
        public bool canEnroll { get; set; }

        public interface IProvider
        {
            IEnumerable<StudentNotEnrolledViewModel> Provide(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly IStudentQuery _studentQuery;
            private readonly SchoolCoreDbContext _context;
            private readonly ILectureQuery _lectureQuery;

            public Provider(ILectureQuery lectureQuery, IStudentQuery studentQuery, SchoolCoreDbContext context)
            {
                _studentQuery = studentQuery;
                _context = context;
                _lectureQuery = lectureQuery;
            }

            public IEnumerable<StudentNotEnrolledViewModel> Provide(Guid lectureId)
            {
                var students = _studentQuery.GetStudents();
                var lecture = _lectureQuery.Get(lectureId);
                var viewModels = new List<StudentNotEnrolledViewModel>();
                if (lecture.Enrollments == null)
                {
                    return viewModels;
                }

                foreach (var student in students)
                {
                    var viewmodel = new StudentNotEnrolledViewModel();
                    viewmodel.Name = student.Name;

                    //var enrollment = lecture.Enrollments.Where(x => x.StudentId == student.Id).SingleOrDefault();
                    viewmodel.canEnroll = new CanEnrollSpec(student).IsSatisfiedBy(lecture); //enrollment == null ? false : true;
                    viewModels.Add(viewmodel);
                }

                return viewModels;
            }
        }
    }
}