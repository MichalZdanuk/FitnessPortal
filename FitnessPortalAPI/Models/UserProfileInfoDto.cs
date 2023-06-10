﻿namespace FitnessPortalAPI.Models
{
    public class UserProfileInfoDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
    }
}
