using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfiguration;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(r => r.Film)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.FilmId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}