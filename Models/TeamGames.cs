namespace Volunteer_Match_Backend.Models
{
    public class TeamGame
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int TeamOneId { get; set; }
        public Team TeamOne { get; set; }
        public int TeamTwoId { get; set; }
        public Team TeamTwo { get; set; }
      }
}
