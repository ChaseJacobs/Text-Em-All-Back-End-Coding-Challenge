using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Extensions;

namespace WebApp.DTOs
{
  public class StudentDetailedDTO
  {
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double? GPA { get; set; }
    public virtual List<StudentGradeDTO> Grades { get; set; }
  }

  public class StudentGradeDTO
  {
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public double? Grade { get; set; }
  }
}
