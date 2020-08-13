using Database;
using Database.Constants;
using Database.Models;
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

    public Repository(DatabaseContext repositoryContext)
    {
      this.RepositoryContext = repositoryContext;
    }

    public StudentDetailedDTO GetStudent(int id)
    {
      var student = this.RepositoryContext.Person
                          .Include(p => p.StudentGrade)
                            .ThenInclude(g => g.Course)
                              .FirstOrDefault(p => p.PersonId == id && p.Discriminator == PersonDiscriminator.Student);

      if (student == null)
      {
        return null;
      }

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
      var students = this.RepositoryContext.Person
                          .Include(p => p.StudentGrade)
                            .ThenInclude(g => g.Course)
                              .Where(p => p.Discriminator == PersonDiscriminator.Student)
                                .ToList();

      if (students == null || students.Count == 0)
      {
        return null;
      }

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

    public NewGradeResponseDTO AddStudentGrade(NewGradeDTO newGradeDTO)
    {
      var student = this.GetStudent(newGradeDTO.StudentId);

      if (student == null)
      {
        return null;
      }

      var course = this.RepositoryContext.Course.FirstOrDefault(g => g.CourseId == newGradeDTO.CourseId);

      if (course == null)
      {
        return null;
      }

      //check if the user already has the course
      var grade = this.RepositoryContext.StudentGrade.FirstOrDefault(g => g.CourseId == newGradeDTO.CourseId && g.StudentId == newGradeDTO.StudentId);

      if (grade != null)
      {
        grade.Grade = newGradeDTO.Grade;
      }
      else
      {
        grade = new StudentGrade()
        {
          CourseId = newGradeDTO.CourseId,
          StudentId = newGradeDTO.StudentId,
          Grade = newGradeDTO.Grade
        };

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
