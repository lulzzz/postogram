using System;
using Autofac;
using Postogram.Common.Container;

namespace Postogram.Server.Container
{
    public class AutofacConfiguratorContext : IConfiguratorContext
    {
        readonly IComponentContext _context;

        public AutofacConfiguratorContext(IComponentContext context)
        {
            _context = context;
        }

        public TAbstraction Resolve<TAbstraction>() => _context.Resolve<TAbstraction>();
        public object Resolve(Type abstractionType) => _context.Resolve(abstractionType);
    }
}
