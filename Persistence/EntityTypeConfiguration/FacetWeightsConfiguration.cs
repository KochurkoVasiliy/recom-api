using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Persistence.EntityTypeConfiguration;

public class FacetWeightsConfiguration : IEntityTypeConfiguration<FacetWeights>
{
    public void Configure(EntityTypeBuilder<FacetWeights> builder)
    {
        builder.HasKey(fw => fw.Id);
        
        builder.Property(fw => fw.Id).ValueGeneratedOnAdd();
        builder.Property(fw => fw.UserId).IsRequired();
        
        // Настройка отношений
        builder.HasOne(fw => fw.User)
            .WithOne(u => u.FacetWeights)
            .HasForeignKey<FacetWeights>(fw => fw.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Настройка словарей как JSON
        builder.Property(fw => fw.GenreWeights)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, double>>(v)!);

        builder.Property(fw => fw.YearWeights)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<int, double>>(v)!);

        builder.Property(fw => fw.FilmLenghtWeights)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<int, double>>(v)!);

        builder.Property(fw => fw.AgeRatingWeights)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, double>>(v)!);
    }
}