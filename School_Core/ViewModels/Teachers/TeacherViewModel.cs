using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;

namespace School_Core.ViewModels.Teachers
{
    public class TeacherViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LectureName { get; set; }

        public interface IProvider
        {
            IEnumerable<TeacherViewModel> Provide();
        }

        public class Provider : IProvider
        {
            private readonly IQuery<Lecture> _lectureQuery;
            private readonly IQuery<Teacher> _teacherQuery;

            public Provider(IQuery<Lecture> lectureQuery, IQuery<Teacher> teacherQuery)
            {
                _lectureQuery = lectureQuery;
                _teacherQuery = teacherQuery;
            }

            public IEnumerable<TeacherViewModel> Provide()
            {
                var teachers = _teacherQuery.GetAll();
                var teacherLectures =
                    _lectureQuery.GetAll(new LecturesWithTeacherIdsSpec(teachers.Select(x => x.Id))); 
                
                foreach (var teacher in teachers)
                {
                    yield return new TeacherViewModel
                    {
                        Id = teacher.Id, Name = teacher.Name, LectureName = teacherLectures.SingleOrDefault(x => x.Teacher.Id == teacher.Id)?.Name ?? "none"
                    };
                }
            }
        }
    }
}