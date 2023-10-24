namespace Volunteer_Match_Backend.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string TeamOneId { get; set; }
        //public Team TeamOne { get; set; }
        //public string TeamTwoId { get; set;}
        //public Team TeamTwo { get; set; }
        public List<Team> Teams { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
