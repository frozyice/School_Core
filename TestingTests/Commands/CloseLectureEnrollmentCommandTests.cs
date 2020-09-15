using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using School_Core.Commands.Lecture;
using School_Core.Contexts;
using School_Core.Domain.Models;
using System.Linq;

namespace TestingTests.Commands
{
    class CloseLectureEnrollmentCommandHandlerTests
    {

        SchoolCoreDbContext _dbContextMock;
        private Lecture _lecture;
        private Guid _id;
        CloseLectureCommand _command;
        CloseLectureCommand.Handler _sut;

        [SetUp]
        public void Setup()
        {
            
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _lecture = new Lecture("name");
            _id = _lecture.Id;
            _command = new CloseLectureCommand(_id);
            _sut = new CloseLectureCommand.Handler(_dbContextMock);
            //dbContextMock.Database.EnsureDeleted();
            //dbContextMock.Database.EnsureCreated();
            //var a = dbContextMock.Lectures.ToList();
        }

        [TearDown]
        public void TestCleanup()
        {
            _dbContextMock.Database.EnsureDeleted();
            //var a = dbContextMock.Lectures.ToList();
        }
        

        [Test]
        public void Handle_Returns_True_When_StatusIsOpen()
        {
            _dbContextMock.Add(_lecture); 
            _dbContextMock.SaveChanges();
            
            // var _sut = new CloseLectureEnrollmentCommand.Handler(_dbContextMock);

            //Act
            var result = _sut.Handle(_command);
            
            
            //Assert
            Lecture resultLecture;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultLecture = context.Lectures.Find(_id);
            }

            Assert.That(result, Is.True);
            Assert.That(resultLecture.Status , Is.EqualTo(LectureStatus.Closed));
        }

        //SQLite inMemory
        // [Test]
        // public void Handle_Returns_True_When_StatusIsOpen2()
        // {
        //     var options = DbContextFactory.CreateSqlLiteOptions();
        //     var lecture = new Lecture("name");
        //     var id = lecture.Id;
        //     bool result;
        //
        //     using (var context = new SchoolCoreDbContext(options))
        //     {
        //         
        //         //You have to create the database
        //         context.Database.EnsureCreated();
        //
        //
        //         context.Add(lecture);
        //         context.SaveChanges();
        //
        //         var command = new CloseLectureEnrollmentCommand(id);
        //
        //         var _sut = new CloseLectureEnrollmentCommand.Handler(context); 
        //         
        //         //Act
        //         result = _sut.Handle(command);
        //     }
        //
        //     //Assert
        //     Lecture resultLecture;
        //     using (var context = new SchoolCoreDbContext(options))
        //     {
        //         resultLecture = context.Lectures.Find(id);
        //     }
        //
        //     Assert.That(result, Is.True);
        //     Assert.That(resultLecture.Status , Is.EqualTo(LectureStatus.Closed));
        // }

        [Test]
        public void Handle_Returns_False_When_LectureIsNotInDatabase()
        {           
            _dbContextMock.Add(new Lecture("uus ")); 


            //Act
            var result = _sut.Handle(_command);

            //Assert
            var lectures = _dbContextMock.Lectures.ToList();
            Assert.That(result, Is.False);
            Assert.That(lectures.Count, Is.EqualTo(0));
        }

        [Test]
        public void Handle_Returns_False_When_LectureStatusIsClosed()
        {
            _lecture.CloseLecture();
            _dbContextMock.Add(_lecture);
            _dbContextMock.SaveChanges();

            //Act
            var result =_sut.Handle(_command);
            
            //Assert
            Lecture lectureResult;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                lectureResult = context.Lectures.Find(_id);
            }
            Assert.That(result, Is.False);
            Assert.That(lectureResult.Status, Is.EqualTo(LectureStatus.Closed));
        }
        
        [Test]
        public void Handle_Returns_False_When_LectureStatusIsArchived()
        {
            _lecture.ArchiveLecture();
            _dbContextMock.Add(_lecture);
            _dbContextMock.SaveChanges();

            //Act
            var result =_sut.Handle(_command);
            
            //Assert
            Lecture lectureResult;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                lectureResult = context.Lectures.Find(_id);
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
            var options = new DbContextOptionsBuilder<SchoolCoreDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                .Options;

            //var options = new DbContextOptionsBuilder<SchoolCoreDbContext>().UseSqlite("Data Source=:memory:;Version=3;New=True;").Options;

            var dbContext = new SchoolCoreDbContext(options);

            //dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return dbContext;
        }

        //https://www.thereformedprogrammer.net/using-in-memory-databases-for-unit-testing-ef-core-applications/
        public static DbContextOptions<SchoolCoreDbContext> CreateSqlLiteOptions()  
        {
            //This creates the SQLite connection string to in-memory database
            var connectionStringBuilder = new SqliteConnectionStringBuilder
                { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();

            //This creates a SqliteConnectionwith that string
            var connection = new SqliteConnection(connectionString);

            //The connection MUST be opened here
            connection.Open();
            //Now we have the EF Core commands to create SQLite options
            var builder = new DbContextOptionsBuilder<SchoolCoreDbContext>();
            builder.UseSqlite(connection);

            return builder.Options;
        }

        //https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/#unit-testing
    }



    

}
