using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Falafel.Sitefinity.Modules.Twitter.Configuration
{
    public class TwitterElement : ConfigElement
    {

        public TwitterElement(ConfigElement parent) : base(parent) { }

        [ObjectInfo(Title = "Access token", Description = "Twitter Access token")]
        [ConfigurationProperty("AccessToken", DefaultValue = "")]
        public string AccessToken
        {
            get { return (string)this["AccessToken"]; }
            set { this["AccessToken"] = value; }
        }

        [ObjectInfo(Title = "Access token secret", Description = "Twitter Access token secret key")]
        [ConfigurationProperty("AccessTokenSecret", DefaultValue = "")]
        public string AccessTokenSecret
        {
            get { return (string)this["AccessTokenSecret"]; }
            set { this["AccessTokenSecret"] = value; }
        }

        [ObjectInfo(Title = "Consumer key", Description = "Twitter Consumer key")]
        [ConfigurationProperty("ConsumerKey", DefaultValue = "")]
        public string ConsumerKey
        {
            get { return (string)this["ConsumerKey"]; }
            set { this["ConsumerKey"] = value; }
        }

        [ObjectInfo(Title = "Consumer key secret", Description = "Twitter Consumer key secret")]
        [ConfigurationProperty("ConsumerKeySecret", DefaultValue = "")]
        public string ConsumerKeySecret
        {
            get { return (string)this["ConsumerKeySecret"]; }
            set { this["ConsumerKeySecret"] = value; }
        }

        [ObjectInfo(Title = "Cache time out", Description = "Twitter Cache time out")]
        [ConfigurationProperty("CacheTimeOut", DefaultValue = 5)]
        public int CacheTimeOut
        {
            get { return (int)this["CacheTimeOut"]; }
            set { this["CacheTimeOut"] = value; }
        }

        [ObjectInfo(Title = "Cache key", Description = "Twitter Cache key")]
        [ConfigurationProperty("CacheKey", DefaultValue = "")]
        public string CacheKey
        {
            get { return (string)this["CacheKey"]; }
            set { this["CacheKey"] = value; }
        }

        [ObjectInfo(Title = "Screen name", Description = "Twitter Screen name")]
        [ConfigurationProperty("ScreenName", DefaultValue = "")]
        public string ScreenName
        {
            get { return (string)this["ScreenName"]; }
            set { this["ScreenName"] = value; }
        }
    }
}
