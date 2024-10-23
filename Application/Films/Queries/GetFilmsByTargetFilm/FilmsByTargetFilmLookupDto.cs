using Application.Common.Mappings;
using AutoMapper;
using Domain;

namespace Application.Films.Queries.GetFilmsByTargetFilm;

public class FilmsByTargetFilmLookupDto : IMapWith<Film>
{
    public int Id { get; set; }
    public string PosterUrl { get; set; }
    public string NameRu { get; set; }
    public double? RatingImdb { get; set; }
    public string? Description { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Film, FilmsByTargetFilmLookupDto>()
            .ForMember(filmDto => filmDto.Id,
                opt => opt.MapFrom(film => film.Id))
            .ForMember(filmDto => filmDto.PosterUrl,
                opt => opt.MapFrom(film => film.PosterUrl))
            .ForMember(filmDto => filmDto.RatingImdb,
                opt => opt.MapFrom(film => film.RatingImdb))
            .ForMember(filmDto => filmDto.Description,
                opt => opt.MapFrom(film => film.Description));
    }
}