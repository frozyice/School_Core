using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;
using School_Core.Specifications;
using TestingTests.Commands;

namespace TestingTests.Queries
{
    public class LectureQueryTests
    {
        private SchoolCoreDbContext _dbContextInMemory;
        private LectureQuery _sut;

        [SetUp]
        public void Setup()
        {
            _dbContextInMemory = DbContextFactory.GetInMemoryDbContext();
            _sut = new LectureQuery(_dbContextInMemory);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContextInMemory.Database.EnsureDeleted();
        }


        [Test]
        public void GetAll_Returns_Teachers_When_Teachers_Exists()
        {
            var lecture = new Lecture("name");
            var lecture2 = new Lecture("name");
            _dbContextInMemory.Add(lecture);
            _dbContextInMemory.Add(lecture2);
            _dbContextInMemory.SaveChanges();

            //Act
            var result = _sut.GetAll();

            //Assert
            result.Should().Contain(lecture);
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
            var lecture = new Lecture("name");
            _dbContextInMemory.Add(lecture);
            _dbContextInMemory.SaveChanges();

            //Act
            var result = _sut.Get(lecture.Id);

            //Assert
            result.Should().Be(lecture);
        }

        [Test]
        public void Get_Returns_Null_When_Teacher_Does_Not_Exists()
        {
            var lecture = new Lecture("name");

            //Act
            var result = _sut.Get(lecture.Id);

            //Assert
            result.Should().BeNull();
        }

        [Test]
        public void GetAll_Returns_Teachers_When_Spec_Is_Passed()
        {
            // var lecture = new Lecture("name");
            // var lectures = new List<Lecture>() {lecture};
            
            var specMock = new Mock<ISpecification<Lecture>>();
            
            // var specMock2 = new Mock<Specification<Lecture>>();

            var expectedLectures = new Lecture[2].AsQueryable();
            
            specMock.Setup(x => x.SatisfyEntitiesFrom(It.IsAny<IQueryable<Lecture>>())).Returns(expectedLectures);

            //Act
            var result = _sut.GetAll(specMock.Object);

            //Assert
             Assert.That(result,Is.EqualTo(expectedLectures));
        }

    }
}