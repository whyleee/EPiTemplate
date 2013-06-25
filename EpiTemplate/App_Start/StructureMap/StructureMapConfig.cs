using System.Web.Http;
using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace EPiTemplate.App_Start.StructureMap
{
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class StructureMapConfig : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure(ioc => new Bootstrapper().Init(ioc));
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(context.Container);
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}