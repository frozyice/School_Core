using System;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.ViewModels.Teachers
{
    public class TeacherDetailsViewModel
    {
        public string Name { get; set; }

        public interface IProvider
        {
            TeacherDetailsViewModel Provide(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly IQuery<Teacher> _teacherQuery;

            public Provider(IQuery<Teacher> teacherQuery)
            {
                _teacherQuery = teacherQuery;
            }

            public TeacherDetailsViewModel Provide(Guid id)
            {
                var teacher = _teacherQuery.GetSingleOrDefault(new HasIdSpec<Teacher>(id));
                if (teacher == null)
                {
                    return null;
                }

                return new TeacherDetailsViewModel
                {
                    Name = teacher.Name
                };
            }
        }
    }
}