using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseClient
{
    public class Subscription
    {
        public string UserID { get; set; }
        public string Location { get; set; }
        public int RequestsPerHour { get; set; }
        public string SubscriptionKey { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
    }
}
