using Moq;
using NUnit.Framework;
using School_Core.Domain.Models;
using School_Core.Repositories;
using School_Core.ViewModels.Student;

namespace TestingTests
{
    public class StudentAddNewViewModelMapperTests
    {
        private Mock<IStudentRepository> _mockStudentRepository;
        public StudentAddNewViewModel.Mapper _sut;

        [SetUp]
        public void Setup()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            //_mockStudentRepository.Setup(x => x.GetStudentByName("NameInDB")).Returns(new Student("NameInDB", null));
            //mockStudentRepository.Setup(x => x.GetStudentByName("NameNotDB"));//.Returns<Student>(null);
            //system under test
            _sut = new StudentAddNewViewModel.Mapper(_mockStudentRepository.Object);

        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("     ")]
        [TestCase("")]
        public void Validate_Returns_Error_Messages_When_StudentName_Is_NullOrWhiteSpace(string studentName)
        {
            //Arrange
            string expected = "Student name is required.";
            //Act
            string result = _sut.Validate(studentName);

            //Assert
            Assert.That(expected, Is.EqualTo(result));

        }

        [Test]
        public void Validate_Returns_Error_Message_When_Student_Is_Already_In_Database()
        {
            var expected = "Student with same name exists.";
            _mockStudentRepository.Setup(x => x.GetStudentByName("NameInDB")).Returns(new Student("NameInDB"));

            // Act
            var result = _sut.Validate("NameInDB");

            // Assert
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void Validate_Returns_Null_When_Student_Is_Not_In_Database()
        {
            string expected = null;

            var result = _sut.Validate("NameNotDB");

            Assert.That(expected, Is.EqualTo(result));
        }

        [TestCase("XXXXXXXXXXX")]
        [TestCase("X")]
        public void Validate_Returns_Error_Message_When_StudentName_Length_Is_Not_2_To_10_Characters(string studentName)
        {
            string expected = "Student name should be min 2 and max 10 characters.";

            var result = _sut.Validate(studentName);

            Assert.That(expected, Is.EqualTo(result));
        }
               
        [TestCase("ÄÄÄÄÄÄ")]
        [TestCase("ääääää")]
        [TestCase("ÖÖÖÖÖÖ")]
        [TestCase("öööööö")]
        [TestCase("111111")]
        [TestCase("ÄäÖö1")]
        public void Validate_Returns_Error_When_StudentName_Contains_Special_Characters(string studentName)
        {
            string expected = "Student name should not contain Ä,Ö or numbers.";

            var result = _sut.Validate(studentName);

            //Assert.AreEqual(expected, result);
            Assert.That(expected, Is.EqualTo(result));
        }

    }

    
}
