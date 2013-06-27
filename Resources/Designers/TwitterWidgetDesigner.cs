using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Falafel.Sitefinity.Modules.Twitter.Configuration;
using Telerik.Sitefinity.Web.UI.ControlDesign;

[assembly: WebResource(Constants.SCRIPT_REFERENCE, "application/x-javascript")]
namespace Falafel.Sitefinity.Modules.Twitter.Resources.Designers {
    
    /// <summary>
    /// Represents a designer for the Twitter Widget
    /// </summary>
    public class TwitterWidgetDesigner : ControlDesignerBase {

        #region Properties

        /// <summary>
        /// Gets the layout template path
        /// </summary>
        public override string LayoutTemplatePath {
            get { return string.Concat(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH, "/", Constants.LAYOUT_TEMPLATE_PATH_DESIGNER);}
        }

        /// <summary>
        /// Gets the layout template name
        /// </summary>
        protected override string LayoutTemplateName {
            get {
                return String.Empty;
            }
        }

        protected override HtmlTextWriterTag TagKey {
            get {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        /// <summary>
        /// Gets the control that is bound to the MaximumNumberOfTweets property
        /// </summary>
        protected virtual Control MaximumNumberOfTweets {
            get {
                return this.Container.GetControl<Control>("MaximumNumberOfTweets", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the TwitterWidgetRenderMode property
        /// </summary>
        protected virtual DropDownList TwitterWidgetRenderMode {
            get {
                return this.Container.GetControl<DropDownList>("TwitterWidgetRenderMode", true);
            }
        }

        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container) {

            TwitterWidgetRenderMode.Items.Add(new ListItem() { Text = "Homepage", Value = "1" });
            TwitterWidgetRenderMode.Items.Add(new ListItem() { Text = "Interior page", Value = "2" });

        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors() {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("maximumNumberOfTweets", this.MaximumNumberOfTweets.ClientID);
            descriptor.AddElementProperty("twitterWidgetRenderMode", this.TwitterWidgetRenderMode.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(Constants.SCRIPT_REFERENCE, typeof(TwitterWidgetDesigner).Assembly.FullName));
            return scripts;
        }
        #endregion

        #region Private members & constants
       
        #endregion
    }
}

