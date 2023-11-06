using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        readonly IMoviesService _moviesSevice;
        readonly IGenresService _genreSevice;
        readonly IMapper _mapper;

        List<string> _allowedExtenstions = new List<string>() { ".jpg", ".png" };
        long _maxAllowedLengthSize = 1048576;
        public MoviesController(IMoviesService moviesSevice, IGenresService genreSevice, IMapper mapper)
        { 
            _moviesSevice = moviesSevice;
            _genreSevice = genreSevice;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAysnc()
        {
            var movies = await _moviesSevice.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviesSevice.GetById(id);
            if (movie is null)
                return NotFound();

            return Ok(movie);
        }
        [HttpGet("GetGenre")]
        public async Task<IActionResult> GetGenreById(byte genreId)
        {
            var movies = await _moviesSevice.GetAll(genreId);
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MoviesDto model)
        {
            if (model.Poster is null)
                return BadRequest("Poster field is required");

            if (!_allowedExtenstions.Contains(Path.GetExtension(model.Poster.FileName)))
                return BadRequest("Only .jpg and .png images are allowed!");

            if(_maxAllowedLengthSize < model.Poster.Length)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre = await _genreSevice.IsValidGenre(model.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            using var dataStream = new MemoryStream(); 
            await model.Poster.CopyToAsync(dataStream);
            //var movie = new Movie()
            //{

            //    GenreId = model.GenreId,
            //    Poster = dataStream.ToArray(),
            //    Rate = model.Rate,
            //    Storeline = model.Storeline,
            //    Title = model.Title,
            //    Year = model.Year,
            //};
            var data = _mapper.Map<Movie>(model);
            data.Poster = dataStream.ToArray();
            await _moviesSevice.Add(data);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm]MoviesDto model)
        {
            var movie = await _moviesSevice.GetById(id);

            if (movie is null) 
                return NotFound($"No movie was found with ID: {id}");

            var isValidGenre = await _genreSevice.IsValidGenre(model.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            if(movie.Poster is not null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(model.Poster.FileName)))
                    return BadRequest("Only .jpg and .png images are allowed!");

                if (_maxAllowedLengthSize < model.Poster.Length)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();
                await model.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }

            //movie.Id = id;
            //movie.Title = model.Title;
            //movie.Year = model.Year;
            //movie.Storeline = model.Storeline;
            //movie.Rate = model.Rate;
            //movie.GenreId = model.GenreId;
            var data = _mapper.Map<Movie>(model);
            _moviesSevice.Update(movie);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesSevice.GetById(id);
            if (movie is null)
                return NotFound($"No movie was found with ID: {id}");

            _moviesSevice.Delete(movie);
            return Ok(movie);
        }
       
    }
}
