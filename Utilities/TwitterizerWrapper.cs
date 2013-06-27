using Falafel.Sitefinity.Modules.Twitter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Twitterizer;

namespace Falafel.Sitefinity.Modules.Twitter.Utilities
{
    public class TwitterizerWrapper
    {
        #region Constants and variables
        const string UserTimelineUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json?";
        const string RateLimitStatusesUrl = "https://api.twitter.com/1.1/application/rate_limit_status.json?resources=statuses";

        readonly string _accessToken;
        readonly string _accessTokenSecret;
        readonly string _consumerKey;
        readonly string _consumerSecret;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenSecret"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        public TwitterizerWrapper(string accessToken, string accessTokenSecret, string consumerKey, string consumerSecret)
        {
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get the actual RateLimit for the UserTimeline
        /// </summary>
        /// <returns></returns>
        public RateLimit GetRateLimitForUserTimeline()
        {
            var tokens = new OAuthTokens
                {
                    AccessToken = _accessToken,
                    AccessTokenSecret = _accessTokenSecret,
                    ConsumerKey = _consumerKey,
                    ConsumerSecret = _consumerSecret
                };

            string responseString = null;
            var proxy = new WebRequestBuilder(new Uri(RateLimitStatusesUrl), HTTPVerb.GET, tokens);

            using (var response = proxy.ExecuteRequest())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseString = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                }
            }

            var index = responseString.IndexOf("/statuses/user_timeline", System.StringComparison.Ordinal);
            if (index < 0)
                return null;

            var result = new RateLimit();
            var value = GetValue(responseString, index, "limit");
            if (string.IsNullOrEmpty(value))
                return null;
            result.Limit = int.Parse(value);

            value = GetValue(responseString, index, "remaining");
            if (string.IsNullOrEmpty(value)) return null;
            result.LimitRemaining = int.Parse(value);

            value = GetValue(responseString, index, "reset");
            if (string.IsNullOrEmpty(value)) return null;
            result.UtcLimitReset = ConvertFromUnixTimestamp(double.Parse(value));

            return result;
        }

        private string GetValue(string source, int startIndex, string propertyName)
        {
            int index1 = source.IndexOf(propertyName, startIndex);
            if (index1 < 0) return null;
            index1 = source.IndexOf(":", index1);
            if (index1 < 0) return null;

            string value = null;
            for (int i = index1 + 1; i < source.Count(); i++)
                if (char.IsDigit(source[i])) value += source[i];
                else break;

            return value;
        }

        /// <summary>
        /// See https://dev.twitter.com/docs/api/1.1/get/statuses/user_timeline
        /// </summary>
        /// <param name="screenName">screen name</param>
        /// <param name="count">number of tweets to be retrieved</param>
        public List<UserTimelineResponse> GetUserTimeLine(string screenName, int count, out RateLimit rateLimit)
        {

            // Return results in a List of UserTimelineResponse
            List<UserTimelineResponse> result = null;

            // Define new OAuthTokens
            OAuthTokens tokens = new OAuthTokens
                {
                    AccessToken = _accessToken,
                    AccessTokenSecret = _accessTokenSecret,
                    ConsumerKey = _consumerKey,
                    ConsumerSecret = _consumerSecret
                };

            // Concat a request string
            string url = UserTimelineUrl + "count=" + count + "&screen_name=" + screenName;

            // Use the WebRequest and WebResponse to execute request and retrieve the response
            WebRequestBuilder proxy = new WebRequestBuilder(new Uri(url), HTTPVerb.GET, tokens);

            // Execute the request
            using (HttpWebResponse response = proxy.ExecuteRequest())
            {
                string responseString = null;

                // Define our RateLimit (
                rateLimit = new RateLimit();
                rateLimit.Limit = int.Parse(response.Headers["X-Rate-Limit-Limit"]);
                rateLimit.LimitRemaining = int.Parse(response.Headers["X-Rate-Limit-Remaining"]);
                rateLimit.UtcLimitReset = ConvertFromUnixTimestamp(double.Parse(response.Headers["X-Rate-Limit-Reset"]));

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseString = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                }

                // JsonSerializer to get back a strongly typed object (UserTimelineResponse)
                JsonSerializer serializer = new JsonSerializer();
                result = (List<UserTimelineResponse>)serializer.Deserialize(new JsonTextReader(new StringReader(responseString)), typeof(List<UserTimelineResponse>));
            }

            return result;
        }

        /// <summary>
        /// Convert DateTime from a Unix timestamp
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private DateTime ConvertFromUnixTimestamp(double timestamp)
        {

            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        #endregion
    }
}
