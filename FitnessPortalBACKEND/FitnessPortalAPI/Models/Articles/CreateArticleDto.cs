﻿namespace FitnessPortalAPI.Models.Articles
{
    public class CreateArticleDto
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? Category { get; set; }
    }
}
