using Application.Common.Mappings;
using AutoMapper;
using Domain;

namespace Application.Films.Queries.GetFilmDetails;

public class FilmDetailsVm : IMapWith<Film>
{
    public int Id { get; set; }
    public string NameRu { get; set; }
    public double RatingImdb { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Film, FilmDetailsVm>()
            .ForMember(filmVm => filmVm.NameRu,
                opt => opt.MapFrom(film => film.NameRu))
            .ForMember(filmVm => filmVm.RatingImdb,
                opt => opt.MapFrom(film => film.RatingImdb))
            .ForMember(filmVm => filmVm.Id,
                opt => opt.MapFrom(film => film.Id));
    }
}