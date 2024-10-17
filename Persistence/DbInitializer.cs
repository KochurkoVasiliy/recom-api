using Domain;
using Newtonsoft.Json;

namespace Persistence;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
        // var films = LoadFilmsFromJson("Kinopois.json");
        // context.Films.AddRange(films);
        // context.SaveChanges();
    }
     
    private static List<Film> LoadFilmsFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Film>>(json);
    }
}