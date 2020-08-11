using Contracts;
using Entities.Models;

namespace Repository
{
  public class StudentGradeRepository : RepositoryBase<StudentGrade>, IStudentGradeRepository
  {
    public StudentGradeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }
  }
}
