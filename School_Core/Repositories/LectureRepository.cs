using School_Core.Databases;
using School_Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace School_Core.Repositories
{


    public interface ILectureRepository
    {

        // query --> " specification pattern "  
        IEnumerable<Lecture> GetLectures();
        Lecture GetLecture(Guid id);

        // command
        Guid AddNewLecture(Lecture lecture);
        Guid EditLecture(Lecture lecture);
    }

    public class LectureRepository : ILectureRepository
    {
        private readonly IDatabase _database;

        public List<Lecture> Lectures { get; private set; }

        public LectureRepository(IDatabase database)
        {


            _database = database;
        }
        public Lecture GetLecture(Guid id)
        {
            return _database.GetLecture(id);
        }

        public IEnumerable<Lecture> GetLectures()
        {
            return _database.GetLectures();
        }

        public Guid AddNewLecture(Lecture lecture)
        {
            _database.AddNewLecture(lecture);
            return lecture.Id;
        }

        public Guid EditLecture(Lecture lecture)
        {
            _database.EditLecture(lecture);
            return lecture.Id;
        }
    }
}
