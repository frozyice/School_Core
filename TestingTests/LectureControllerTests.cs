using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using School_Core.Controllers;
using School_Core.ViewModels.Lecture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingTests
{
    class LectureControllerTests
    {
        public LectureController _sut;
        private Mock<LectureAddStudentViewModel.IProvider> _mockLectureAddStudentProvider;
        private Mock<LectureAddStudentViewModel.IMapper> _mockLectureAddStudentMapper;


        [SetUp]
        public void Setup()
        {
            _mockLectureAddStudentProvider = new Mock<LectureAddStudentViewModel.IProvider>();
            _mockLectureAddStudentMapper = new Mock<LectureAddStudentViewModel.IMapper>();
        }

        //https://stackoverflow.com/questions/22561834/asp-net-mvc-controller-post-method-unit-test-modelstate-isvalid-always-true

        //[Test]
        //public void AddStudent_ModelState_Is_Not_valid_When_ModelState_Has_Error()
        //{
        //    var viewModel = new LectureAddStudentViewModel();
        //    _mockLectureAddStudentMapper.Setup(x => x.Validate(It.IsAny<Guid>(), It.IsAny<string>())).Returns(new List<string>());
        //    _sut.ModelState.AddModelError(string.Empty, "error");


        //    var result = _sut.AddStudent(viewModel);
        //    var viewResult = result as ViewResult;
        //    var isModelStateValid = viewResult.ViewData.ModelState.IsValid;
        //    var errorCount = viewResult.ViewData.ModelState.ErrorCount;


        //    Assert.That(errorCount, Is.EqualTo(1));
        //    Assert.That(isModelStateValid, Is.False);
        //}
        [Test]
        public void AddStudent_Returns_ViewResult_When_Modelstate_Is_Not_Valid()
        {
            var modelId = Guid.NewGuid();
            var parameterModel = new LectureAddStudentViewModel();
            parameterModel.Id = modelId;

            _mockLectureAddStudentMapper.Setup(x => x.Validate(modelId, It.IsAny<string>())).Returns(new List<string>());
            _sut.ModelState.AddModelError(string.Empty, "error");


            // Act
            var result = (ViewResult)_sut.EnrollStudent(parameterModel);

            // Assert
            var model = (LectureAddStudentViewModel)result.ViewData.Model;
            Assert.That(model.Id, Is.EqualTo(parameterModel.Id));

            //Assert.That(isModelStateValid, Is.False);
        }



        //https://laptrinhx.com/how-to-get-all-errors-from-asp-net-mvc-modelstate-252836070/
        [Test]
        public void AddStudent_Returns_ViewResult_When_Validate_Has_Errors()
        {
            var viewModel = new LectureAddStudentViewModel();

            var errors = new List<string>() { "error1", "error2", "error3" };
            _mockLectureAddStudentMapper.Setup(x => x.Validate(It.IsAny<Guid>(), It.IsAny<string>())).Returns(errors);


            var result = _sut.EnrollStudent(viewModel);

            var viewResult = result as ViewResult;
            //var modelErrors = _sut.ViewData.ModelState.ContainsKey("StudentName");
            var errorsOut = new List<string>();
            var modelstateValues = _sut.ViewData.ModelState.Values;
            foreach (var modelstate in modelstateValues)
            {
                foreach (var error in modelstate.Errors)
                {
                    errorsOut.Add(error.ErrorMessage);
                }
            }

            var mErrors2 = _sut.ViewData.ModelState["StudentName"];


            Assert.That(mErrors2.Errors.FirstOrDefault().ErrorMessage, Is.EqualTo("error1"));
            Assert.That(mErrors2.Errors.FirstOrDefault().ErrorMessage, Is.EqualTo("error2"));
            Assert.That(mErrors2.Errors.FirstOrDefault().ErrorMessage, Is.EqualTo("error3"));

        }

        [Test]
        public void AddStudent_AddsStudentToLecture_When_Modelstate_Is_Valid()
        {
            var viewModel = new LectureAddStudentViewModel();
            _mockLectureAddStudentMapper.Setup(x => x.Validate(It.IsAny<Guid>(), It.IsAny<string>())).Returns(new List<string>());
            _mockLectureAddStudentMapper.Setup(x => x.AddStudentToLecture(viewModel)).Returns(null);


            var result = _sut.EnrollStudent(viewModel);


            //Assert.That(isModelStateValid, Is.True);
            _mockLectureAddStudentMapper.Verify(x => x.AddStudentToLecture(viewModel), Times.Once());
        }



    }
}
