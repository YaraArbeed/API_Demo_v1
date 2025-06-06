﻿using API_Demo_V2.Data;
using API_Demo_V2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Demo_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
     [Authorize(Roles ="User")]// (if you're using JWT)
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.GetAll();

            return Ok(genres);
        }



        [HttpPost]

        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };

            await _genresService.Add(genre);// No need to write _context.Genre
            return Ok(genre);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(byte  id,[FromBody] CreateGenreDto dto)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");

            genre.Name = dto.Name;

            _genresService.Update(genre);
            return Ok(genre);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult>DeleteAsync(byte id)
        {
            var genre = await _genresService.GetById(id);
            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");
            _genresService.Delete(genre);
            return Ok(genre);

        }

    }
}
