using System;
using FluentAssertions;
using NUnit.Framework;
using School_Core.Commands.Lectures;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Queries;

namespace TestingTests.Commands
{
    public class ArchiveLectureCommandTests
    {
        private SchoolCoreDbContext _dbContextMock;
        private LectureQuery _lectureQuery;
        private ArchiveLectureCommand.Handler _sut;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _lectureQuery = new LectureQuery(_dbContextMock);
            _sut = new ArchiveLectureCommand.Handler(_dbContextMock, _lectureQuery);
        }

        [TearDown]
        public void TestCleanup()
        {
            _dbContextMock.Database.EnsureDeleted();
        }

        [Test]
        public void Handle_Throws_Exception_When_Lecture_Is_Null()
        {
            var lecture = new Lecture("name");
            var command = new ArchiveLectureCommand(lecture.Id);

            //Act
            Action result = () => _sut.Handle(command);

            //Assert
            result.Should().Throw<ArgumentException>().WithMessage(nameof(command.Id));
        }

        [Test]
        public void Handle_Returns_True_And_Archives_Lecture_When_Lecture_Enrollment_Has_No_Students_With_Grade_None()
        {
            var lecture = new Lecture("name");
            var command = new ArchiveLectureCommand(lecture.Id);
            
            _dbContextMock.Add(lecture);
            _dbContextMock.SaveChanges();

            //Act
            var result = _sut.Handle(command);

            //Assert
            Lecture resultLecture;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultLecture = context.Lectures.Find(lecture.Id);
            }

            Assert.That(result, Is.True);
            Assert.That(resultLecture.Status, Is.EqualTo(LectureStatus.Archived));
        }

        [Test]
        public void Handle_Returns_False_And_Does_Not_Archive_Lecture_When_Lecture_Enrollment_Has_Students_With_Grade_None()
        {
            var lecture = new Lecture("name");
            var student = new Student("name");
            
            _dbContextMock.Add(lecture);
            _dbContextMock.Add(student);
            lecture.EnrollStudent(student);
            _dbContextMock.SaveChanges();
            
            var command = new ArchiveLectureCommand(lecture.Id);

            //Act
            var result = _sut.Handle(command);

            //Assert
            Lecture resultLecture;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultLecture = context.Lectures.Find(lecture.Id);
            }

            Assert.That(result, Is.False);
            Assert.That(resultLecture.Status, Is.EqualTo(LectureStatus.Open));
        }
    }
}