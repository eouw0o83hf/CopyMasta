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
using CopyMasta.Core.Handler;

namespace CopyMasta
{
    public class CmWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<Window>()
                                      //.WithServiceBase()
                                      .WithServiceAllInterfaces()
                                      .WithServiceSelf()
                                      .LifestyleTransient());

            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<IHandler>()
                                      .WithServiceBase()
                                      .LifestyleSingleton());

            container.Register(Component.For<KeystrokeManager>()
                                        .LifestyleSingleton());

            container.Register(Component.For<KeystrokeListenerBase>()
                                        .ImplementedBy<KeystrokeLitener>()
                                        .LifestyleSingleton());
        }
    }
}
