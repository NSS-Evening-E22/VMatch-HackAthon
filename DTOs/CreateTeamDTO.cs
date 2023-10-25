using Volunteer_Match_Backend.Models;

namespace Volunteer_Match_BE.DTOs
{
    public class CreateTeamDTO
    {
        public string VolunteerId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Sponsor { get; set; }
    }
}
