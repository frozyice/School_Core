using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using School_Core.Domain.Models;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Teachers;

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
            var student = new Student("name");
            _sut.EnrollStudent(student);

            //Act
            _sut.ArchiveLecture();

            //Assert
            var result = _sut.Status;
            Assert.That(result, Is.EqualTo(LectureStatus.Open));
        }

        [Test]
        public void CanArchive_Returns_False_When_There_Are_Enrolments_With_GradeNone()
        {
            var student = new Student("name");
            _sut.EnrollStudent(student);

            //Act
            var result = _sut.CanArchive();

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
            _sut.EnrollStudent(student);

            //Assert
            var result = _sut.Enrollments.FirstOrDefault(x => x.StudentId == studentId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StudentId, Is.EqualTo(studentId));
        }

        [Test]
        public void EnrollStudent_Do_Not_Add_Enrollment_When_LectureStatus_Is_Closed()
        {
            var student = TestStudent.Create();
            var studentId = student.Id;
            _sut.CloseLecture();

            //Act
            _sut.EnrollStudent(student);

            //Assert
            var result = _sut.Enrollments.FirstOrDefault(x => x.StudentId == studentId);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnrollStudent_Do_Not_Add_Enrollment_When_LectureStatus_Is_Archived()
        {
            var student = TestStudent.Create();
            var studentId = student.Id;
            _sut.ArchiveLecture();

            //Act
            _sut.EnrollStudent(student);

            //Assert
            var result = _sut.Enrollments.FirstOrDefault(x => x.StudentId == studentId);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnrollStudent_Does_Not_Add_Enrollment_When_There_Is_Student_With_Same_Id_Enrolled()
        {
            var student = new Student("name", 1, StudyField.Law);
            var sut = new Lecture("name", 1, StudyField.Law);
            var studentId = student.Id;
            sut.EnrollStudent(student);

            //Act
            sut.EnrollStudent(student);

            //Assert
            var enrollment = sut.Enrollments.Single(x => x.StudentId == studentId);
            Assert.That(enrollment.StudentId, Is.EqualTo(studentId));
        }

        [Test]
        public void AssignTeacher_Assigns_Teacher_To_Lecture_When_Lecture_Status_Not_Archived()
        {
            var teacher = new Teacher("name");

            //Act
            _sut.AssignTeacher(teacher);

            //Assert
            _sut.Teacher.Id.Should().Be(teacher.Id);
            _sut.Status.Should().NotBe(LectureStatus.Archived);
        }
        
        [Test]
        public void AssignTeacher_Does_Not_Assign_Teacher_To_Lecture_When_Lecture_Status_Is_Archived()
        {
            var teacher = new Teacher("name");
            _sut.ArchiveLecture();
            
            //Act
            _sut.AssignTeacher(teacher);

            //Assert
            _sut.Teacher.Id.Should().Be(teacher.Id);
            _sut.Status.Should().Be(LectureStatus.Archived);
        }
    }

    public class TestStudent
    {
        public static Student Create(string name = "testName", int yearsOFStudy = 1, List<Enrollment> enrollments = null)
        {
            var student = new Student(name, yearsOFStudy);
            return student;
        }
    }
}