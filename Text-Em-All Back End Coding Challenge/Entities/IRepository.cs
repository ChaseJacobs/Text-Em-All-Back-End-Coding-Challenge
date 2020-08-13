using Database.Models;

namespace Database
{
  public interface IRepository
  {
    public Person GetPerson(int id);
  }
}
