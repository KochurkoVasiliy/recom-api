using Application.Common.Mappings;
using Application.Films.Queries.GetFilmDetails;
using AutoMapper;
using Domain;

namespace Application.Films.Queries.GetFilmList;

public class FilmListVm : IMapWith<Film>
{
    public int Id { get; set; }
    public string? NameRu { get; set; }
    public double? RatingImdb { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Film, FilmListVm>()
            .ForMember(filmVm => filmVm.NameRu,
                opt => opt.MapFrom(film => film.NameRu))
            .ForMember(filmVm => filmVm.RatingImdb,
                opt => opt.MapFrom(film => film.RatingImdb))
            .ForMember(filmVm => filmVm.Id,
                opt => opt.MapFrom(film => film.Id))
            .ForMember(filmVm => filmVm.Description,
                opt => opt.MapFrom(film => film.Description))
            .ForMember(filmVm => filmVm.PosterUrl,
                opt => opt.MapFrom(film => film.PosterUrl));
    }
}