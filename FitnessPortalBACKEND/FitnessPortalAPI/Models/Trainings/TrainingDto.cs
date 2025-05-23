﻿namespace FitnessPortalAPI.Models.Trainings
{
    public record TrainingDto
    {
        public int Id { get; set; }
        public DateTime DateOfTraining { get; set; }
        public float TotalPayload { get; set; }
        public int NumberOfSeries { get; set; }
        public List<ExerciseDto> Exercises { get; set; }
    }
}
