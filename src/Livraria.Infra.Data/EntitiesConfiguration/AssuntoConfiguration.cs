using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.EntitiesConfiguration
{
    public class AssuntoConfiguration : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Descricao).HasMaxLength(20).IsRequired();

            // Relacionamentos
            builder.HasMany(a => a.LivroAssuntos)
                   .WithOne(la => la.Assunto)
                   .HasForeignKey(la => la.Assunto_CodAs);
        }
    }
}

