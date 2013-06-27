using System;

namespace Falafel.Sitefinity.Modules.Twitter.Utilities
{

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert DateTime to relative DateTime string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToRelativeDateString(this DateTime date)
        {
            return GetPrettyDate(date);
        }

        private static string GetPrettyDate(DateTime d)
        {
            // Get time span elapsed since the date.
            var s = DateTime.Now.Subtract(d);

            // Get total number of days elapsed.
            var dayDiff = (int)s.TotalDays;

            // Get total number of seconds elapsed.
            var secDiff = (int)s.TotalSeconds;

            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
                return null;

            // Handle same-day times.
            if (dayDiff == 0)
            {
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }

                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }

                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }

                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }

                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }

            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format("{0} weeks ago",
                Math.Ceiling((double)dayDiff / 7));
            }
            return null;
        }
    }
}
