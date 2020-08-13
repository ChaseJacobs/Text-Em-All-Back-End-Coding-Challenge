using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs
{
  class StudentDetailedDTO
  {
    public StudentDetailedDTO()
    {
      Grades = new List<StudentGrade>();
    }

    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double GPA { get; set; }
    public virtual ICollection<StudentGrade> Grades { get; set; }
  }
}
