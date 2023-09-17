﻿namespace FitnessPortalAPI.Models.UserProfileActions
{
    public class UpdateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
    }
}
