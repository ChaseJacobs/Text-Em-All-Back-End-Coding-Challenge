using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Extensions;

namespace WebApp.DTOs
{
  public class StudentSimpleDTO
  {
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double? GPA { get; set; }
  }
}
