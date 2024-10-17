using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfiguration;

public class FilmVectorConfiguration : IEntityTypeConfiguration<FilmVector>
{
    public void Configure(EntityTypeBuilder<FilmVector> builder)
    {
        // Определяем свойства сущности
        builder.HasKey(fv => fv.Id);

        // Преобразование массива float в массив байтов для хранения в базе данных
        builder.Property(fv => fv.DescriptionVector)
            .HasConversion(
                v => v.SelectMany(BitConverter.GetBytes).ToArray(),
                v => v.Chunk(sizeof(float)).Select(arr => BitConverter.ToSingle(arr, 0)).ToArray()
            );

        builder.Property(fv => fv.GenresVector)
            .HasConversion(
                v => v.SelectMany(BitConverter.GetBytes).ToArray(),
                v => v.Chunk(sizeof(float)).Select(arr => BitConverter.ToSingle(arr, 0)).ToArray()
            );
    }
}