namespace Volunteer_Match_Backend.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string VolunteerId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public string Sponsor { get; set; }
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }
    }
}
