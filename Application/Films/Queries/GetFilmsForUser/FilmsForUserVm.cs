using Application.Common.Mappings;
using Application.Films.Queries.GetFilmDetails;
using Application.Films.Queries.GetFilmsForUser;
using AutoMapper;
using Domain;

namespace Application.Films.Queries.GetFilmsForUser;

public class FilmsForUserVm
{
    public IList<FilmsForUserLookupDto> Films { get; set; }
}