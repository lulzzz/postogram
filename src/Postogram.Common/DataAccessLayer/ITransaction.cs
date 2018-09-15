using System;

namespace Postogram.DataAccessLayer
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void RollBack();
    }
}
