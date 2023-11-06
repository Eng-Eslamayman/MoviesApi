using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class MoviesService : IMoviesService
    {
        readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            var movies = await _context.Movies
                .Where(m => m.GenreId == genreId || genreId == 0)
                .Include(g => g.Genre)
                .OrderByDescending(m => m.Rate)
                .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _context.Movies.Include(g => g.Genre).SingleOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
