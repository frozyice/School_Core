using System;
using System.Collections.Generic;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.ViewModels.Lectures
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
            private readonly IQuery<Student> _studentQuery;
            private readonly IQuery<Lecture> _lectureQuery;

            public Provider(IQuery<Lecture> lectureQuery, IQuery<Student> studentQuery)
            {
                _studentQuery = studentQuery;
                _lectureQuery = lectureQuery;
            }

            public IEnumerable<StudentNotEnrolledViewModel> Provide(Guid lectureId)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Domain.Models.Lectures.Lecture>(lectureId));
                var viewModels = new List<StudentNotEnrolledViewModel>();
                if (lecture.Enrollments == null)
                {
                    return viewModels;
                }
                var students = _studentQuery.GetAll();
                //todo mingi parem lahendus olema kui et me küsimse välja kõik studentid. speckiga või kuidagi tuleb sed ahendada

                foreach (var student in students)
                {
                    var viewmodel = new StudentNotEnrolledViewModel();
                    viewmodel.Name = student.Name;

                    viewmodel.canEnroll = new CanEnrollSpec(student).IsSatisfiedBy(lecture);
                    viewModels.Add(viewmodel);
                }

                return viewModels;
            }
        }
    }
}