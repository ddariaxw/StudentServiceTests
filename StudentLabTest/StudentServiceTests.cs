using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using StudentLabTest;

namespace StudentLabTest
{
    public class StudentServiceTests
    {
        [Fact]
        public void GetSpringFourthCourseStudentsCount_ReturnsCorrectCount()
        {
            var mockRepo = new Mock<IStudentRepository>();
            var testStudents = new List<Student>
            {
                new Student("Шевченко", "Тарас", 4, "AB12345", new DateTime(2002, 4, 15), 22), 
                new Student("Франко", "Іван", 4, "AB12346", new DateTime(2002, 10, 10), 22),  
                new Student("Українка", "Леся", 3, "AB12347", new DateTime(2003, 5, 5), 21),  
                new Student("Костенко", "Ліна", 4, "AB12348", new DateTime(2002, 3, 19), 22)   
            };

            mockRepo.Setup(repo => repo.LoadStudentsFromFile(It.IsAny<string>()))
                    .Returns(testStudents);

            var service = new StudentService(mockRepo.Object);

            int count = service.GetSpringFourthCourseStudentsCount("dummy_path.json");

            Assert.Equal(2, count);
        }

        [Fact]
        public void CheckAndIncrementAge_IncrementsWhenBirthdayMatches()
        {

            var birthDate = new DateTime(2002, 11, 13);
            var student = new Student("Грушевський", "Михайло", 4, "AB99999", birthDate, 24);
            var currentDate = new DateTime(2026, 11, 13); 

            student.CheckAndIncrementAge(currentDate);

            Assert.Equal(25, student.Age);
        }

        [Fact]
        public void PerformBakerAction_UsesMockBakerSuccessfully()
        {
            var mockBaker = new Mock<IBaker>();
            mockBaker.Setup(b => b.BakePastries("полуниця"))
                     .Returns("Пекар приготував тістечка з використанням таких інгредієнтів: полуниця.");

            var service = new StudentService(Mock.Of<IStudentRepository>(), mockBaker.Object, null);

            string result = service.PerformBakerAction("полуниця");

            Assert.Contains("полуниця", result);
        }

        [Fact]
        public void JumpWithParachute_ReturnsExpectedMessage()
        {

            var service = new StudentService(Mock.Of<IStudentRepository>());

            string message = service.JumpWithParachute("Олександр");

            Assert.Equal("Олександр успішно стрибнув(ла) з парашутом!", message);
        }
    }
}