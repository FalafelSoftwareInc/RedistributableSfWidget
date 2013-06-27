using Falafel.Sitefinity.Modules.Twitter.Configuration;
using Falafel.Sitefinity.Modules.Twitter.Model;
using Falafel.Sitefinity.Modules.Twitter.Resources.Designers;
using Falafel.Sitefinity.Modules.Twitter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Sitefinity.Web.UI.PublicControls.Enums;

[assembly: WebResource(Constants.STYLE_REFERENCE, "text/css")]
namespace Falafel.Sitefinity.Modules.Twitter
{
    [ControlTemplateInfo("TwitterWidgetResources", "TwitterWidgetTitle", "TwitterWidgetAreaName")]
    [ControlDesigner(typeof(TwitterWidgetDesigner))]
    public class TwitterWidget : SimpleView, ICustomWidgetVisualization
    {
        #region Private variables

        // Get an instance of the FalafelConfig section
        private readonly FalafelConfig _config = Config.Get<FalafelConfig>();

        #endregion

        #region Properties

        /// <summary>
        ///     Get and Set the maximum number of tweets to be shown
        /// </summary>
        public int MaximumNumberOfTweets { get; set; }

        /// <summary>
        ///     Get and Set the render mode (Homepage or Interior Page). Used by designer
        /// </summary>
        public int TwitterWidgetRenderMode { get; set; }

        /// <summary>
        ///     Gets the layout template path
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                // Get the base property value
                var layoutTemplatePath = base.LayoutTemplatePath;

                // If the base property value is null, than get the default value from the constants
                return !string.IsNullOrEmpty(layoutTemplatePath) ? layoutTemplatePath
                           : string.Concat(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH, "/", Constants.LAYOUT_TEMPLATE_PATH);
            }
            set { base.LayoutTemplatePath = value; }
        }

        /// <summary>
        ///     Gets the layout template name
        /// </summary>
        protected override string LayoutTemplateName
        {
            get { return String.Empty; }
        }

        /// <summary>
        ///     Gets the is empty.
        /// </summary>
        /// <value>The is empty.</value>
        public bool IsEmpty
        {
            get
            {
                // Check if the Access Tokens are set
                return string.IsNullOrEmpty(_config.Twitter.AccessToken) ||
                       string.IsNullOrEmpty(_config.Twitter.AccessTokenSecret) ||
                       string.IsNullOrEmpty(_config.Twitter.ConsumerKey) ||
                       string.IsNullOrEmpty(_config.Twitter.ConsumerKeySecret);
            }
        }

        /// <summary>
        ///     Gets the empty link text.
        /// </summary>
        /// <value>The empty link text.</value>
        public string EmptyLinkText
        {
            get { return "This control is not yet configured. Please configure the tokens and settings."; }
        }

        #endregion

        #region Control references

        /// <summary>
        ///     Get a reference to the Repeater on the Home page view
        /// </summary>
        protected virtual Repeater TweetsHomePage
        {
            get { return Container.GetControl<Repeater>("rptTweetsHome", true); }
        }

        /// <summary>
        ///     Get a reference to the Repeater on the Interior page view
        /// </summary>
        protected virtual Repeater TweetsInteriorPage
        {
            get { return Container.GetControl<Repeater>("rptTweetsInterior", true); }
        }

        #endregion

        #region Overrides

        protected override GenericContainer CreateContainer(ITemplate template)
        {
            var container = base.CreateContainer(template);
            CheckConditionalTemplates(container);
            return container;
        }

        /// <summary>
        ///     Checks if the container contains any conditional templates.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void CheckConditionalTemplates(GenericContainer container)
        {
            var conditionalTemplates = container.GetControl<ConditionalTemplateContainer>();
            if (conditionalTemplates != null)
            {
                conditionalTemplates.Evaluate(this);
            }
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            // Check if we are not in DesignMode
            if (SystemManager.IsDesignMode) return;

            // Include our stylesheet
            const string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";
            var includeLocation =
                Page.ClientScript.GetWebResourceUrl(typeof(TwitterWidget), Constants.STYLE_REFERENCE);

            var include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            Page.Header.Controls.Add(include);

            // Include our javascript
            var twitterWebIndent = new JavaScriptEmbedControl
                {
                    ScriptEmbedPosition = ScriptEmbedPosition.Head,
                    Url = "//platform.twitter.com/widgets.js"
                };
            Page.Header.Controls.Add(twitterWebIndent);

            // If there are Tweets inside the cache, return the cache
            if (LoadTweetsFromCache()) return;

            RateLimit rateLimit;
            var tweets = GetUserTimeline(out rateLimit);

            DateTime absoluteExpiration;
            if (rateLimit.LimitRemaining > 20)
            {
                var cacheTimeOut = _config.Twitter.CacheTimeOut;
                if (cacheTimeOut == 0) cacheTimeOut = 5;
                absoluteExpiration = DateTime.Now.AddMinutes(cacheTimeOut);
            }
            else
            {
                absoluteExpiration = rateLimit.UtcLimitReset.ToLocalTime().AddMinutes(1);
            }

            CacheTweets(tweets, absoluteExpiration);
            LoadTweets(tweets);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Bind the Tweets collection to the active repeater
        /// </summary>
        /// <param name="tweets"></param>
        private void LoadTweets(IEnumerable<Tweet> tweets)
        {
            if (tweets == null || !tweets.Any()) return;

            // Check the DisplayMode to see which view will be active
            switch (TwitterWidgetRenderMode)
            {
                case 1:
                    TweetsHomePage.DataSource = tweets;
                    TweetsHomePage.DataBind();
                    break;
                case 2:
                    TweetsInteriorPage.DataSource = tweets;
                    TweetsInteriorPage.DataBind();
                    break;
            }
        }

        /// <summary>
        /// Cache the Tweets collection to the Page cache
        /// </summary>
        /// <param name="tweets"></param>
        /// <param name="absoluteExpiration"></param>
        private void CacheTweets(IEnumerable<Tweet> tweets, DateTime absoluteExpiration)
        {
            if (tweets == null || !tweets.Any()) return;

            Page.Cache.Add(string.Concat(_config.Twitter.CacheKey, "_", TwitterWidgetRenderMode),
                           tweets,
                           null,
                           absoluteExpiration,
                           Cache.NoSlidingExpiration,
                           CacheItemPriority.Low,
                           null);
        }

        /// <summary>
        /// Get the Tweets collection from the Page cache
        /// </summary>
        /// <returns></returns>
        private bool LoadTweetsFromCache()
        {
            var cacheKey = string.Concat(
                _config.Twitter.CacheKey, "_", TwitterWidgetRenderMode);

            if (Page.Cache[cacheKey] != null)
            {
                var tweets = Page.Cache[cacheKey] as List<Tweet>;
                if (tweets == null) return false;
                LoadTweets(tweets);
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Get the Tweets from our user
        /// </summary>
        /// <param name="rateLimit"></param>
        /// <returns></returns>
        private List<Tweet> GetUserTimeline(out RateLimit rateLimit)
        {
            var twitter = new TwitterizerWrapper(
                _config.Twitter.AccessToken,
                _config.Twitter.AccessTokenSecret,
                _config.Twitter.ConsumerKey,
                _config.Twitter.ConsumerKeySecret);

            // Get the tweets from our user
            var tweets = twitter.GetUserTimeLine(_config.Twitter.ScreenName,
                MaximumNumberOfTweets, out rateLimit);

            // Create a new list with formatted tweets, based on our own model
            if (tweets != null && tweets.Any())
                return Tweet.CreateFrom(tweets);

            return new List<Tweet>();
        }

        #endregion
    }
}