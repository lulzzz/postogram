namespace Postogram.DataAccessLayer
{
    public interface IRepository<TEntry, TKey> where TEntry : class
    {
        void Create(TEntry entry);
        TEntry Get(TKey id);
        void Update(TEntry entry);
        void Delete(TKey id);
    }
}
