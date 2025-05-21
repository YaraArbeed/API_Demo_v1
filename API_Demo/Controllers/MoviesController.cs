using API_Demo_V2.Data;
using API_Demo_V2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Demo_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;

        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;// Convert 1MB to byte 

        public MoviesController(IMoviesService moviesService, IGenresService genresService)
        {
            _moviesService = moviesService;
            _genresService = genresService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();// Return All Movies Even Though we didn't pass genreId

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            // we can't use .Include with FindAsync
            var movie = await _moviesService.GetById(id);

            if (movie == null)  
            {
                return NotFound();
            }

            return Ok(movie);//?????
        }

        [HttpGet("GetByGenreId")]

        public async Task<IActionResult>GetByGenreIdAsync(byte genreId)
        {
            var movie = await _moviesService.GetAll(genreId);//?????

            return Ok(movie);

        }
        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre = await _genresService.IsvalidGenre(dto.GenreId);

            if (!isValidGenre)
                return BadRequest("Invalid genere ID!");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Poster = dataStream.ToArray(),// Convert Image to Array
                Rate = dto.Rate,
                Storeline = dto.Storeline,
                Year = dto.Year
            };

            movie.Poster = dataStream.ToArray();

            _moviesService.Add(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult>UpdateAsync(int id,[FromForm]MovieDto dto)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
            {
                return NotFound(value: $"No movie was found with ID {id}");
            }

            var isValidGenre = await _genresService.IsvalidGenre(dto.GenreId);

            if (!isValidGenre)
            {
                return BadRequest(error: "Invalid genre ID!");
            }

            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest(error: "Only .png and .jpg images are allowed!");
                }

                if (dto.Poster.Length > _maxAllowedPosterSize)
                {
                    return BadRequest(error: "Max allowed size for poster is 1MB!");
                }

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;
            movie.Storeline = dto.Storeline;
            movie.Rate = dto.Rate;

            _moviesService.Update(movie);

            return Ok(movie);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
                return NotFound($"No movie was found with ID: {id}");
            _moviesService.Delete(movie);
            return Ok(movie);

        }
    }
}
