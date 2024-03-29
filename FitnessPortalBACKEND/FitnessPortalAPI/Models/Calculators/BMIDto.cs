﻿using FitnessPortalAPI.Constants;

namespace FitnessPortalAPI.Models.Calculators
{
    public class BMIDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float BMIScore { get; set; }
        public BMICategory BMICategory { get; set; }
    }
}
