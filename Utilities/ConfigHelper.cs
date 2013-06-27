using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;

namespace Falafel.Sitefinity.Modules.Twitter.Utilities
{
    public static class ConfigHelper
    {
        /// <summary>
        /// Registers a virtual path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="assembly">The assembly</param>
        public static void RegisterVirtualPath(string path,
            string assembly = "Falafel.Sitefinity.Modules.Twitter")
        {

            var virtualPathConfig = Config.Get<VirtualPathSettingsConfig>();

            // Register the Virtual Path for Falafel if applicable
            if (!virtualPathConfig.VirtualPaths.ContainsKey(path))
            {

                // Create a Virtual Path element
                virtualPathConfig.VirtualPaths.Add(new VirtualPathElement(virtualPathConfig.VirtualPaths)
                {
                    VirtualPath = path,
                    ResolverName = "EmbeddedResourceResolver",
                    ResourceLocation = assembly
                });

                // Get config
                using (var manager = Config.GetManager())
                {

                    // Save config
                    manager.Provider.SuppressSecurityChecks = true;
                    manager.SaveSection(virtualPathConfig);
                }
            }
        }

        /// <summary>
        /// Registers a new item into the toolbox
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title">The Title of the toolbox item</param>
        /// <param name="cssClass">The CssClass of the toolbox item</param>
        /// <param name="resourceClassId">The Resource Class Id for this toolbox item</param>
        /// <param name="sectionName">The Section for this toolbox item</param>
        public static void RegisterToolbox<T>(string title = null, string cssClass = null,
            string resourceClassId = null, string sectionName = null)
        {

            // Get configuration
            using (var manager = Config.GetManager())
            {

                // Relax on the security
                manager.Provider.SuppressSecurityChecks = true;

                var config = manager.GetSection<ToolboxesConfig>();
                var controls = config.Toolboxes["PageControls"];

                ToolboxSection section;

                // Get the default section (Content)
                if (string.IsNullOrEmpty(sectionName))
                {
                    section = controls.Sections.Where<ToolboxSection>(tb => tb.Name == ToolboxesConfig.ContentToolboxSectionName).FirstOrDefault();
                }
                else
                {
                    // Create a new section
                    section = controls.Sections.Where<ToolboxSection>(e => e.Name == sectionName).FirstOrDefault();

                    if (section == null)
                    {
                        section = new ToolboxSection(controls.Sections)
                        {
                            Name = sectionName,
                            Title = sectionName,
                            Description = sectionName,
                            ResourceClassId = typeof(PageResources).Name
                        };
                        controls.Sections.Add(section);
                    }
                }

                // Register in the toolbox
                var control = typeof(T);

                // If the section is null, or the toolbox item already exists, do nothing
                if (section == null || section.Tools.Any<ToolboxItem>(t => t.Name == control.Name)) return;

                // If the toolbox item doesn't exists, create it
                var tool = new ToolboxItem(section.Tools)
                {
                    Name = control.Name,
                    Title = title ?? control.Name,
                    Description = title ?? control.Name,
                    CssClass = cssClass,
                    ResourceClassId = resourceClassId,
                    ControlType = control.AssemblyQualifiedName
                };

                section.Tools.Add(tool);
                manager.SaveSection(config);
            }
        }

        /// <summary>
        /// Register a new ControlTemplate into Sitefinity
        /// </summary>
        /// <param name="friendlyName"></param>
        /// <param name="templateName"></param>
        /// <param name="templatePath"></param>
        /// <param name="areaName"></param>
        public static void RegisterControlTemplate(string friendlyName, string templateName, string templatePath, string areaName)
        {

            var initializer = SiteInitializer.GetInitializer();
            var manager = initializer.PageManager;

            // Check if the given template already exists by filtering on the template path and name
            var existingTemplate = manager.GetPresentationItems<ControlPresentation>()
                .SingleOrDefault(p => p.EmbeddedTemplateName == templatePath &&
                    p.ControlType == typeof(TwitterWidget).FullName && p.Name == templateName);

            // If it already exists, do nothing
            if (existingTemplate != null) return;

            // If it does not exists, create the template
            initializer.RegisterControlTemplate(
                templatePath,
                typeof(TwitterWidget).FullName,
                templateName,
                null,
                areaName,
                "ASP_NET_TEMPLATE",
                "Falafel.Sitefinity.Modules.Twitter",
                friendlyName
                );

            initializer.SaveChanges();
        }
    }
}
