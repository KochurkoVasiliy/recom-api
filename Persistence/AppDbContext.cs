using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfiguration;

namespace Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<FacetWeights> FacetWeights { get; set; }
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new FilmConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new FacetWeightsConfiguration());
        builder.ApplyConfiguration(new RatingConfiguration());
        base.OnModelCreating(builder);
    }
}