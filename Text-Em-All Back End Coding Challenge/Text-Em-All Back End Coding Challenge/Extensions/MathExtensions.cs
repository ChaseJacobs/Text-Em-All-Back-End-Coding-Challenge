using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Extensions
{
  public class MathExtensions
  {
    //Extension function to generate the GPA
    public static double? GenerateGPA(Person student)
    {
      //GPA is (grade * credits) / (credits)
      //multiple is ((grade * credits) + (grade * credits)) / (credits + credits)

      double totalGradeCredits = 0;
      var totalCredits = 0;

      foreach (var grade in student.StudentGrade)
      {
        if (grade.Grade.HasValue)
        {
          totalGradeCredits += (double)(grade.Grade.Value * grade.Course.Credits);
          totalCredits += grade.Course.Credits;
        }
      }

      //if no classes with grade, return null
      if(totalCredits == 0)
      {
        return null;
      }

      return totalGradeCredits / totalCredits;
    }
  }
}
