using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using School_Core.Commands;
using School_Core.Commands.Teachers;
using School_Core.Controllers;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;
using School_Core.ViewModels.Teachers;

namespace TestingTests.Controllers
{
    public class TeacherControllerTests
    {
        private Mock<Messages> _messagesMock;
        private Mock<TeacherListViewModel.IProvider> _teacherProviderMock;
        private Mock<IQuery<Teacher>> _teacherQueryMock;
        private Mock<TeacherAssignToLectureViewModel.IProvider> _viewmodelProviderMock;
        private Mock<IQuery<Lecture>> _lectureQueryMock;
        private TeacherController _sut;

        [SetUp]
        public void Setup()
        {
            _messagesMock = new Mock<Messages>();
            _teacherProviderMock = new Mock<TeacherListViewModel.IProvider>();
            _teacherQueryMock = new Mock<IQuery<Teacher>>();
            _viewmodelProviderMock = new Mock<TeacherAssignToLectureViewModel.IProvider>();
            _lectureQueryMock = new Mock<IQuery<Lecture>>();
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
            var viewModel = new TeacherAssignToLectureViewModel
            {
                TeacherId = teacher.Id, 
                LectureId = lectureId
            };

            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewModel);
            _teacherQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);

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
            var viewModel = new TeacherAssignToLectureViewModel
            {
                TeacherId = teacher.Id,
                LectureId = lectureId
            };
            _teacherQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);

            //Act
            var result = (NotFoundResult) _sut.AssignToLecture(viewModel);

            //Assert
            _messagesMock.Verify(x => x.Dispatch(It.Is<AssignTeacherToLectureCommand>(x => x.LectureId == lectureId && x.TeacherId == teacher.Id)), Times.Never);
        }

        [Test]
        public void AssignToLecture_PostMethod_Returns_NotFound_When_Teacher_Is_Not_Found()
        {
            var teacherId = Guid.NewGuid();
            var lecture = new Lecture("name");
            var viewModel = new TeacherAssignToLectureViewModel
            {
                TeacherId = teacherId,
                LectureId = lecture.Id
            };
            _lectureQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Lecture>(lecture.Id))).Returns(lecture);

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
            var viewModel = new TeacherAssignToLectureViewModel
            {
                TeacherId = teacher.Id,
                LectureId = lecture.Id
            };
            _lectureQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Lecture>(lecture.Id))).Returns(lecture);
            _teacherQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);
            var commandResult = Result.Success();
            _messagesMock.Setup(x => x.Dispatch(It.IsAny<ICommand>())).Returns(commandResult);
            
            var tempDataMock = new Mock<ITempDataDictionary>();
            _sut.TempData = tempDataMock.Object;

            //Act
            var result = (RedirectToActionResult) _sut.AssignToLecture(viewModel);
            

            //Assert
            _messagesMock.Verify(x => x.Dispatch(It.Is<AssignTeacherToLectureCommand>(x => x.LectureId == lecture.Id && x.TeacherId == teacher.Id)), Times.Once);
            tempDataMock.Verify(x=> x.Add("redirectWithSuccess", true));
            result.ActionName.Should().Be(nameof(_sut.List));
        }

        //ShouldAddTempInfo meetodi sisse 1. ei taha tulla
        [Test]
        public void AssignToLecture_Returns_Viewmodel_With_TempDummyVal_Set_By_Viewmodel_Provider_When_ShouldAddTempInfo_Returns_False()
        {
            var sutMock = new Mock<TeacherController>
            (
                _messagesMock.Object,
                _teacherProviderMock.Object,
                _teacherQueryMock.Object,
                _viewmodelProviderMock.Object,
                _lectureQueryMock.Object
            ) {CallBase = true};
            
            var teacher = new Teacher("name");
            _teacherQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);
            
            var viewmodel = new TeacherAssignToLectureViewModel();
            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewmodel);
            
            sutMock.Setup(x => x.ShouldAddTempInfo(null)).Returns(false);

            //Act
            var result = (ViewResult) sutMock.Object.AssignToLecture(teacher.Id, null);

            // Assert
            var model = (TeacherAssignToLectureViewModel) result.Model;
            model.TempDummyVal.Should().Be("missing");
            model.Should().BeSameAs(viewmodel);
        }

        //ShouldAddTempInfo meetodi sisse 1. ei taha tulla
        [Test]
        public void AssignToLecture_Returns_Viewmodel_With_TempDummyVal_Set_By_AssignToLecture_When_ShouldAddTempInfo_Returns_True()
        {
            var sutMock = new Mock<TeacherController>
            (
                _messagesMock.Object,
                _teacherProviderMock.Object,
                _teacherQueryMock.Object,
                _viewmodelProviderMock.Object,
                _lectureQueryMock.Object
            ) {CallBase = true};
            
            var teacher = new Teacher("name");
            _teacherQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);
            
            var viewmodel = new TeacherAssignToLectureViewModel();
            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewmodel);

            var info = "smt";
            sutMock.Setup(x => x.ShouldAddTempInfo(info)).Returns(true);

            //Act
            var result = (ViewResult) sutMock.Object.AssignToLecture(teacher.Id, info);

            //Assert
            var model = (TeacherAssignToLectureViewModel) result.Model;
            model.TempDummyVal.Should().Be("important thing can not do in provider for some stupid reason");
            model.Should().BeSameAs(viewmodel);
        }
        
        //ShouldAddTempInfo meetodi sisse 2. tuleme
        [TestCase(null)]
        [TestCase(" ")]
        public void AssignToLecture_Returns_Viewmodel_With_TempDummyVal_Set_By_Viewmodel_Provider_When_ShouldAddTempInfo_Returns_False_ver2(string info)
        {
            var sutMock = new Mock<TeacherController>
            (
                _messagesMock.Object,
                _teacherProviderMock.Object,
                _teacherQueryMock.Object,
                _viewmodelProviderMock.Object,
                _lectureQueryMock.Object
            ) {CallBase = true};
            
            var teacher = new Teacher("name");
            _teacherQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);
            
            var viewmodel = new TeacherAssignToLectureViewModel();
            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewmodel);

            //Act
            var result = (ViewResult) sutMock.Object.AssignToLecture(teacher.Id, info);

            //Assert
            var model = (TeacherAssignToLectureViewModel) result.Model;
            model.TempDummyVal.Should().Be("missing");
            model.Should().BeSameAs(viewmodel);
        }
        
        //ShouldAddTempInfo meetodi sisse 2. tuleme
        [Test]
        public void AssignToLecture_Returns_Viewmodel_With_TempDummyVal_Set_By_AssignToLecture_When_ShouldAddTempInfo_Returns_True_ver2()
        {
            var sutMock = new Mock<TeacherController>
            (
                _messagesMock.Object,
                _teacherProviderMock.Object,
                _teacherQueryMock.Object,
                _viewmodelProviderMock.Object,
                _lectureQueryMock.Object
            ) {CallBase = true};

            var teacher = new Teacher("name");
            _teacherQueryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Teacher>(teacher.Id))).Returns(teacher);
            
            var viewmodel = new TeacherAssignToLectureViewModel();
            _viewmodelProviderMock.Setup(x => x.Provide(teacher.Id)).Returns(viewmodel);

            var info = "smt";

            //Act
            var result = (ViewResult) sutMock.Object.AssignToLecture(teacher.Id, info);

            // Assert
            var model = (TeacherAssignToLectureViewModel) result.Model;
            model.TempDummyVal.Should().Be("important thing can not do in provider for some stupid reason");
            model.Should().BeSameAs(viewmodel);
        }

        //ShouldAddTempInfo meetodi sisse 3. testime selle otse 
        [TestCase(null)]
        [TestCase(" ")]
        public void ShouldAddTempInfo_Returns_False(string info)
        {
            // Act
            var result = _sut.ShouldAddTempInfo(info);

            // Assert
            result.Should().BeFalse();
        }

        //ShouldAddTempInfo meetodi sisse 3. testime selle otse 
        [Test]
        public void ShouldAddTempInfo_Returns_True()
        {
            // Act
            var result = _sut.ShouldAddTempInfo("smt");

            //Assert
            result.Should().BeTrue();
        }
    }
}