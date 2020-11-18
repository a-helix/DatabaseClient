namespace DatabaseClient
{
    public class Subscription
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Location { get; set; }
        public int RequestsPerHour { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public long CreatedAt { get; set; }
        public long ExpiredAt { get; set; }
        public long LastSent { get; set; }
    }
}
