namespace Database
{
  public partial class Repository : IRepository
  {
    protected DatabaseContext RepositoryContext { get; set; }

    public Repository(DatabaseContext repositoryContext)
    {
      this.RepositoryContext = repositoryContext;
    }

  }
}
