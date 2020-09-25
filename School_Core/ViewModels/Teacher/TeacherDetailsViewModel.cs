using System;
using System.Linq;
using School_Core.Queries;

namespace School_Core.ViewModels.Teacher
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
            private readonly ITeacherQuery _query;

            public Provider(ITeacherQuery query)
            {
                _query = query;
            }

            public TeacherDetailsViewModel Provide(Guid id)
            {
                var teacher = _query.GetAll().FirstOrDefault(x => x.Id == id); // todo peaks spec olema ja query täiendus
                if (teacher == null)
                {
                    return null;
                }

                return new TeacherDetailsViewModel() {Name = teacher.Name};
            }
        }
    }
}