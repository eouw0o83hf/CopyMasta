using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CopyMasta.Core;

namespace CopyMasta
{
    public class CmWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<Window>()
                                      .LifestyleTransient());

            container.Register(Component.For<IKeystrokeManager>()
                                        .ImplementedBy<KeystrokeManager>()
                                        .LifestyleSingleton());
        }
    }
}
