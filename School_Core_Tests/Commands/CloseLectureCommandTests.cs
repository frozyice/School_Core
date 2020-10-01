using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using School_Core.Commands.Lectures;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;

namespace TestingTests.Commands
{
    public class CloseLectureEnrollmentCommandHandlerTests
    {
        private SchoolCoreDbContext _dbContextMock;
        private LectureQuery _lectureQuery;
        private CloseLectureCommand.Handler _sut;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _lectureQuery = new LectureQuery(_dbContextMock);
            _sut = new CloseLectureCommand.Handler(_dbContextMock, _lectureQuery);
            
            var lecture = new Lecture("name");
            var command = new CloseLectureCommand(lecture.Id);
        }

        [TearDown]
        public void TestCleanup()
        {
            _dbContextMock.Database.EnsureDeleted();
        }


        [Test]
        public void Handle_Returns_True_When_StatusIsOpen()
        {
            var lecture = new Lecture("name");
            _dbContextMock.Add(lecture);
            _dbContextMock.SaveChanges();
            
            var command = new CloseLectureCommand(lecture.Id);

            //Act
            var result = _sut.Handle(command);


            //Assert
            Lecture resultLecture;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultLecture = context.Lectures.Find(lecture.Id);
            }

            Assert.That(result, Is.True);
            Assert.That(resultLecture.Status, Is.EqualTo(LectureStatus.Closed));
        }

        [Test]
        public void Handle_Returns_False_When_LectureIsNotInDatabase()
        {
            _dbContextMock.Add(new Lecture("uus "));
            
            var lecture = new Lecture("name");
            var command = new CloseLectureCommand(lecture.Id);


            //Act
            var result = _sut.Handle(command);

            //Assert
            var lectures = _dbContextMock.Lectures.ToList();
            Assert.That(result, Is.False);
            Assert.That(lectures.Count, Is.EqualTo(0));
        }

        [Test]
        public void Handle_Returns_False_When_LectureStatusIsClosed()
        {
            var lecture = new Lecture("name");
            lecture.CloseLecture();
            _dbContextMock.Add(lecture);
            _dbContextMock.SaveChanges();

            var command = new CloseLectureCommand(lecture.Id);
            
            //Act
            var result = _sut.Handle(command);

            //Assert
            Lecture lectureResult;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                lectureResult = context.Lectures.Find(lecture.Id);
            }

            Assert.That(result, Is.False);
            Assert.That(lectureResult.Status, Is.EqualTo(LectureStatus.Closed));
        }

        [Test]
        public void Handle_Returns_False_When_LectureStatusIsArchived()
        {
            var lecture = new Lecture("name");
            lecture.ArchiveLecture();
            _dbContextMock.Add(lecture);
            _dbContextMock.SaveChanges();
            
            var command = new CloseLectureCommand(lecture.Id);

            //Act
            var result = _sut.Handle(command);

            //Assert
            Lecture lectureResult;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                lectureResult = context.Lectures.Find(lecture.Id);
            }

            Assert.That(result, Is.False);
            Assert.That(lectureResult.Status, Is.EqualTo(LectureStatus.Archived));
        }
    }

    public static class DbContextFactory
    {
        //https://justsimplycode.com/2018/06/02/mocking-entity-framework-core-dbcontext-for-unit-testing/
        public static SchoolCoreDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<SchoolCoreDbContext>().UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase").Options;
            var dbContext = new SchoolCoreDbContext(options);
            return dbContext;
        }
    }
}