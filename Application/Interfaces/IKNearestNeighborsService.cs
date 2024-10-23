using Domain;
using Persistence.Services.Models;

namespace Application.Interfaces;

public interface IKNearestNeighborsService
{
    List<int> GetNearestNeighbors(Film targetFilm, List<Film> films, int k = 25);
}