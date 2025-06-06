﻿namespace API_Demo_V2.Dtos
{
    public class MovieDto
    {
        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string Storeline { get; set; }

        public IFormFile? Poster { get; set; }// CUZ it will come from Front-End as Imgae not array of byte

        public byte GenreId { get; set; }

    }
}
