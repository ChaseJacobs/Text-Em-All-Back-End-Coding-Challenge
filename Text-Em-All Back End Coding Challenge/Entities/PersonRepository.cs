using Database.Models;
using System.Linq;

namespace Database
{
  public partial class Repository : IRepository
  {
    public Person GetPerson(int id)
    {
      return this.RepositoryContext.Person.FirstOrDefault(p => p.PersonId == id);
    }
  }
}
