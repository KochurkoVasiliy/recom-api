using Application.Films.Queries.GetFilmDetails;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Films.Queries.GetFilmList;

public class GetFilmListQueryValidator : AbstractValidator<GetFilmListQuery>
{
    public GetFilmListQueryValidator()
    {
    }
}