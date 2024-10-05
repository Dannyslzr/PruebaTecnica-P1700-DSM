namespace Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction();
        Task SaveChanges();
        Task Commit();
        Task Rollback();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
