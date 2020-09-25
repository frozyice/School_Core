using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Teachers;
using School_Core.ViewModels.Lecture;
using School_Core.ViewModels.Teacher;

namespace TestingTests.ViewModels
{
    public class TeacherAssignToLectureViewModelProviderTests
    {
        private TeacherAssignToLectureViewModel.Provider _sut;
        private Teacher _teacher;
        private Lecture _lecture;
        private LectureViewModel _lectureViewModel;
        private Mock<LectureViewModel.IProvider> _lectureViewModelProvider;
        private IEnumerable<LectureViewModel> _lectureViewModels;
        private List<LectureViewModel> _lectureViewModelsList;

        [SetUp]
        public void Setup()
        {
            _lectureViewModelProvider = new Mock<LectureViewModel.IProvider>();
            _sut = new TeacherAssignToLectureViewModel.Provider(_lectureViewModelProvider.Object);

            _teacher = new Teacher("name");
            
            _lecture = new Lecture("name");
            
            
            _lectureViewModel = new LectureViewModel()
            {
                Id = _lecture.Id,
                Name = _lecture.Name,
                Status = _lecture.Status
            };
            _lectureViewModelsList = new List<LectureViewModel>()
            {
                _lectureViewModel
            };
            _lectureViewModels = _lectureViewModelsList.AsEnumerable(); 
        }
        
        [Test]
        public void Provide_Returns_ViewModel_When_Id_Is_Passed()
        {
            _lectureViewModelProvider.Setup(x => x.Provide()).Returns(_lectureViewModels);
            
            //Act
            var result = _sut.Provide(_teacher.Id);
            
            //Assert
            result.TeacherId.Should().Be(_teacher.Id);
            
            result.Lectures.Should().NotBeNull();
            result.Lectures.Should().Contain(_lectureViewModel);
        }
    }
}