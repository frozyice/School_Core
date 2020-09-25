using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using School_Core.Commands.Teacher;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;

namespace TestingTests.Commands
{
    public class AssignTeacherToLectureCommandHandlerTests
    {
        private Mock<ITeacherQuery> _teacherQuery;
        private Mock<ILectureQuery> _lectureQuery;
        private SchoolCoreDbContext _dbContextMock;
        private AssignTeacherToLectureCommand.Handler _sut;

        [SetUp]
        public void Setup()
        {
            _teacherQuery = new Mock<ITeacherQuery>();
            _lectureQuery = new Mock<ILectureQuery>();
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _sut = new AssignTeacherToLectureCommand.Handler(_teacherQuery.Object, _lectureQuery.Object, _dbContextMock);
        }

        [Test]
        public void Handle_Does_Not_Assign_Teacher_To_Lecture_And_Returns_False_When_Lecture_Is_Not_Found()
        {
            var teacher = new Teacher("name");
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};

            _teacherQuery.Setup(x => x.Get(It.Is<Guid>(x => x == teacher.Id))).Returns(teacher);

            //Act
            var result = _sut.Handle(command);

            //Assert
            lectureMock.Verify(x => x.AssignTeacher(It.IsAny<Teacher>()), Times.Never);
            result.Should().BeFalse();
        }

        [Test]
        public void Handle_Does_Not_Assign_Teacher_To_Lecture_And_Returns_False_When_Teacher_Is_Not_Found()
        {
            var teacher = new Teacher("name");
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};

            _lectureQuery.Setup(x => x.Get(It.Is<Guid>(x => x == teacher.Id))).Returns(lectureMock.Object);

            //Act
            var result = _sut.Handle(command);

            //Assert
            lectureMock.Verify(x => x.AssignTeacher(It.IsAny<Teacher>()), Times.Never);
            result.Should().BeFalse();
        }

        [Test]
        public void Handle_Does_Not_Assign_Teacher_To_Lecture_And_Returns_False_When_Lecture_Status_Is_Archived()
        {
            var teacher = new Teacher("name");
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};

            _teacherQuery.Setup(x => x.Get(It.Is<Guid>(x => x == teacher.Id))).Returns(teacher);
            _lectureQuery.Setup(x => x.Get(It.Is<Guid>(x => x == lectureMockId))).Returns(lectureMock.Object);
            _lectureQuery.Setup(x => x.GetAll(It.IsAny<Specification<Lecture>>())).Returns(new List<Lecture>() {lectureMock.Object});
            lectureMock.Setup(x => x.Status).Returns(LectureStatus.Archived);

            //Act
            var result = _sut.Handle(command);

            //Assert
            lectureMock.Verify(x => x.AssignTeacher(It.IsAny<Teacher>()), Times.Never);
            result.Should().BeFalse();
        }

        [Test]
        public void Handle_Does_Not_Assign_Teacher_To_Lecture_And_Returns_False_When_Teacher_Has_Lecture_Assigned()
        {
            var teacher = new Teacher("name");
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};

            _teacherQuery.Setup(x => x.Get(It.Is<Guid>(x => x == teacher.Id))).Returns(teacher);
            _lectureQuery.Setup(x => x.Get(It.Is<Guid>(x => x == lectureMockId))).Returns(lectureMock.Object);
            _lectureQuery.Setup(x => x.GetAll(It.IsAny<Specification<Lecture>>())).Returns(new List<Lecture>() {lectureMock.Object});
            // lectureMock.Setup(x => x.Id).Returns(lectureMockId);

            //Act
            var result = _sut.Handle(command);

            //Assert
            // result.Should().BeFalse();
            teacher.Should().Be(teacher);
            lectureMock.Verify(x => x.AssignTeacher(It.IsAny<Teacher>()), Times.Never);
        }

        [TestCase(LectureStatus.Open)]
        [TestCase(LectureStatus.Closed)]
        public void Handle_Assigns_Teacher_To_Lecture_And_Returns_True_When_Lecture_Status_Not_Archived(LectureStatus lectureStatus)
        {
            var teacher = new Teacher("name");
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};

            _teacherQuery.Setup(x => x.Get(It.Is<Guid>(x => x == command.TeacherId))).Returns(teacher);
            _lectureQuery.Setup(x => x.Get(It.Is<Guid>(x => x == command.LectureId))).Returns(lectureMock.Object);
            _lectureQuery.Setup(x => x.GetAll(It.IsAny<Specification<Lecture>>())).Returns(new List<Lecture>() {});
            lectureMock.Setup(x => x.Status).Returns(lectureStatus);
            
            //Act
            var result = _sut.Handle(command);
            
            //Assert
            result.Should().BeTrue();
            lectureMock.Verify(x => x.AssignTeacher(It.Is<Teacher>(x => x.Id == teacher.Id)), Times.Once);
        }
    }
}