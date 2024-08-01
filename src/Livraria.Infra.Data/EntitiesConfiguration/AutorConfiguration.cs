using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.EntitiesConfiguration
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nome).HasMaxLength(40).IsRequired();

            // Relacionamentos
            builder.HasMany(a => a.LivroAutores)
                   .WithOne(la => la.Autor)
                   .HasForeignKey(la => la.Autor_CodAu);
        }
    }
}
