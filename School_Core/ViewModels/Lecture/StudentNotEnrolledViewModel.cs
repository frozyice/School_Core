﻿using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Contexts;
using School_Core.Querys;


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
                var lecture = _lectureQuery.GetLecture(lectureId);
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
                    viewmodel.canEnroll = lecture.CanEnroll(student);//enrollment == null ? false : true;
                    viewModels.Add(viewmodel);
                    
                }

                return viewModels;
            }
        }
    }

}