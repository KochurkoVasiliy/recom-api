using Application.Interfaces;
using Application.Interfaces.Models;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using XGBoostSharp.lib;

namespace Persistence.Services;

public class MovieRatingPrediction
{
    public float Label;
    public float Score;
}

public class RecommendationModelService : IRecommendationModelService
{
    private ITransformer _model;
    private MLContext _mlContext;
    private readonly object _modelLock = new object(); // Для потокобезопасности
    
    public ITransformer GetModel()
    {
        if (_model == null)
            throw new InvalidOperationException("Model has not been trained yet.");
        
        return _model;
    }

    public void TrainModel(IEnumerable<MovieRating> ratings)
    {
        lock (_modelLock)
        {
            if (_model != null && _mlContext != null)
                return;
            
            Console.WriteLine("=============== Training the matrix factorization model ===============");
            
            _mlContext = new MLContext();
            IDataView trainingDataView = _mlContext.Data.LoadFromEnumerable(ratings);
            
            var estimator = _mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: "movieId"));
            
            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "movieIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 1000,
                ApproximationRank = 100
            };
            Console.WriteLine("1");
            var trainerEstimator = estimator.Append(_mlContext.Recommendation().Trainers.MatrixFactorization(options));
            Console.WriteLine("2");
            ITransformer model = trainerEstimator.Fit(trainingDataView);
            _model = trainerEstimator.Fit(trainingDataView);
            Console.WriteLine("Model training completed.");
        }
    }
    
    public List<Film> GetTopRecommendationsWithTitle(IEnumerable<Film> movieData, int userId, int topK)
    {
        // Проверяем, что модель обучена
        if (_model == null || _mlContext == null)
        {
            throw new InvalidOperationException("The model has not been trained yet.");
        }

        // Создаём PredictionEngine для предсказаний
        // PredictionEngine не потокобезопасен, поэтому его следует создавать на каждый вызов
        using var predictionEngine = _mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(_model);

        // Собираем рекомендации для пользователя
        var recommendations = movieData
            .Select(movie => new
            {
                Movie = movie,
                Score = predictionEngine.Predict(new MovieRating
                {
                    userId = userId,
                    movieId = movie.Id
                }).Score
            })
            .OrderByDescending(x => x.Score)
            .Take(topK)
            .Select(x => x.Movie)
            .ToList();

        return recommendations;
    }
    
    public void EvaluateModel(IDataView testDataView)
    {
        Console.WriteLine("=============== Evaluating the model ===============");

        var prediction = _model.Transform(testDataView);
        var metrics = _mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

        Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
        Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
    }
}
