using NUnit.Framework;
using School_Core.Contexts;
using School_Core.Queries;
using FluentAssertions;
using School_Core.Domain.Models.Teachers;
using TestingTests.Commands;

namespace TestingTests.Queries
{
    public class TeacherQueryTests
    {
        private SchoolCoreDbContext _dbContextInMemory;
        private TeacherQuery _sut;

        [SetUp]
        public void Setup()
        {
            _dbContextInMemory = DbContextFactory.GetInMemoryDbContext();
            _sut = new TeacherQuery(_dbContextInMemory);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContextInMemory.Database.EnsureDeleted();
        }
        

        [Test]
        public void GetAll_Returns_Teachers_When_Teachers_Exists()
        {
            var teacher = new Teacher("name");
            _dbContextInMemory.Add(teacher);
            _dbContextInMemory.SaveChanges();

            //Act
            var result = _sut.GetAll();

            //Assert
            result.Should().Contain(teacher);
        }

        [Test]
        public void GetAll_Returns_Empty_List_When_Teachers_Does_Not_Exist()
        {
            //Act
            var result = _sut.GetAll();

            //Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void Get_Returns_Teacher_When_Teacher_Exists()
        {
            var teacher = new Teacher("name");
            _dbContextInMemory.Add(teacher);
            _dbContextInMemory.SaveChanges();

            //Act
            var result = _sut.Get(teacher.Id);

            //Assert
            result.Should().Be(teacher);
        }

        [Test]
        public void Get_Returns_Null_When_Teacher_Does_Not_Exists()
        {
            var teacher = new Teacher("name");

            //Act
            var result = _sut.Get(teacher.Id);

            //Assert
            result.Should().BeNull();
        }
    }
}