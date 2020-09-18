using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Specs;
using School_Core.Querys;

namespace School_Core.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public string Name { get; set; }
        public string LectureName { get; set; }

        public interface IProvider
        {
            IEnumerable<TeacherViewModel> Provide();
        }

        public class Provider : IProvider
        {
            private readonly ILectureQuery _lectureQuery;
            private readonly ITeacherQuery _teacherQuery;

            public Provider(ILectureQuery lectureQuery, ITeacherQuery teacherQuery)
            {
                _lectureQuery = lectureQuery;
                _teacherQuery = teacherQuery;
            }

            public IEnumerable<TeacherViewModel> Provide()
            {
                var teachers = _teacherQuery.GetAll();
                var teacherLectures =
                    _lectureQuery.GetLectures(
                        new LecturesWithTeacherIdsSpec(teachers.Select(x =>
                            x.Id))); // Kuna me ei taha, et student näeks kollektsiooni Lecture-st. ( meie DDD lähenemine ), kuid võiksime ka kollektsiooni lisada ( readonly )  

                foreach (var teacher in teachers)
                {
                    yield return new TeacherViewModel {Name = teacher.Name, LectureName = teacherLectures.SingleOrDefault(x => x.Teacher.Id == teacher.Id)?.Name ?? "none"};
                }
            }
        }
    }
}