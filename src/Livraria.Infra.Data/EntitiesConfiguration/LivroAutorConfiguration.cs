using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.EntitiesConfiguration
{
    public class LivroAutorConfiguration : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.HasKey(la => new { la.Livro_Codl, la.Autor_CodAu });

            builder.HasOne(la => la.Livro)
                   .WithMany(l => l.LivroAutores)
                   .HasForeignKey(la => la.Livro_Codl);

            builder.HasOne(la => la.Autor)
                   .WithMany(a => a.LivroAutores)
                   .HasForeignKey(la => la.Autor_CodAu);
        }
    }
}

