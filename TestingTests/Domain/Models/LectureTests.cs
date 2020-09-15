using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using School_Core.Domain.Models;
using School_Core.Domain.Models.Students;

namespace TestingTests.Domain.Models
{
    public class LectureTests
    {
        private Lecture _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Lecture("name");
        }

        [Test]
        public void CloseLectureEnrollment_Sets_StatusToClosed_When_StatusIsOpen()
        {
            //Act
            _sut.CloseLecture();

            //Assert
            var result = _sut.Status;
            Assert.That(result, Is.EqualTo(LectureStatus.Closed));
        }

        [Test]
        public void CloseLectureEnrollment_DoesNotSet_StatusToClosed_When_StatusIsArchived()
        {
            _sut.ArchiveLecture();

            //Act
            _sut.CloseLecture();

            //Assert
            var result = _sut.Status;
            Assert.That(result, Is.EqualTo(LectureStatus.Archived));
        }

        [Test]
        public void ArchiveLecture_Sets_StatusToArchived_When_StatusIsOpen()
        {
            //Act
            _sut.ArchiveLecture();

            //Assert
            var result = _sut.Status;
            Assert.That(result, Is.EqualTo(LectureStatus.Archived));
        }

        [Test]
        public void ArchiveLecture_Sets_Status_To_Archived_When_StatusIsClosed()
        {
            _sut.CloseLecture();

            //Act
            _sut.ArchiveLecture();

            //Assert
            var result = _sut.Status;
            Assert.That(result, Is.EqualTo(LectureStatus.Archived));
        }

        [Test]
        public void ArchiveLecture_DoesNotSet_StatusToArchived_When_There_Are_Enrolments_With_GradeNone()
        {
            var sut = new Mock<Lecture>();
            var enrollments = new List<Enrollment>()
            {
                new Enrollment(Guid.NewGuid(), Grade.None)
            };
            sut.Setup(x => x.Enrollments).Returns(enrollments);

            //Act
            sut.Object.ArchiveLecture();

            //Assert
            var result = sut.Object.Status;
            Assert.That(result, Is.EqualTo(LectureStatus.Open));
        }

        [Test]
        public void CanArchive_Returns_False_When_There_Are_Enrolments_With_GradeNone()
        {
            var sut = new Mock<Lecture>();
            var enrollments = new List<Enrollment>()
            {
                new Enrollment(Guid.NewGuid(), Grade.A),
                new Enrollment(Guid.NewGuid(), Grade.None)
            };
            sut.Setup(x => x.Enrollments).Returns(enrollments);

            //Act
            var result = sut.Object.CanArchive();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CanArchive_Returns_True_When_There_Are_No_Enrolments_With_Grade_None()
        {
            var sut = new Mock<Lecture>();
            var enrollments = new List<Enrollment>()
            {
                new Enrollment(Guid.NewGuid(), Grade.A),
                new Enrollment(Guid.NewGuid(), Grade.B),
                new Enrollment(Guid.NewGuid(), Grade.C),
                new Enrollment(Guid.NewGuid(), Grade.D),
                new Enrollment(Guid.NewGuid(), Grade.E),
                new Enrollment(Guid.NewGuid(), Grade.F),
            };
            
            sut.Setup(x => x.Enrollments).Returns(enrollments);

            //Act
            var result = sut.Object.CanArchive();

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void EnrollStudent_Adds_Enrollment_When_LectureStatus_Is_Open()
        {
            var student = new Student("name");
            var studentId = student.Id;

            //Act
            _sut.EnrollStudent(studentId);
            
            //Assert
            var result = _sut.Enrollments.FirstOrDefault(x => x.StudentId == studentId);
            Assert.That(result, Is.Not.Null);
            //Assert.That(result.LectureId,Is.EqualTo(_sut.Id)); //todo?
            Assert.That(result.StudentId,Is.EqualTo(studentId));
        }

        [Test]
        public void EnrollStudent_Do_Not_Add_Enrollment_When_LectureStatus_Is_Closed()
        {
            var student =  TestStudent.Create();
            var studentId = student.Id;
            _sut.CloseLecture();

            //Act
            _sut.EnrollStudent(studentId);
            
            //Assert
            var result = _sut.Enrollments.FirstOrDefault(x => x.StudentId == studentId);
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void EnrollStudent_Do_Not_Add_Enrollment_When_LectureStatus_Is_Archived()
        {
            var student =  TestStudent.Create();
            var studentId = student.Id;
            _sut.ArchiveLecture();

            //Act
            _sut.EnrollStudent(studentId);
            
            //Assert
            var result = _sut.Enrollments.FirstOrDefault(x => x.StudentId == studentId);
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void EnrollStudent_Does_Not_Add_Enrollment_When_There_Is_Student_With_Same_Id_Enrolled()
        {
            var student = TestStudent.Create();
            
            
            var studentId = student.Id;
            _sut.EnrollStudent(studentId);

            //Act
            _sut.EnrollStudent(studentId);
            
            //Assert
            var enrollment = _sut.Enrollments.Single(x => x.StudentId == studentId);
            Assert.That(enrollment.StudentId, Is.EqualTo(studentId));
            Assert.That(enrollment.LectureId, Is.EqualTo(_sut.Id));
            
            
            //var result = _sut.Enrollments.FirstOrDefault(x => x.StudentId == studentId);
            
        }
        
        [Test]
        public void EnrollStudent_Throws_ArgumentException_When_Empty_Guid_Is_Passed()
        {

            var guid = Guid.Empty;
            
            //Act
            var ex = Assert.Throws<ArgumentException>(() => _sut.EnrollStudent(guid));

            //Assert
            Assert.That(ex.Message, Is.EqualTo($"{guid} :  is not valid studentId"));
        }

        [Test]
        public void CanEnroll_Returns_False_When_Student_Is_Not_Allowed_To_Enroll()
        {
            var student = new TestStudent();
            throw new NotImplementedException();
        }
    }

    public class TestStudent
    {
        public static Student Create(string name = "testName", int yearsOFStudy = 1, List<Enrollment> enrollments = null)
        {
            var student = new Student(name,yearsOFStudy);
            return student;
        }
    }
}