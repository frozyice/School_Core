using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using School_Core.Commands.Lecture;
using School_Core.Contexts;
using School_Core.Domain.Models;
using School_Core.Domain.Models.Students;

namespace School_Core_Tests.Commands
{
    public class EnrollStudentCommandTests
    {
        SchoolCoreDbContext _dbContextMock;
        private Lecture _lecture;
        private Student _student;
        private Guid _lectureId;
        EnrollStudentCommand _command;
        EnrollStudentCommand.Handler _sut;


        [SetUp]
        public void Setup()
        {
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _lecture = new Lecture("name");
            _student = new Student("name");
            _lectureId = _lecture.Id;
            _command = new EnrollStudentCommand(_lectureId, _student.Name);
            _sut = new EnrollStudentCommand.Handler(_dbContextMock);
        }

        [TearDown]
        public void TestCleanup()
        {
            _dbContextMock.Database.EnsureDeleted();
        }


        [Test]
        public void Handle_Returns_False_When_Lecture_Is_Null()
        {
            _dbContextMock.Add(_student);
            _dbContextMock.SaveChanges();

            //Act
            var result = _sut.Handle(_command);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Handle_Returns_False_When_Student_Is_Null()
        {
            _dbContextMock.Add(_lecture);
            _dbContextMock.SaveChanges();

            //Act
            var result = _sut.Handle(_command);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Handle_Returns_False_And_Does_Not_Enroll_Student_To_lecture_When_Student_Has_Enrollment()
        {
            _dbContextMock.Add(_student);
            _dbContextMock.Add(_lecture);
            _lecture.EnrollStudent(_student);
            _dbContextMock.SaveChanges();

            //Act
            var result = _sut.Handle(_command);

            //Assert
            List<Enrollment> enrollments = new List<Enrollment>();
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                enrollments = context.Enrollments.Where(e => e.LectureId == _lectureId).ToList(); //.Lectures.Where(l => l.Id == lecture.Id).Include(x => x.Enrollments).ToList();
            }

            Assert.That(result, Is.False);
            Assert.That(enrollments.Count, Is.EqualTo(1));
        }

        [TestCase(1, StudyField.Law)]
        [TestCase(2, StudyField.None)]
        [TestCase(1, StudyField.None)]
        [Test]
        public void Handle_Returns_False_And_Does_Not_Enroll_Student_To_lecture_When_Student_Is_Not_Allowed_To_Enroll(int studentYearOfStudy, StudyField studentStudyField)
        {
            var student = new Student("name", studentYearOfStudy, studentStudyField);
            var lecture = new Lecture("name", 2, StudyField.Law);
            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            //Act
            var result = _sut.Handle(command);

            //Assert
            Enrollment resultEnrollment;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultEnrollment = context.Enrollments.Where(x => x.StudentId == _student.Id).SingleOrDefault();
            }

            Assert.That(result, Is.False);
            Assert.That(resultEnrollment, Is.Null);
        }

        [TestCase(1, StudyField.None)]
        [TestCase(1, StudyField.Law)]
        [TestCase(2, StudyField.None)]
        [TestCase(2, StudyField.Law)]
        [TestCase(1, StudyField.Law, 1, StudyField.Law)]
        [TestCase(2, StudyField.None, 2, StudyField.None)]
        [TestCase(2, StudyField.Law, 2, StudyField.None)]
        [TestCase(2, StudyField.Law, 2, StudyField.Law)]
        [Test]
        public void Handle_Returns_True_And_Enrolls_Student_To_lecture_When_Student_Is_Allowed_To_Enroll(int studentYearOfStudy, StudyField studentStudyField,
            int lectureEnrollableFromYear = 1, StudyField lectureStudyField = StudyField.None)
        {
            var student = new Student("name", studentYearOfStudy, studentStudyField);
            var lecture = new Lecture("name", lectureEnrollableFromYear, lectureStudyField);
            _dbContextMock.Add(student);
            _dbContextMock.Add(lecture);
            _dbContextMock.SaveChanges();

            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            //Act
            var result = _sut.Handle(command);

            //Assert
            Enrollment resultEnrollment;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                resultEnrollment = context.Enrollments.Where(x => x.StudentId == student.Id).SingleOrDefault();
            }

            Assert.That(result, Is.True);
            Assert.That(resultEnrollment.LectureId, Is.EqualTo(lecture.Id));
            Assert.That(resultEnrollment.StudentId, Is.EqualTo(student.Id));
        }
    }
}