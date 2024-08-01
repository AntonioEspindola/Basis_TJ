using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.EntitiesConfiguration
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Titulo).HasMaxLength(40).IsRequired();
            builder.Property(l => l.Editora).HasMaxLength(40).IsRequired(false);
            builder.Property(l => l.Edicao).IsRequired();
            builder.Property(l => l.AnoPublicacao).HasMaxLength(4).IsRequired();

            // Relacionamentos
            builder.HasMany(l => l.LivroAutores)
                   .WithOne(la => la.Livro)
                   .HasForeignKey(la => la.Livro_Codl);

            builder.HasMany(l => l.LivroAssuntos)
                   .WithOne(la => la.Livro)
                   .HasForeignKey(la => la.Livro_Codl);
        }
    }
}

