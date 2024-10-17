using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Film> Films { get; set; }
    DbSet<FilmVector> FilmVectors { get; set; }
    
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}