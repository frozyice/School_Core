using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Contexts;
using School_Core.Domain.Models;
using School_Core.Querys;


namespace School_Core.ViewModels.Lecture
{
    public class StudentEnrolledViewModel
    {
        public string Name { get; set; }
        public bool isEnrolled { get; set; }
        public interface IProvider
        {
            IEnumerable<StudentEnrolledViewModel> Provide(Guid id);
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
                //todo: siin on pooleli
            }
        
            public IEnumerable<StudentEnrolledViewModel> Provide(Guid lectureId)
            {
                var students = _studentQuery.GetStudents();
                var lecture = _lectureQuery.GetLecture(lectureId);
                var viewModels = new List<StudentEnrolledViewModel>();
                if (lecture.Enrollments == null)
                {
                    return viewModels;
                }

                foreach (var student in students)
                {
                    var viewmodel = new StudentEnrolledViewModel();
                    viewmodel.Name = student.Name;
                    
                    var enrollment = lecture.Enrollments.Where(x => x.StudentId == student.Id).SingleOrDefault();
                    viewmodel.isEnrolled = enrollment == null ? false : true;
                    viewModels.Add(viewmodel);
                }

                return viewModels;
            }
        }
    }

}