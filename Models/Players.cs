﻿namespace Volunteer_Match_Backend.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string VolunteerId { get; set; }
        public string TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Postition  { get; set; }

    }
}
