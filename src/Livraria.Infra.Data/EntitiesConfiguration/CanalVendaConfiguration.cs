using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.EntitiesConfiguration
{
    public class CanalVendaConfiguration : IEntityTypeConfiguration<CanalVenda>
    {
        public void Configure(EntityTypeBuilder<CanalVenda> builder)
        {
            builder.HasKey(cv => cv.Id);
            builder.Property(cv => cv.NomeCanal).HasMaxLength(100).IsRequired();

            // Relacionamentos
            builder.HasMany(cv => cv.LivroPrecoCanalVenda)
                   .WithOne(lpcv => lpcv.CanalVenda)
                   .HasForeignKey(lpcv => lpcv.CanalVenda_CodCanal);
        }
    }
}

