using Microsoft.EntityFrameworkCore.Storage;
using Postogram.DataAccessLayer;

namespace Postogram.EfDal
{
    public class DbContextTransactionAdapter : ITransaction
    {
        private readonly IDbContextTransaction _transaction;

        public DbContextTransactionAdapter(IDbContextTransaction transaction)
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
