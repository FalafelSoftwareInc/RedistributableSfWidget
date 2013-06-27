using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Falafel.Sitefinity.Modules.Twitter {

    [ObjectInfo(typeof(TwitterWidgetResources), Title = "TwitterWidgetResourcesTitle", Description = "TwitterWidgetResourcesDescription")]
    public class TwitterWidgetResources : Resource {

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="ProductsResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public TwitterWidgetResources() {
        }

        /// <summary>
        /// Initializes new instance of <see cref="ProductsResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public TwitterWidgetResources(ResourceDataProvider dataProvider)
            : base(dataProvider) {
        }

        #endregion

        #region Class Description

        /// <summary>
        /// The title of this class
        /// </summary>
        [ResourceEntry("TwitterWidgetResourcesTitle",
            Value = "Twitter Widget resources class",
            Description = "The title of this class.",
            LastModified = "2010/12/01")]
        public string TwitterWidgetResourcesTitle {
            get { return this["TwitterWidgetResourcesTitle"]; }
        }

        /// <summary>
        /// The description of this class
        /// </summary>
        [ResourceEntry("TwitterWidgetResourcesDescription",
            Value = "Twitter Widget for Sitefinity",
            Description = "The description of this class.",
            LastModified = "2010/12/01")]
        public string TwitterWidgetResourcesDescription {
            get { return this["TwitterWidgetResourcesDescription"]; }
        }

        #endregion

        #region Resources

        /// <summary>
        /// Twitter Widget Title
        /// </summary>
        [ResourceEntry("TwitterWidgetTitle",
            Value = "Twitter Widget",
            Description = "The title of this widget.",
            LastModified = "2010/12/01")]
        public string TwitterWidgetTitle {
            get { return this["TwitterWidgetTitle"]; }
        }

        /// <summary>
        /// Twitter Widget Name
        /// </summary>
        [ResourceEntry("TwitterWidgetName",
            Value = "TwitterWidget",
            Description = "The name of this widget.",
            LastModified = "2010/12/01")]
        public string TwitterWidgetName {
            get { return this["TwitterWidgetName"]; }
        }

        /// <summary>
        /// Twitter Widget Title
        /// </summary>
        [ResourceEntry("TwitterWidgetAreaName",
            Value = "Custom",
            Description = "The area name for this widget",
            LastModified = "2010/12/01")]
        public string TwitterWidgetAreaName {
            get { return this["TwitterWidgetAreaName"]; }
        }

        #endregion

    }
}
