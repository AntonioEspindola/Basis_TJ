using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.EntitiesConfiguration
{
    public class LivroAssuntoConfiguration : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> builder)
        {
            builder.HasKey(la => new { la.Livro_Codl, la.Assunto_CodAs });

            builder.HasOne(la => la.Livro)
                   .WithMany(l => l.LivroAssuntos)
                   .HasForeignKey(la => la.Livro_Codl);

            builder.HasOne(la => la.Assunto)
                   .WithMany(a => a.LivroAssuntos)
                   .HasForeignKey(la => la.Assunto_CodAs);
        }
    }
}

