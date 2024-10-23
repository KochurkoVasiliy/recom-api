using Application.Films.Queries.GetFilmDetails;
using Application.Films.Queries.GetFilmsForUser;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Films.Queries.GetFilmsForUser;

public class GetFilmsForUserQueryValidator : AbstractValidator<GetFilmsForUserQuery>
{
    public GetFilmsForUserQueryValidator()
    {
    }
}