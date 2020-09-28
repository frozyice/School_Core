using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using School_Core.Commands.Teacher;
using School_Core.Controllers;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Util;
using School_Core.ViewModels.Teacher;

namespace TestingTests.Controllers
{
    public class TeacherControllerTests
    {
        private Mock<Messages> _messagesMock;
        private Mock<TeacherListViewModel.IProvider> _teacherProviderMock;
        private Mock<ITeacherQuery> _teacherQueryMock;
        private Mock<TeacherAssignToLectureViewModel.IProvider> _viewmodelProviderMock;
        private Mock<ILectureQuery> _lectureQueryMock;
        private TeacherController _sut;

        [SetUp]
        public void Setup()
        {
            _messagesMock = new Mock<Messages>();
            _teacherProviderMock = new Mock<TeacherListViewModel.IProvider>();
            _teacherQueryMock = new Mock<ITeacherQuery>();
            _viewmodelProviderMock = new Mock<TeacherAssignToLectureViewModel.IProvider>();
            _lectureQueryMock = new Mock<ILectureQuery>();
            _sut = new TeacherController(_messagesMock.Object, _teacherProviderMock.Object, _teacherQueryMock.Object, _viewmodelProviderMock.Object, _lectureQueryMock.Object);
        }

        [Test]
        public void AssignToLecture_GetMethod_Returns_NotFound_When_Teacher_Is_Not_Found()
        {
            var teacherId = Guid.NewGuid();

            //Act && Assert
            var result = (NotFoundResult) _sut.AssignToLecture(teacherId);
        }

        [Test]
        public void AssignToLecture_GetMethod_Returns_View_With_ViewModel()
        {
            var teacher = new Teacher("name");
            var lectureId = Guid.NewGuid();
            var viewModel = new TeacherAssignToLectureViewModel() {TeacherId = teacher.Id, LectureId = lectureId};

            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewModel);
            _teacherQueryMock.Setup(x => x.Get(teacher.Id)).Returns(teacher);

            //Act
            var result = (ViewResult) _sut.AssignToLecture(teacher.Id);

            //Assert
            var model = result.Model;
            Assert.That(model, Is.EqualTo(viewModel));
        }

        [Test]
        public void AssignToLecture_PostMethod_Returns_NotFound_When_Lecture_Is_Not_Found()
        {
            var teacher = new Teacher("name");
            var lectureId = Guid.NewGuid();
            var viewModel = new TeacherAssignToLectureViewModel() {TeacherId = teacher.Id, LectureId = lectureId};
            _teacherQueryMock.Setup(x => x.Get(teacher.Id)).Returns(teacher);

            //Act
            var result = (NotFoundResult) _sut.AssignToLecture(viewModel);

            //Assert
            _messagesMock.Verify(x => x.Dispatch(It.Is<AssignTeacherToLectureCommand>(x => x.LectureId == lectureId && x.TeacherId == teacher.Id)), Times.Never);
        }

        [Test]
        public void AssignToLecture_PostMethod_Returns__NotFound_When_Teacher_Is_Not_Found()
        {
            var teacherId = Guid.NewGuid();
            var lecture = new Lecture("name");
            var viewModel = new TeacherAssignToLectureViewModel() {TeacherId = teacherId, LectureId = lecture.Id};
            _lectureQueryMock.Setup(x => x.Get(lecture.Id)).Returns(lecture);

            //Act
            var result = (NotFoundResult) _sut.AssignToLecture(viewModel);

            //Assert
            _messagesMock.Verify(x => x.Dispatch(It.Is<AssignTeacherToLectureCommand>(x => x.LectureId == lecture.Id && x.TeacherId == teacherId)), Times.Never);
        }

        [Test]
        public void AssignToLecture_PostMethod_Assigns_Teacher_To_Lecture()
        {
            var teacher = new Teacher("name");
            var lecture = new Lecture("name");
            var viewModel = new TeacherAssignToLectureViewModel() {TeacherId = teacher.Id, LectureId = lecture.Id};
            _lectureQueryMock.Setup(x => x.Get(lecture.Id)).Returns(lecture);
            _teacherQueryMock.Setup(x => x.Get(teacher.Id)).Returns(teacher);

            //Act
            var result = (RedirectToActionResult) _sut.AssignToLecture(viewModel);

            //Assert
            _messagesMock.Verify(x => x.Dispatch(It.Is<AssignTeacherToLectureCommand>(x => x.LectureId == lecture.Id && x.TeacherId == teacher.Id)), Times.Once);
        }

        [Test]
        //ShouldAddTempInfo meetodi sisse 1. ei taha tulla
        public void ShouldAddTempInfo_ver1()
        {
            var sut = new Mock<TeacherController>
            (
                _messagesMock.Object,
                _teacherProviderMock.Object,
                _teacherQueryMock.Object,
                _viewmodelProviderMock.Object,
                _lectureQueryMock.Object
            ) {CallBase = true};

            var teacher = new Teacher("name");
            var lecture = new Lecture("name");
            _lectureQueryMock.Setup(x => x.Get(lecture.Id)).Returns(lecture);
            _teacherQueryMock.Setup(x => x.Get(teacher.Id)).Returns(teacher);

            var viewModel = new TeacherAssignToLectureViewModel() {TeacherId = teacher.Id, LectureId = lecture.Id};
            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewModel);
            var info = "smt";
            sut.Setup(x => x.ShouldAddTempInfo(info)).Returns(!string.IsNullOrWhiteSpace(info));

            //Act
            var result = (ViewResult) sut.Object.AssignToLecture(teacher.Id, info);

            // Assert
            sut.Verify(x => x.ShouldAddTempInfo(info), Times.Once);
        }

        [Test]
        //ShouldAddTempInfo meetodi sisse 2. tuleme
        public void ShouldAddTempInfo_ver2()
        {
            var sut = new Mock<TeacherController>
            (
                _messagesMock.Object,
                _teacherProviderMock.Object,
                _teacherQueryMock.Object,
                _viewmodelProviderMock.Object,
                _lectureQueryMock.Object
            ) {CallBase = true};

            var teacher = new Teacher("name");
            var lecture = new Lecture("name");
            _lectureQueryMock.Setup(x => x.Get(lecture.Id)).Returns(lecture);
            _teacherQueryMock.Setup(x => x.Get(teacher.Id)).Returns(teacher);

            var viewModel = new TeacherAssignToLectureViewModel() {TeacherId = teacher.Id, LectureId = lecture.Id};
            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewModel);
            var info = "smt";

            //Act
            var result = (ViewResult) sut.Object.AssignToLecture(teacher.Id, info);

            // Assert
            sut.Verify(x => x.ShouldAddTempInfo(info), Times.Once);
        }

        [Test]
        //ShouldAddTempInfo meetodi sisse 3. testime selle otse 
        public void ShouldAddTempInfo_ver3()
        {
            var sut = new Mock<TeacherController>
            (
                _messagesMock.Object,
                _teacherProviderMock.Object,
                _teacherQueryMock.Object,
                _viewmodelProviderMock.Object,
                _lectureQueryMock.Object
            ) {CallBase = true};

            var teacher = new Teacher("name");
            var lecture = new Lecture("name");
            _lectureQueryMock.Setup(x => x.Get(lecture.Id)).Returns(lecture);
            _teacherQueryMock.Setup(x => x.Get(teacher.Id)).Returns(teacher);

            var viewModel = new TeacherAssignToLectureViewModel() {TeacherId = teacher.Id, LectureId = lecture.Id};
            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewModel);
            var info = "smt";

            //Act
            var result = sut.Object.ShouldAddTempInfo(info);

            // Assert
            sut.Verify(x => x.ShouldAddTempInfo(info), Times.Once);
        }
    }
}