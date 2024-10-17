using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfiguration;

namespace Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<FilmVector> FilmVectors { get; set; }
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new FilmConfiguration());
        builder.ApplyConfiguration(new FilmVectorConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(builder);
    }
}