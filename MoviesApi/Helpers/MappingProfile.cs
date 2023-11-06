using AutoMapper;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesDto>();

            CreateMap<MoviesDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}
