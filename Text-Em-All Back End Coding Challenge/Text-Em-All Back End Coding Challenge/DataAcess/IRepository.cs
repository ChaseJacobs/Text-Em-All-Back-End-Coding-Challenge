using Database.Models;
using System.Collections.Generic;
using WebApp.DTOs;

namespace WebApp.DataAccess
{
  public interface IRepository
  {
    NewGradeResponseDTO AddUpdateStudentGrade(NewGradeDTO newGradeDTO);
    StudentDetailedDTO GetStudent(int id);
    List<StudentSimpleDTO> GetStudents();
  }
}
