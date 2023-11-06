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
    public class GenresController : ControllerBase
    {
        readonly IGenresService _genresService;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(byte id)
        {
            var genre = await _genresService.GetById(id);
            return Ok(genre);

        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto model)
        {
            var genre = new Genre()
            {
                Name = model.Name, 
            };
            await _genresService.Add(genre);
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, CreateGenreDto model)
        {
            var genre = await _genresService.GetById(id);

            if (genre is null)
                return NotFound($"No genre was found with ID: {id}");

            genre.Name = model.Name;
            _genresService.Update(genre);
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresService.GetById(id);

            if (genre is null)
                return NotFound($"No genre was found with ID: {id}");

            _genresService.Delete(genre);
            return Ok(genre);
        }
    }
}
