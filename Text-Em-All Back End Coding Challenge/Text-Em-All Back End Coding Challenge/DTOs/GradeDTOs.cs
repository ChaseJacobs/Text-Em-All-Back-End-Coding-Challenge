using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.DTOs
{
  public class NewGradeDTO
  {
    public int studentId { get; set; }
    public int courseId { get; set; }
    public decimal? grade { get; set; }
  }

  public class NewGradeResponseDTO
  {
    public int GradeId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public decimal? Grade { get; set; }
  }
}
