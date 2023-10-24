namespace Volunteer_Match_Backend.Models
{
    public class Teams
    {
        public int Id { get; set; }
        public string VolunteerId { get; set; }
        public string CaptainId { get; set;  }
        public string Name { get; set; }
        public string Image { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
    }
}
