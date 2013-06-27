using Falafel.Sitefinity.Modules.Twitter.Configuration;
using Falafel.Sitefinity.Modules.Twitter.Utilities;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

[assembly: PreApplicationStartMethod(typeof(Falafel.Sitefinity.Modules.Twitter.Global), "PreInit")]
namespace Falafel.Sitefinity.Modules.Twitter
{

    /// <summary>
    /// Startup and Shutdown code into a web application. This gives a much cleaner
    /// solution than having to modify global.asax with the startup logic from many packages.
    /// </summary>
    public class Global : IHttpModule
    {
        #region Methods

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides
        /// access to the methods, properties, and events common to all application objects
        /// within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
        }

        /// <summary>
        /// Will run when the application is starting (same as Application_Start)
        /// Called by the assembly PreApplicationStartMethod attribute
        /// </summary>
        public static void PreInit()
        {

            // Register any module to hook into the Application LifeCycle
            // Register itself so it can subscribe to more events
            DynamicModuleUtility.RegisterModule(typeof(Global));

            // Subscribe to the Sitefinity bootstrap events
            SystemManager.ApplicationStart += OnBootstrapperApplicationStart;
            Bootstrapper.Initializing += OnBootstrapperInitializing;
            Bootstrapper.Initialized += OnBootstrapperInitialized;
        }

        /// <summary>
        /// Disposes of the resources (other than memory)
        /// used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Initializing event of the Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutingEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitializing(object sender, ExecutingEventArgs e)
        {

            if (e.CommandName == "RegisterRoutes")
            {

                // Register custom Virtual Path
                ConfigHelper.RegisterVirtualPath(string.Concat(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH, "/*"));
            }
        }

        /// <summary>
        /// Handles the Initialized event of the Bootstrapper.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitialized(object sender, ExecutedEventArgs e)
        {

            // Register Application Wide custom setting
            Config.RegisterSection<FalafelConfig>();

            // Register any Resource classes
            Res.RegisterResource<TwitterWidgetResources>();

            // Perform actions in Bootstrapper
            if (e.CommandName == "Bootstrapped")
            {

            }
        }

        /// <summary>
        /// Handles the ApplicationStart event of Sitefinity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperApplicationStart(object sender, EventArgs e)
        {

            // Install our Twitter Widget template only once in our database records
            ConfigHelper.RegisterControlTemplate(
                Res.Get<TwitterWidgetResources>("TwitterWidgetTitle"),
                Res.Get<TwitterWidgetResources>("TwitterWidgetName"),
                Constants.LAYOUT_TEMPLATE_PATH,
                Res.Get<TwitterWidgetResources>("TwitterWidgetAreaName"));

            // Register widgets into the toolbox
            ConfigHelper.RegisterToolbox<TwitterWidget>(
                Res.Get<TwitterWidgetResources>("TwitterWidgetTitle"),
                "sfTwitterFeedIcn");
        }

        #endregion

    }
}
