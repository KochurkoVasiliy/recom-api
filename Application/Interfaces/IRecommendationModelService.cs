using Application.Interfaces.Models;
using Domain;
using Microsoft.ML;

namespace Application.Interfaces;

public interface IRecommendationModelService
{
    ITransformer GetModel();
    void TrainModel(IEnumerable<MovieRating> ratings);
    List<Film> GetTopRecommendationsWithTitle(IEnumerable<Film> movieData, int userId, int topK);
}