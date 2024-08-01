using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.EntitiesConfiguration
{
    public class LivroPrecoCanalVendaConfiguration : IEntityTypeConfiguration<LivroPrecoCanalVenda>
    {
        public void Configure(EntityTypeBuilder<LivroPrecoCanalVenda> builder)
        {
            builder.HasKey(lpcv => new { lpcv.Livro_Codl, lpcv.CanalVenda_CodCanal });

            builder.Property(lpcv => lpcv.PrecoVenda).IsRequired();

            // Relacionamentos
            builder.HasOne(lpcv => lpcv.Livro)
                   .WithMany(l => l.LivroPrecoCanalVenda)
                   .HasForeignKey(lpcv => lpcv.Livro_Codl);

            builder.HasOne(lpcv => lpcv.CanalVenda)
                   .WithMany(cv => cv.LivroPrecoCanalVenda)
                   .HasForeignKey(lpcv => lpcv.CanalVenda_CodCanal);
        }
    }
}
