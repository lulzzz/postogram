using Autofac;
using Postogram.DataAccessLayer;

namespace Postogram.EfDal
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            RegisterRepositories(builder);
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
        }
    }
}
