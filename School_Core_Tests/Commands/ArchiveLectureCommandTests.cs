using System;
using NUnit.Framework;
using School_Core.Commands.Lecture;
using School_Core.Contexts;
using School_Core.Domain.Models;
using School_Core.Domain.Models.Students;

namespace School_Core_Tests.Commands
{
    public class ArchiveLectureCommandTests
    {
        SchoolCoreDbContext _dbContextMock;
        Lecture _lecture;
        Guid _lectureId;
        ArchiveLectureCommand.Handler _sut;
        ArchiveLectureCommand _command;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _lecture = new Lecture("name");
            _lectureId = _lecture.Id;
            _command = new ArchiveLectureCommand(_lectureId);
            _sut = new ArchiveLectureCommand.Handler(_dbContextMock);
        }

        [TearDown]
        public void TestCleanup()
        {
            _dbContextMock.Database.EnsureDeleted();
        }
        
        [Test]
        public void Handle_Returns_False_When_Lecture_Is_Null()
        {
            //Act
            var result = _sut.Handle(_command);

            //Assert
            Lecture resultLecture;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultLecture = context.Lectures.Find(_lectureId);
            }
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Handle_Returns_True_And_Archives_Lecture_When_Lecture_Enrollment_Has_No_Students_With_Grade_None()
        {
            _dbContextMock.Add(_lecture);
            _dbContextMock.SaveChanges();
            
            //Act
            var result = _sut.Handle(_command);

            //Assert
            Lecture resultLecture;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultLecture = context.Lectures.Find(_lectureId);
            }
            Assert.That(result, Is.True);
            Assert.That(resultLecture.Status, Is.EqualTo(LectureStatus.Archived));
        }
        
        [Test]
        public void Handle_Returns_False_And_Does_Not_Archive_Lecture_When_Lecture_Enrollment_Has_Students_With_Grade_None()
        {
            var student = new Student("name");
            _dbContextMock.Add(student);
            _lecture.EnrollStudent(student);
            _dbContextMock.Add(new Student("name"));
            
            _dbContextMock.Add(_lecture);
            _dbContextMock.SaveChanges();
            
            //Act
            var result = _sut.Handle(_command);

            //Assert
            Lecture resultLecture;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultLecture = context.Lectures.Find(_lectureId);
            }
            Assert.That(result, Is.False);
            Assert.That(resultLecture.Status, Is.EqualTo(LectureStatus.Open));
        }
    }
}