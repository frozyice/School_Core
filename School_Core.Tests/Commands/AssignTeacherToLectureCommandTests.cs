using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using School_Core.Commands.Teachers;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;

namespace TestingTests.Commands
{
    public class AssignTeacherToLectureCommandHandlerTests
    {
        private Mock<IQuery<Teacher>> _teacherQuery;
        private Mock<IQuery<Lecture>> _lectureQuery;
        private SchoolCoreDbContext _dbContextMock;
        private AssignTeacherToLectureCommand.Handler _sut;

        [SetUp]
        public void Setup()
        {
            _teacherQuery = new Mock<IQuery<Teacher>>();
            _lectureQuery = new Mock<IQuery<Lecture>>();
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _sut = new AssignTeacherToLectureCommand.Handler(_teacherQuery.Object, _lectureQuery.Object, _dbContextMock);
        }

        [Test]
        public void Handle_Throws_ArgumentException_When_Lecture_Is_Not_Found()
        {
            var teacher = new Teacher("name");
            var lecture = new Lecture("name");
            var command = new AssignTeacherToLectureCommand
            {
                LectureId = lecture.Id, 
                TeacherId = teacher.Id
            };

            _teacherQuery.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);

            //Act
            Action result = () => _sut.Handle(command);

            //Assert
            result.Should().Throw<ArgumentException>().WithMessage(nameof(command.LectureId));
        }

        [Test]
        public void Handle_Throws_ArgumentException_When_Teacher_Is_Not_Found()
        {
            var lecture = new Lecture("name");
            var teacher = new Teacher("name");
            var command = new AssignTeacherToLectureCommand
            {
                LectureId = lecture.Id,
                TeacherId = teacher.Id
            };

            _lectureQuery.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Lecture>(lecture.Id))).Returns(lecture);

            //Act
            Action result = () => _sut.Handle(command);

            //Assert
            result.Should().Throw<ArgumentException>().WithMessage(nameof(command.TeacherId));
        }

        [Test]
        public void Handle_Does_Not_Assign_Teacher_To_Lecture_And_Returns_False_When_Lecture_Status_Is_Archived()
        {
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();

            var teacher = new Teacher("name");
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};
            //
            _teacherQuery.Setup(x => x.GetSingleOrDefault(It.Is<HasIdSpec<Teacher>>(x => x.Id == teacher.Id))).Returns(teacher); 
            _lectureQuery.Setup(x => x.GetSingleOrDefault(It.Is<HasIdSpec<Lecture>>(x => x.Id == lectureMockId))).Returns(lectureMock.Object);
            _lectureQuery.Setup(x => x.GetAll(It.IsAny<Specification<Lecture>>())).Returns(new List<Lecture>() {lectureMock.Object});
            lectureMock.Setup(x => x.Status).Returns(LectureStatus.Archived);

            //Act
            var c = command;
            var result = _sut.Handle(command);

            //Assert
            lectureMock.Verify(x => x.AssignTeacher(It.IsAny<Teacher>()), Times.Never);
            result.isSuccess.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Key == "alert");
            result.Errors.Should().Contain(x => x.Error == "Can not assign, lecture is archived.");
        }

        [Test]
        public void Handle_Does_Not_Assign_Teacher_To_Lecture_And_Returns_False_When_Teacher_Has_Lecture_Assigned()
        {
            var teacher = new Teacher("name");
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};

            _teacherQuery.Setup(x => x.GetSingleOrDefault(It.Is<HasIdSpec<Teacher>>(x => x.Id == teacher.Id))).Returns(teacher);
            _lectureQuery.Setup(x => x.GetSingleOrDefault(It.Is<HasIdSpec<Lecture>>(x => x.Id == lectureMockId))).Returns(lectureMock.Object);
            _lectureQuery.Setup(x => x.GetAll(It.IsAny<Specification<Lecture>>())).Returns(new List<Lecture>() {lectureMock.Object});
            // lectureMock.Setup(x => x.Id).Returns(lectureMockId);

            //Act
            var result = _sut.Handle(command);

            //Assert
            // result.Should().BeFalse();
            teacher.Should().Be(teacher);
            lectureMock.Verify(x => x.AssignTeacher(It.IsAny<Teacher>()), Times.Never);
            result.Errors.Should().Contain(x => x.Key == "alert");
            result.Errors.Should().Contain(x => x.Error == "Can not assign, teacher has assigned to a lecture.");
        }

        [TestCase(LectureStatus.Open)]
        [TestCase(LectureStatus.Closed)]
        public void Handle_Assigns_Teacher_To_Lecture_And_Returns_True_When_Lecture_Status_Not_Archived(LectureStatus lectureStatus)
        {
            var teacher = new Teacher("name");
            var lectureMock = new Mock<Lecture>();
            var lectureMockId = Guid.NewGuid();
            var command = new AssignTeacherToLectureCommand() {LectureId = lectureMockId, TeacherId = teacher.Id};

            _teacherQuery.Setup(x => x.GetSingleOrDefault(It.Is<HasIdSpec<Teacher>>(x => x.Id == teacher.Id))).Returns(teacher);
            _lectureQuery.Setup(x => x.GetSingleOrDefault(It.Is<HasIdSpec<Lecture>>(x => x.Id == lectureMockId))).Returns(lectureMock.Object);
            _lectureQuery.Setup(x => x.GetAll(It.IsAny<Specification<Lecture>>())).Returns(new List<Lecture>() {});
            lectureMock.Setup(x => x.Status).Returns(lectureStatus);
            
            //Act
            var result = _sut.Handle(command);
            
            //Assert
            result.isSuccess.Should().BeTrue();
            lectureMock.Verify(x => x.AssignTeacher(It.Is<Teacher>(x => x.Id == teacher.Id)), Times.Once);
        }
    }
}