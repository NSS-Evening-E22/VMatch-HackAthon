namespace Volunteer_Match_Backend.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
