using System.Data.Entity;
using Postogram.DataAccessLayer;

namespace Postogram.EfDal
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public void CommitChanges()
        {
            SaveChanges();
        }

        public ITransaction CreateTransaction()
        {
            return new DbContextTransactionAdapter(Database.BeginTransaction());
        }
    }
}
