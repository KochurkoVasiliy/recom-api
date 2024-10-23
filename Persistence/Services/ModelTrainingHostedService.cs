using Application.Interfaces;
using Application.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML;

namespace Persistence.Services;

public class ModelTrainingHostedService : IHostedService
{
    private readonly IRecommendationModelService _recommendationModelService;
    private readonly IServiceProvider _serviceProvider;

    public ModelTrainingHostedService(
        IRecommendationModelService recommendationModelService,
        IServiceProvider serviceProvider)
    {
        _recommendationModelService = recommendationModelService;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("=============== Model training started at application startup ===============");

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

            // Получаем данные для обучения
            var ratings = await dbContext.Ratings.Select(r => new MovieRating
            {
                userId = (float)r.UserId.GetHashCode(), // Преобразуем GUID в float
                movieId = (float)r.FilmId,
                Label = (float)r.Score
            }).ToListAsync(cancellationToken);

            // Обучаем модель
            _recommendationModelService.TrainModel(ratings);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}