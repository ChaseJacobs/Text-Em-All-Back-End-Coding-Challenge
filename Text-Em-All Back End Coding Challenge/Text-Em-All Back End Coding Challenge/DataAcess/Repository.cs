using Database;
using Database.Constants;
using Database.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApp.DTOs;
using WebApp.Extensions;

namespace WebApp.DataAccess
{
  public class Repository : IRepository
  {
    private DatabaseContext RepositoryContext { get; set; }
    private readonly ILoggerManager Logger;

    public Repository(DatabaseContext repositoryContext, ILoggerManager logger)
    {
      this.RepositoryContext = repositoryContext;
      this.Logger = logger;
    }

    //Function to look up a student
    public StudentDetailedDTO GetStudent(int id)
    {
      Logger.LogInfo($"Entered Get Student - {id}");

      //only load a student value, include the grade and course objects
      var student = this.RepositoryContext.Person
                          .Include(p => p.StudentGrade)
                            .ThenInclude(g => g.Course)
                              .FirstOrDefault(p => p.PersonId == id && p.Discriminator == PersonDiscriminator.Student);
      //stop processing if nothing found
      if (student == null)
      {
        Logger.LogWarn($"Student not found - {id}");
        return null;
      }
      Logger.LogInfo($"Student found - {id}");

      //translate database object into smaller DTOs
      var studentGradesDTO = new List<StudentGradeDTO>();
      foreach (var grade in student.StudentGrade)
      {
        var studentGradeDTO = new StudentGradeDTO()
        {
          CourseId = grade.CourseId,
          Title = grade.Course.Title,
          Credits = grade.Course.Credits,
          Grade = (double)grade.Grade
        };
        studentGradesDTO.Add(studentGradeDTO);
      };

      var studentDTO = new StudentDetailedDTO()
      {
        StudentId = student.PersonId,
        FirstName = student.FirstName,
        LastName = student.LastName,
        Grades = studentGradesDTO,
        GPA = MathExtensions.GenerateGPA(student)
      };

      return studentDTO;
    }

    public List<StudentSimpleDTO> GetStudents()
    {
      Logger.LogInfo($"Entered Get Students");

      var students = this.RepositoryContext.Person
                          .Include(p => p.StudentGrade)
                            .ThenInclude(g => g.Course)
                              .Where(p => p.Discriminator == PersonDiscriminator.Student)
                                .ToList();

      if (students == null || students.Count == 0)
      {
        Logger.LogWarn($"No Students found");
        return null;
      }
      Logger.LogInfo($"Students found");

      //translate database object into smaller DTOs
      var studentsDTO = new List<StudentSimpleDTO>();
      foreach (var student in students)
      {

        var studentDTO = new StudentSimpleDTO
        {
          StudentId = student.PersonId,
          FirstName = student.FirstName,
          LastName = student.LastName,
          GPA = MathExtensions.GenerateGPA(student)
        };
        studentsDTO.Add(studentDTO);
      }

      return studentsDTO;
    }

    public NewGradeResponseDTO AddUpdateStudentGrade(NewGradeDTO newGradeDTO)
    {
      Logger.LogInfo($"Entered Add or Update Grade - student:{newGradeDTO.studentId} course:{newGradeDTO.courseId} grade:{newGradeDTO.grade}");

      var student = this.GetStudent(newGradeDTO.studentId);

      if (student == null)
      {
        Logger.LogWarn($"Student not found - {newGradeDTO.studentId}");
        return null;
      }
      Logger.LogInfo($"Student found - {newGradeDTO.studentId}");

      var course = this.RepositoryContext.Course.FirstOrDefault(g => g.CourseId == newGradeDTO.courseId);

      if (course == null)
      {
        Logger.LogWarn($"Course not found - {newGradeDTO.courseId}");
        return null;
      }
      Logger.LogInfo($"Course found - {newGradeDTO.courseId}");

      //check if the user already has the course
      var grade = this.RepositoryContext.StudentGrade.FirstOrDefault(g => g.CourseId == newGradeDTO.courseId && g.StudentId == newGradeDTO.studentId);

      if (grade != null)
      {
        Logger.LogInfo($"Grade found updating - grade:{grade.EnrollmentId}");
        grade.Grade = newGradeDTO.grade;
      }
      else
      {
        Logger.LogInfo($"Grade not found adding");
        grade = new StudentGrade()
        {
          CourseId = newGradeDTO.courseId,
          StudentId = newGradeDTO.studentId,
          Grade = newGradeDTO.grade
        };
        
        //assign to get the created ID
        grade = this.RepositoryContext.Add(grade).Entity;
      }

      this.RepositoryContext.SaveChanges();

      var newGrade = new NewGradeResponseDTO()
      {
        GradeId = grade.EnrollmentId,
        StudentId = grade.StudentId,
        CourseId = grade.CourseId,
        Grade = grade.Grade
      };

      return newGrade;
    }
  }
}
