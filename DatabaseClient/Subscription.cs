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
        public int ExpiredAt { get; set; }
        public int LastSent { get; set; }
    }
}
