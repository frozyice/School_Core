using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using School_Core.Commands.Lectures;
using School_Core.Contexts;
using School_Core.Domain.Models;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Queries;

namespace TestingTests.Commands
{
    public class EnrollStudentCommandTests
    {
        private SchoolCoreDbContext _dbContextMock;
        private LectureQuery _lectureQuery;
        private StudentQuery _studentQuery;
        private EnrollStudentCommand.Handler _sut;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = DbContextFactory.GetInMemoryDbContext();
            _lectureQuery = new LectureQuery(_dbContextMock);
            _studentQuery = new StudentQuery(_dbContextMock);
            _sut = new EnrollStudentCommand.Handler(_dbContextMock, _lectureQuery, _studentQuery);
        }

        [TearDown]
        public void TestCleanup()
        {
            _dbContextMock.Database.EnsureDeleted();
        }

        [Test]
        public void Handle_Throws_ArgumentException_When_Lecture_Is_Not_Found()
        {
            var lecture = new Lecture("name");
            var student = new Student("name");
            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            _dbContextMock.Add(student);
            _dbContextMock.SaveChanges();

            //Act
            Action result = () => _sut.Handle(command);

            //Assert
            result.Should().Throw<ArgumentException>().WithMessage(nameof(command.LectureId));
        }

        [Test]
        public void Handle_Throws_ArgumentException_When_Student_Is_Not_Found()
        {
            var lecture = new Lecture("name");
            var student = new Student("name");
            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            _dbContextMock.Add(lecture);
            _dbContextMock.SaveChanges();

            //Act
            Action result = () => _sut.Handle(command);

            //Assert
            result.Should().Throw<ArgumentException>().WithMessage(nameof(command.StudentName));
        }

        [Test]
        public void Handle_Returns_ResultFail_When_Lecture_Is_Not_Open()
        {
            var lecture = new Lecture("name");
            var student = new Student("name");
            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            _dbContextMock.AddRange(lecture, student);
            lecture.CloseLecture();
            _dbContextMock.SaveChanges();

            //Act
            var result = _sut.Handle(command);

            //Assert
            Enrollment expectedEnrollment;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                expectedEnrollment = context.Enrollments.Where(x => x.StudentId == student.Id).SingleOrDefault();
            }
            result.isSuccess.Should().BeFalse();
            Assert.That(expectedEnrollment, Is.Null);
            result.Errors.Should().Contain(x => x.Key == "alert" && x.Error == "Can not enroll, lecture is not open for enrollments");
        }

        [Test]
        public void Handle_Returns_ResultFail_When_Student_Has_Enrollment()
        {
            var lecture = new Lecture("name");
            var student = new Student("name");
            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            _dbContextMock.Add(student);
            _dbContextMock.Add(lecture);
            lecture.EnrollStudent(student);
            _dbContextMock.SaveChanges();

            //Act
            var result = _sut.Handle(command);

            //Assert
            var enrollments = new List<Enrollment>();
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                enrollments = context.Enrollments.Where(e => e.LectureId == lecture.Id).ToList(); //.Lectures.Where(l => l.Id == lecture.Id).Include(x => x.Enrollments).ToList();
            }

            Assert.That(result.isSuccess, Is.False);
            Assert.That(enrollments.Count, Is.EqualTo(1));
            result.Errors.Should().Contain(x => x.Key == "" && x.Error == "Can not enroll, lecture has already this student enrolled");
        }

        [Test]
        public void Handle_Returns_ResultFail_When_Student_Does_Not_Have_Required_Years_Of_Study_To_Enroll()
        {
            var student = new Student("name", 1, StudyField.Law);
            var lecture = new Lecture("name", 2, StudyField.Law);
            _dbContextMock.AddRange(student, lecture);
            _dbContextMock.SaveChanges();

            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            //Act
            var result = _sut.Handle(command);

            //Assert
            Enrollment expectedEnrollment;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                expectedEnrollment = context.Enrollments.Where(x => x.StudentId == student.Id).SingleOrDefault();
            }

            Assert.That(result.isSuccess, Is.False);
            Assert.That(expectedEnrollment, Is.Null);
            result.Errors.Should().Contain(x => x.Key == "" && x.Error == "Can not enroll, student does not have required years of study.");
        }
        
        [Test]
        public void Handle_Returns_ResultFail_When_Student_Does_Not_Have_Correct_Field_Of_Study()
        {
            var student = new Student("name", 1, StudyField.None);
            var lecture = new Lecture("name", 1, StudyField.Law);
            _dbContextMock.AddRange(student, lecture);
            _dbContextMock.SaveChanges();

            var command = new EnrollStudentCommand(lecture.Id, student.Name);

            //Act
            var result = _sut.Handle(command);

            //Assert
            Enrollment expectedEnrollment;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                expectedEnrollment = context.Enrollments.Where(x => x.StudentId == student.Id).SingleOrDefault();
            }

            Assert.That(result.isSuccess, Is.False);
            Assert.That(expectedEnrollment, Is.Null);
            result.Errors.Should().Contain(x => x.Key == "" && x.Error == "Can not enroll, student does not have correct field of study.");
        }

        [TestCase(1, StudyField.None)]
        [TestCase(1, StudyField.Law)]
        [TestCase(2, StudyField.None)]
        [TestCase(2, StudyField.Law)]
        [TestCase(1, StudyField.Law, 1, StudyField.Law)]
        [TestCase(2, StudyField.None, 2, StudyField.None)]
        [TestCase(2, StudyField.Law, 2, StudyField.None)]
        [TestCase(2, StudyField.Law, 2, StudyField.Law)]
        public void Handle_Returns_ResultSuccess_And_Enrolls_Student_To_lecture_When_Student_Is_Allowed_To_Enroll(int studentYearOfStudy, StudyField studentStudyField,
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
            Enrollment expectedEnrollment;
            using (var context = DbContextFactory.GetInMemoryDbContext())
            {
                expectedEnrollment = context.Enrollments.Where(x => x.StudentId == student.Id).SingleOrDefault();
            }

            Assert.That(result.isSuccess, Is.True);
            Assert.That(expectedEnrollment.LectureId, Is.EqualTo(lecture.Id));
            Assert.That(expectedEnrollment.StudentId, Is.EqualTo(student.Id));
        }
    }
}