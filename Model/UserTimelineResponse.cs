using System;
using System.Collections.Generic;
using System.Globalization;

namespace Falafel.Sitefinity.Modules.Twitter.Model
{
    public class Url2
    {
        public string url { get; set; }
        public object expanded_url { get; set; }
        public List<int> indices { get; set; }
    }

    public class Url
    {
        public List<Url2> urls { get; set; }
    }

    public class Description
    {
        public List<object> urls { get; set; }
    }

    public class Entities
    {
        public Url url { get; set; }
        public Description description { get; set; }
    }

    public class User
    {
        public int? id { get; set; }
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public Entities entities { get; set; }
        public bool? @protected { get; set; }
        public int? followers_count { get; set; }
        public int? friends_count { get; set; }
        public int? listed_count { get; set; }
        public string created_at { get; set; }
        public int? favourites_count { get; set; }
        public int? utc_offset { get; set; }
        public string time_zone { get; set; }
        public bool? geo_enabled { get; set; }
        public bool? verified { get; set; }
        public int? statuses_count { get; set; }
        public string lang { get; set; }
        public bool? contributors_enabled { get; set; }
        public bool? is_translator { get; set; }
        public string profile_background_color { get; set; }
        public string profile_background_image_url { get; set; }
        public string profile_background_image_url_https { get; set; }
        public bool? profile_background_tile { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public bool? profile_use_background_image { get; set; }
        public bool? default_profile { get; set; }
        public bool? default_profile_image { get; set; }
        public object following { get; set; }
        public bool? follow_request_sent { get; set; }
        public object notifications { get; set; }
    }

    public class Url4
    {
        public string url { get; set; }
        public object expanded_url { get; set; }
        public List<int> indices { get; set; }
    }

    public class Url3
    {
        public List<Url4> urls { get; set; }
    }

    public class Description2
    {
        public List<object> urls { get; set; }
    }

    public class Entities2
    {
        public Url3 url { get; set; }
        public Description2 description { get; set; }
    }

    public class User2
    {
        public int? id { get; set; }
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public Entities2 entities { get; set; }
        public bool? @protected { get; set; }
        public int? followers_count { get; set; }
        public int? friends_count { get; set; }
        public int? listed_count { get; set; }
        public string created_at { get; set; }
        public int? favourites_count { get; set; }
        public int? utc_offset { get; set; }
        public string time_zone { get; set; }
        public bool? geo_enabled { get; set; }
        public bool? verified { get; set; }
        public int? statuses_count { get; set; }
        public string lang { get; set; }
        public bool? contributors_enabled { get; set; }
        public bool? is_translator { get; set; }
        public string profile_background_color { get; set; }
        public string profile_background_image_url { get; set; }
        public string profile_background_image_url_https { get; set; }
        public bool? profile_background_tile { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public bool? profile_use_background_image { get; set; }
        public bool? default_profile { get; set; }
        public bool? default_profile_image { get; set; }
        public object following { get; set; }
        public bool? follow_request_sent { get; set; }
        public object notifications { get; set; }
    }

    public class Entities3
    {
        public List<object> hashtags { get; set; }
        public List<object> urls { get; set; }
        public List<object> user_mentions { get; set; }
    }

    public class RetweetedStatus
    {
        public string created_at { get; set; }
        public long? id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public bool? truncated { get; set; }
        public object in_reply_to_status_id { get; set; }
        public object in_reply_to_status_id_str { get; set; }
        public object in_reply_to_user_id { get; set; }
        public object in_reply_to_user_id_str { get; set; }
        public object in_reply_to_screen_name { get; set; }
        public User2 user { get; set; }
        public object geo { get; set; }
        public object coordinates { get; set; }
        public object place { get; set; }
        public object contributors { get; set; }
        public int? retweet_count { get; set; }
        public Entities3 entities { get; set; }
        public bool? favorited { get; set; }
        public bool? retweeted { get; set; }
    }

    public class UserMention
    {
        public string screen_name { get; set; }
        public string name { get; set; }
        public int? id { get; set; }
        public string id_str { get; set; }
        public List<int> indices { get; set; }
    }

    public class Entities4
    {
        public List<object> hashtags { get; set; }
        public List<object> urls { get; set; }
        public List<UserMention> user_mentions { get; set; }
    }

    public class UserTimelineResponse : ResponseBase
    {
        public string created_at { get; set; }
        public long? id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public bool? truncated { get; set; }
        public object in_reply_to_status_id { get; set; }
        public object in_reply_to_status_id_str { get; set; }
        public object in_reply_to_user_id { get; set; }
        public object in_reply_to_user_id_str { get; set; }
        public object in_reply_to_screen_name { get; set; }
        public User user { get; set; }
        public object geo { get; set; }
        public object coordinates { get; set; }
        public object place { get; set; }
        public object contributors { get; set; }
        public RetweetedStatus retweeted_status { get; set; }
        public int? retweet_count { get; set; }
        public Entities4 entities { get; set; }
        public bool? favorited { get; set; }
        public bool? retweeted { get; set; }

        public DateTime? utc_created_at
        {
            get
            {
                DateTime result;
                if (ParseTwitterTime(created_at, out result)) return result;
                return null;
            }
        }

        public static bool ParseTwitterTime(string s, out DateTime result)
        {
            const string format = "ddd MMM dd HH:mm:ss zzzz yyyy"; //"Sat Nov 10 11:32:28 +0000 2012"
            return DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal,
                                          out result);
        }
    }
}