using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextP1700 _dbContext;
        private bool _transactionStarted;

        public UnitOfWork(DbContextP1700 dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransaction()
        {
            await _dbContext.Database.BeginTransactionAsync();
            _transactionStarted = true;
        }

        public async Task SaveChanges()
        {
            if (_transactionStarted)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Commit()
        {
            if (_transactionStarted)
            {
                await SaveChanges();
                await _dbContext.Database.CommitTransactionAsync();
            }
        }

        public async Task Rollback()
        {
            if (_transactionStarted)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _transactionStarted = false;
                ClearChangeTracker();
            }
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_dbContext);
        }

        private void ClearChangeTracker()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries().ToList())
            {
                entry.State = EntityState.Detached;
            }
        }

        public async Task<DataTable> ExecuteQuery(string connectionString, string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return dataTable;
        }


        public void Dispose()
        {
            //if (_transactionStarted)
            //{
            //    Rollback();
            //}
            _dbContext.Dispose();
        }
    }
}
