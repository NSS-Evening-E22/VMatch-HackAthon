namespace Volunteer_Match_Backend.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string VolunteerId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position  { get; set; }
        public bool IsCaptain { get; set; }
        public string image { get; set; }

    }
}
