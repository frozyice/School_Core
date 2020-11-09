using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using School_Core.API.DTOs;
using School_Core.Controllers;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;
using School_Core.ViewModels.Students;

namespace TestingTests.Controllers
{
    public class StudentControllerTests
    {
        private Mock<IQuery<Student>> _queryMock;
        private Mock<IDefaultHttpClient> _httpClientMock;
        private Mock<StudentListViewModel.IProvider> _studentListViewModelProviderMock;
        private Mock<StudentMedicalViewModel.IProvider> _medicalViewModelProviderMock;
        private StudentController _sut;

        [SetUp]
        public void Setup()
        {
            _queryMock = new Mock<IQuery<Student>>();
            _httpClientMock = new Mock<IDefaultHttpClient>();
            _studentListViewModelProviderMock = new Mock<StudentListViewModel.IProvider>();
            _medicalViewModelProviderMock = new Mock<StudentMedicalViewModel.IProvider>();
            _sut = new StudentController
            (
                _queryMock.Object,
                _httpClientMock.Object,
                _studentListViewModelProviderMock.Object,
                _medicalViewModelProviderMock.Object
            );
        }

        [Test]
        public void Medical_Throws_ArgumentException_When_Student_Is_Not_Found()
        {
            //Act
            Func<Task> act = async () => { await _sut.Medical(Guid.NewGuid()); };

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("studentId");
        }

        [Test]
        public async Task Medical_Returns_View_With_ViewModel_And_Not_Valid_ModelState_When_Request_Fails()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.ServiceUnavailable};
            _httpClientMock.Setup(x => x.GetAsync($"medical/student/{student.Id}")).ReturnsAsync(response);

            var model = new StudentMedicalViewModel
            {
                StudentName = student.Name,
                StudentId = student.Id,
                Medicals = new List<MedicalReadDto>(),
                WriteDto = new MedicalWriteDto()
            };
            _medicalViewModelProviderMock.Setup(x => x.Provide(student.Id, new List<MedicalReadDto>(), false)).Returns(model);

            //Act
            var result = await _sut.Medical(student.Id);

            //Assert
            var viewResult = (ViewResult) result;
            var exceptedModel = new StudentMedicalViewModel
            {
                StudentName = student.Name,
                StudentId = student.Id,
                Medicals = new List<MedicalReadDto>(),
                WriteDto = new MedicalWriteDto()
            };
            _sut.ViewData.ModelState.IsValid.Should().BeFalse();
            var errors = _sut.ModelState.Values.SelectMany(x => x.Errors).ToList().Select(x => x.ErrorMessage).ToList();
            errors.Should().Contain("Request failed successfully!");
            viewResult.Model.Should().BeEquivalentTo(exceptedModel);
        }

        [Test]
        public async Task Medical_Returns_View_With_ViewModel_When_Request_Is_Successful()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var medicalId = Guid.NewGuid();
            var medicals = new List<MedicalReadDto> {new MedicalReadDto() {Id = medicalId, StudentId = student.Id, Active = "TestString:Yes", Reason = "test:Reason"}};
            var serializedObject = JsonConvert.SerializeObject(medicals);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(serializedObject);
            _httpClientMock.Setup(x => x.GetAsync($"medical/student/{student.Id}")).ReturnsAsync(response);

            var tempDataMock = new Mock<ITempDataDictionary>();
            _sut.TempData = tempDataMock.Object;

            var model = new StudentMedicalViewModel
            {
                StudentName = student.Name,
                StudentId = student.Id,
                Medicals = medicals
            };
            _medicalViewModelProviderMock.Setup(x => x.Provide(student.Id, It.Is<List<MedicalReadDto>>(x => x.Single().Id == medicalId), false)).Returns(model);

            //Act
            var result = (ViewResult) await _sut.Medical(student.Id);

            //Assert
            var expectedModel = new StudentMedicalViewModel
            {
                StudentName = student.Name,
                StudentId = student.Id,
                Medicals = new List<MedicalReadDto>
                {
                    new MedicalReadDto()
                    {
                        Id = medicalId,
                        StudentId = student.Id,
                        Active = "TestString:Yes",
                        Reason = "test:Reason"
                    }
                }
            };
            result.Model.Should().BeEquivalentTo(expectedModel);
        }

        [Test]
        public void AddMedical_Throws_ArgumentException_When_Student_Is_Not_Found()
        {
            // Act
            Func<Task> act = async () => { await _sut.AddMedical(Guid.NewGuid(), new MedicalWriteDto()); };

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("studentId");
        }

        [Test]
        public async Task AddMedical_Returns_View_With_ViewModel_And_Not_Valid_ModelState_When_Request_Fails()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.ServiceUnavailable};
            var writeDto = new MedicalWriteDto();
            _httpClientMock.Setup(x => x.PostAsync($"medical/student/{student.Id}", writeDto)).ReturnsAsync(response);

            //Act
            var result = (RedirectToActionResult) await _sut.AddMedical(student.Id, writeDto);

            //Assert
            _sut.ModelState.IsValid.Should().BeFalse();
            var errors = _sut.ModelState.Values.SelectMany(x => x.Errors).ToList().Select(x => x.ErrorMessage).ToList();
            errors.Should().Contain("Request failed successfully!");
            result.ActionName.Should().Be(nameof(StudentController.Medical));
            result.RouteValues.Should().Contain("studentId", student.Id);
        }

        [Test]
        public async Task AddMedical_Returns_View_With_ViewModel_When_Request_Is_Successful()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.Created};
            var writeDto = new MedicalWriteDto();
            _httpClientMock.Setup(x => x.PostAsync($"medical/student/{student.Id}", writeDto)).ReturnsAsync(response);

            var tempDataMock = new Mock<ITempDataDictionary>();
            _sut.TempData = tempDataMock.Object;
            tempDataMock.Setup(x => x.Add("redirectWithSuccess", true));

            //Act
            var result = (RedirectToActionResult) await _sut.AddMedical(student.Id, writeDto);

            //Assert
            tempDataMock.Verify(x => x.Add("redirectWithSuccess", true));
            result.ActionName.Should().Be(nameof(StudentController.Medical));
            result.RouteValues.Should().Contain("studentId", student.Id);
        }

        [Test]
        public void EditMedicalReason_Throws_ArgumentException_When_Student_Is_Not_Found()
        {
            // Act
            Func<Task> act = async () => { await _sut.EditMedicalReason(Guid.NewGuid(), Guid.NewGuid(), new MedicalWriteDto()); };

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("studentId");
        }

        [Test]
        public void EditMedicalReason_Throws_ArgumentException_When_Medical_Is_Not_Found()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var medicalId = Guid.NewGuid();
            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.NotFound};
            var writeDto = new MedicalWriteDto();
            _httpClientMock.Setup(x => x.PutAsync($"medical/{medicalId}", writeDto)).ReturnsAsync(response);

            // Act
            Func<Task> act = async () => { await _sut.EditMedicalReason(medicalId, student.Id, writeDto); };

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("medicalId");
        }

        [Test]
        public async Task EditMedicalReason_Returns_View_With_ViewModel_And_Not_Valid_ModelState_When_Request_Fails()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var medicalId = Guid.NewGuid();
            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.ServiceUnavailable};
            var writeDto = new MedicalWriteDto();
            _httpClientMock.Setup(x => x.PutAsync($"medical/{medicalId}", writeDto)).ReturnsAsync(response);

            //Act
            var result = (RedirectToActionResult) await _sut.EditMedicalReason(medicalId, student.Id, writeDto);

            //Assert
            _sut.ModelState.IsValid.Should().BeFalse();
            var errors = _sut.ModelState.Values.SelectMany(x => x.Errors).ToList().Select(x => x.ErrorMessage).ToList();
            errors.Should().Contain("Request failed successfully!");
            result.ActionName.Should().Be(nameof(StudentController.Medical));
            result.RouteValues.Should().Contain("studentId", student.Id);
        }


        [Test]
        public async Task EditMedicalReason_Returns_View_With_ViewModel_When_Request_Is_Successful()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var medicalId = Guid.NewGuid();
            var updateMedical = new MedicalWriteDto
            {
                Reason = "reasonText"
            };
            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.NoContent};
            _httpClientMock.Setup(x => x.PutAsync($"medical/{medicalId}", updateMedical)).ReturnsAsync(response);


            var tempDataMock = new Mock<ITempDataDictionary>();
            _sut.TempData = tempDataMock.Object;
            tempDataMock.Setup(x => x.Add("redirectWithSuccess", true));

            //Act
            var result = (RedirectToActionResult) await _sut.EditMedicalReason(medicalId, student.Id, updateMedical);

            //Assert
            tempDataMock.Verify(x => x.Add("redirectWithSuccess", true));
            result.ActionName.Should().Be(nameof(StudentController.Medical));
            result.RouteValues.Should().Contain("studentId", student.Id);
        }

        [Test]
        public void MarkMedicalNotActive_Throws_ArgumentException_When_Student_Is_Not_Found()
        {
            // Act
            Func<Task> act = async () => { await _sut.MarkMedicalNotActive(Guid.NewGuid(), Guid.NewGuid()); };

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("studentId");
        }

        [Test]
        public void MarkMedicalNotActive_Throws_ArgumentException_When_Medical_Is_Not_Found()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var medicalId = Guid.NewGuid();
            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.NotFound};
            var writeDto = new MedicalWriteDto();
            _httpClientMock.Setup(x => x.DeleteAsync($"medical/{medicalId}")).ReturnsAsync(response);

            // Act
            Func<Task> act = async () => { await _sut.MarkMedicalNotActive(medicalId, student.Id); };

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("medicalId");
        }
        
        [Test]
        public async Task MarkMedicalNotActive_Returns_View_With_ViewModel_And_Not_Valid_ModelState_When_Request_Fails()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var medicalId = Guid.NewGuid();
            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.ServiceUnavailable};
            var writeDto = new MedicalWriteDto();
            _httpClientMock.Setup(x => x.DeleteAsync($"medical/{medicalId}")).ReturnsAsync(response);

            //Act
            var result = (RedirectToActionResult) await _sut.MarkMedicalNotActive(medicalId, student.Id);

            //Assert
            _sut.ModelState.IsValid.Should().BeFalse();
            var errors = _sut.ModelState.Values.SelectMany(x => x.Errors).ToList().Select(x => x.ErrorMessage).ToList();
            errors.Should().Contain("Request failed successfully!");
            result.ActionName.Should().Be(nameof(StudentController.Medical));
            result.RouteValues.Should().Contain("studentId", student.Id);
        }


        [Test]
        public async Task MarkMedicalNotActive_Returns_View_With_ViewModel_When_Request_Is_Successful()
        {
            var student = new Student("name");
            _queryMock.Setup(x => x.GetSingleOrDefault(new HasIdSpec<Student>(student.Id))).Returns(student);

            var medicalId = Guid.NewGuid();
            var updateMedical = new MedicalWriteDto
            {
                Reason = "reasonText"
            };
            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.NoContent};
            _httpClientMock.Setup(x => x.DeleteAsync($"medical/{medicalId}")).ReturnsAsync(response);


            var tempDataMock = new Mock<ITempDataDictionary>();
            _sut.TempData = tempDataMock.Object;
            tempDataMock.Setup(x => x.Add("redirectWithSuccess", true));

            //Act
            var result = (RedirectToActionResult) await _sut.MarkMedicalNotActive(medicalId, student.Id);

            //Assert
            tempDataMock.Verify(x => x.Add("redirectWithSuccess", true));
            result.ActionName.Should().Be(nameof(StudentController.Medical));
            result.RouteValues.Should().Contain("studentId", student.Id);
        }
    }
}