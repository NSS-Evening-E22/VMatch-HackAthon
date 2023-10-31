namespace Volunteer_Match_BE.DTOs
{
    public class UpdatePlayerDTO
    {
        public string VolunteerId { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public bool IsCaptain { get; set; }
    }
}
