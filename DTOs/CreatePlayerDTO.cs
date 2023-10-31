namespace Volunteer_Match_BE.DTO
{
    public class CreatePlayerDTO
    {
        public string VolunteerId { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool IsCaptain { get; set; }
        public string Image { get; set; }
    }
}
