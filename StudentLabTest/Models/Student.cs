using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StudentLabTest
{
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Course { get; set; }
        public string StudentTicket { get; set; }
        public DateTime BirthDate { get; set; } 
        public int Age { get; set; }

        public Student(string lastName, string firstName, int course, string studentTicket, DateTime birthDate, int age)
        {
            LastName = lastName;
            FirstName = firstName;
            Course = course;
            StudentTicket = studentTicket;
            BirthDate = birthDate;
            Age = age;
        }

        public void CheckAndIncrementAge(DateTime currentDate)
        {
            if (currentDate.Month == BirthDate.Month && currentDate.Day == BirthDate.Day)
            {
                Age++;
            }
        }
    }

    public interface IBaker
    {
        string BakePastries(string ingredients);
    }

    public class Baker : IBaker
    {
        public string BakePastries(string ingredients)
        {
            if (string.IsNullOrWhiteSpace(ingredients))
                throw new ArgumentException("Інгредієнти не можуть бути пустими.");
            return $"Пекар приготував тістечка з використанням таких інгредієнтів: {ingredients}.";
        }
    }

    public interface IEntrepreneur
    {
        bool RegisterBusiness();
    }

    public class Entrepreneur : IEntrepreneur
    {
        public bool RegisterBusiness()
        {
            return true;
        }
    }

    public interface IStudentRepository
    {
        List<Student> LoadStudentsFromFile(string filePath);
    }

    public class JsonStudentRepository : IStudentRepository
    {
        public List<Student> LoadStudentsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<Student>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }
    }

    public class StudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IBaker _baker;
        private readonly IEntrepreneur _entrepreneur;

        public StudentService(IStudentRepository repository, IBaker baker = null, IEntrepreneur entrepreneur = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _baker = baker;
            _entrepreneur = entrepreneur;
        }

        public int GetSpringFourthCourseStudentsCount(string filePath)
        {
            var students = _repository.LoadStudentsFromFile(filePath);
            return students.Count(s => s.Course == 4 && (s.BirthDate.Month == 3 || s.BirthDate.Month == 4 || s.BirthDate.Month == 5));
        }

        public string PerformBakerAction(string ingredients)
        {
            if (_baker == null) throw new InvalidOperationException("Пекар не ініціалізований.");
            return _baker.BakePastries(ingredients);
        }

        public string JumpWithParachute(string studentName)
        {
            return $"{studentName} успішно стрибнув(ла) з парашутом!";
        }
    }
}