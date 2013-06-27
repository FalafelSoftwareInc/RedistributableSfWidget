using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Falafel.Sitefinity.Modules.Twitter.Configuration {

    [ObjectInfo(Title = "Falafel Configurations", Description = "Settings used for Falafel extensions")]
    public class FalafelConfig : ConfigSection {

        [ConfigurationProperty("Twitter")]
        public TwitterElement Twitter {
            get {
                return (TwitterElement)this["Twitter"];
            }
        }
    }
}