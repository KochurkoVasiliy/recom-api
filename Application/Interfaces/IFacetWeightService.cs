using Domain;

namespace Application.Interfaces;

public interface IFacetWeightService
{
    FacetWeights Calculate(List<Film> films);
    List<Film> SortFilmsByUserFacets(IEnumerable<Film> films, FacetWeights userFacetWeights);
}