namespace Volunteer_Match_Backend.Models
{
    public class Games
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeamOneId { get; set; }
        public string TeamTwoId { get; set;}
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
