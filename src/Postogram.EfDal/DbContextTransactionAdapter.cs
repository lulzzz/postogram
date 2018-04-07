using System.Data.Entity;
using Postogram.DataAccessLayer;

namespace Postogram.EfDal
{
    public class DbContextTransactionAdapter : ITransaction
    {
        private readonly DbContextTransaction _transaction;

        public DbContextTransactionAdapter(DbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }
    }
}
