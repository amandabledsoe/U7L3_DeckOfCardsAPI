namespace U7L3.Models
{
    public class DrawACardFullResponse
    {
        public DrawACard deckDetails { get; set; }
        public DrawACard_CardDetails[] cardDetails { get; set; }

    }
    public class DrawACard
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public int remaining { get; set; }
        public DrawACard_CardDetails[] cards { get; set; }

    }
    public class DrawACard_CardDetails
    {
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
        public string code { get; set; }
    }
}
