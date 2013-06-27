using System;

namespace Falafel.Sitefinity.Modules.Twitter.Model {

    public class RateLimit {
        public int Limit { get; set; }
        public int LimitRemaining { get; set; }
        public DateTime UtcLimitReset { get; set; }
    }
}
