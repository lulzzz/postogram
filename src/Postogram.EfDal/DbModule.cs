using Postogram.DataAccessLayer;
using Postogram.Common.Container;

namespace Postogram.EfDal
{
    public class DbModule : IContainerModule
    {
        public void Configure(IConfigurator configurator)
        {
            configurator.Register<IUnitOfWork, UnitOfWork>();
        }
    }
}
