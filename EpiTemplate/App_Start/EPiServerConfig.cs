using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace EPiTemplate.App_Start
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class EPiServerConfig : IInitializableModule
    {
        private ITabDefinitionRepository _tabRepo;

        public void Initialize(InitializationEngine context)
        {
            // Get dependencies
            _tabRepo = ServiceLocator.Current.GetInstance<ITabDefinitionRepository>();

            // Init
            RegisterTabs();

            // Your other init code for EPiServer here
        }

        private void RegisterTabs()
        {
            // You custom tabs registrations here (defined in Tabs.cs)

            //RegisterTab(Tabs.SiteSettings, sortIndex: 30);
        }

        private void RegisterTab(string name, int sortIndex)
        {
            var tab = new TabDefinition
                {
                    Name = name,
                    RequiredAccess = AccessLevel.Edit,
                    SortIndex = sortIndex
                };

            var existingTab = _tabRepo.List()
                .FirstOrDefault(t => t.Name.Equals(tab.Name, StringComparison.OrdinalIgnoreCase));

            if (existingTab != null)
            {
                tab.ID = existingTab.ID;
            }

            _tabRepo.Save(tab);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}