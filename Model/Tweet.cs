using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Falafel.Sitefinity.Modules.Twitter.Utilities;

namespace Falafel.Sitefinity.Modules.Twitter.Model {

    public class Tweet {

        #region Properties
        public string ScreenName { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public string AccountUrl { get; set; }
        public DateTime? CreatedAtDateTime { get; set; }
        public string CreatedAt { get; set; }
        public string ReplyUrl { get; set; }
        public string RetweetUrl { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Get a Tweet and transform it to a UserTimeLineRespone
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Tweet CreateFrom(UserTimelineResponse response) {

            var result = new Tweet();

            if (response.user != null)
                result.Author = response.user.name;

            if (response.utc_created_at != null)
                result.CreatedAtDateTime = response.utc_created_at.Value.ToLocalTime();

            if (response.utc_created_at != null)
                result.CreatedAt = response.utc_created_at.Value.ToRelativeDateString();

            result.Message = FormatMessage(response.text);
            if (response.user != null) result.AccountUrl = response.user.url;

            result.ReplyUrl = string.Format("http://twitter.com/intent/tweet?in_reply_to={0}", response.id);
            result.RetweetUrl = string.Format("http://twitter.com/intent/retweet?tweet_id={0}", response.id);



            return result;
        }

        /// <summary>
        /// Get a list of Tweets and transform them to a UserTimeLineRespone
        /// </summary>
        /// <param name="responses"></param>
        /// <returns></returns>
        public static List<Tweet> CreateFrom(List<UserTimelineResponse> responses) {

            var result = new List<Tweet>(responses.Count());
            result.AddRange(responses.Select(CreateFrom));
            return result;
        }

        /// <summary>
        /// Format all twitter reserved words to real html links
        /// </summary>
        /// <param name="text"></param>
        /// <returns>formated html title text</returns>
        private static string FormatMessage(string text) {

            // Replace all urls to html links
            text = Regex.Replace(text,
                            @"[A-Za-z]+://[^\s]+",
                            u => string.Format("<span class='emphasise'><a target='_blank' href=\"{0}\">{0}</a></span>", u.Value),
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Replace all twitter names like '@name' to html links
            text = Regex.Replace(text,
                            @"@[A-Za-z0-9_]+",
                            u => string.Format("<span class='emphasise'>@<a target='_blank' href=\"http://twitter.com/{0}\">{0}</a></span>", u.Value.Replace("@", string.Empty)),
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Replace all twitter search words like '#search_word' to html links
            text = Regex.Replace(text,
                            @"#[A-Za-z0-9_]+",
                            u => string.Format("<span class='emphasise'><a target='_blank' href=\"http://twitter.com/search?q={0}&src=hash\">{0}</a></span>", u.Value),
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            text = text.Replace("?q=#", "?q=%23");

            return text;
        }

        #endregion
    }
}
