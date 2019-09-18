namespace WeatherVote.Services
{
    public class Voting
    {
        public int Id { get; set; }
        public WeatherSupplier Supplier { get; set; }
        public string Location { get; set; }
        public int Likes { get; set; }
    }
}