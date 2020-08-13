using Contracts;
using Entities.Models;

namespace Repository
{
  public class PersonRepository : RepositoryBase<Person>, IPersonRepository
  {
    public PersonRepository(DatabaseContext repositoryContext)
        : base(repositoryContext)
    {
    }
  }
}
