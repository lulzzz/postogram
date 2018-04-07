namespace Postogram.DataAccessLayer
{
    public interface IUnitOfWork
    {
        ITransaction CreateTransaction();
        void CommitChanges();
    }
}
