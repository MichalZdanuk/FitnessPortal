﻿namespace FitnessPortalAPI.Models.Trainings
{
    public record TrainingQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
