using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
  public class RepositoryWrapper : IRepositoryWrapper
  {
    private RepositoryContext _repoContext;
    private IPersonRepository _person;
    private IStudentGradeRepository _studentGrade;

    public IPersonRepository Person
    {
      get
      {
        if (_person == null)
        {
          _person = new PersonRepository(_repoContext);
        }

        return _person;
      }
    }

    public IStudentGradeRepository StudentGrade
    {
      get
      {
        if (_studentGrade == null)
        {
          _studentGrade = new StudentGradeRepository(_repoContext);
        }

        return _studentGrade;
      }
    }

    public RepositoryWrapper(RepositoryContext repositoryContext)
    {
      _repoContext = repositoryContext;
    }

    public void Save()
    {
      _repoContext.SaveChanges();
    }
  }
}
