using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
  public interface IRepositoryWrapper
  {
    IPersonRepository Person { get; }
    IStudentGradeRepository StudentGrade { get; }
    void Save();
  }
}
