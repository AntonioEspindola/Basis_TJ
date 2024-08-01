using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Assunto> Assunto { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }
        public DbSet<LivroPrecoCanalVenda> LivroPrecoCanalVenda { get; set; }
        public DbSet<CanalVenda> CanalVenda { get; set; }
        public DbSet<LivroAutorAssunto> LivroAutorAssuntos { get; set; } // View do Relatório

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurações para Livro
            builder.Entity<Livro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(40);
                entity.Property(e => e.Editora).HasMaxLength(40);
                entity.Property(e => e.Edicao).IsRequired();
                entity.Property(e => e.AnoPublicacao).IsRequired().HasMaxLength(4);

                // Configuração para LivroAutor (relacionamento muitos-para-muitos)
                entity.HasMany(l => l.LivroAutores)
                      .WithOne(la => la.Livro)
                      .HasForeignKey(la => la.Livro_Codl);

                // Configuração para LivroAssunto (relacionamento muitos-para-muitos)
                entity.HasMany(l => l.LivroAssuntos)
                      .WithOne(la => la.Livro)
                      .HasForeignKey(la => la.Livro_Codl);

                // Configuração para LivroPrecoCanalVenda (relacionamento muitos-para-muitos)
                entity.HasMany(l => l.LivroPrecoCanalVenda)
                      .WithOne(lp => lp.Livro)
                      .HasForeignKey(lp => lp.Livro_Codl);
            });


            // Configurações para Autor
            builder.Entity<Autor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(40);
            });

            // Configurações para Assunto
            builder.Entity<Assunto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(20);
            });

            // Configurações para LivroAutor (relacionamento muitos-para-muitos)
            builder.Entity<LivroAutor>(entity =>
            {
                entity.HasKey(e => new { e.Livro_Codl, e.Autor_CodAu });

                entity.HasOne(e => e.Livro)
                      .WithMany(e => e.LivroAutores)
                      .HasForeignKey(e => e.Livro_Codl);

                entity.HasOne(e => e.Autor)
                      .WithMany(e => e.LivroAutores)
                      .HasForeignKey(e => e.Autor_CodAu);
            });

            // Configurações para LivroAssunto (relacionamento muitos-para-muitos)
            builder.Entity<LivroAssunto>(entity =>
            {
                entity.HasKey(e => new { e.Livro_Codl, e.Assunto_CodAs });

                entity.HasOne(e => e.Livro)
                      .WithMany(e => e.LivroAssuntos)
                      .HasForeignKey(e => e.Livro_Codl);

                entity.HasOne(e => e.Assunto)
                      .WithMany(e => e.LivroAssuntos)
                      .HasForeignKey(e => e.Assunto_CodAs);
            });

            // Configurações para CanalVenda
            builder.Entity<CanalVenda>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeCanal).IsRequired().HasMaxLength(50);
            });

            // Configurações para LivroPrecoCanalVenda (relacionamento muitos-para-muitos)
            builder.Entity<LivroPrecoCanalVenda>(entity =>
            {
                entity.HasKey(e => new { e.Livro_Codl, e.CanalVenda_CodCanal });

                entity.HasOne(e => e.Livro)
                      .WithMany(l => l.LivroPrecoCanalVenda)
                      .HasForeignKey(e => e.Livro_Codl);

                entity.HasOne(e => e.CanalVenda)
                      .WithMany(c => c.LivroPrecoCanalVenda)
                      .HasForeignKey(e => e.CanalVenda_CodCanal);

                entity.Property(e => e.PrecoVenda).IsRequired();
            });

            // Configuração para LivroAutorAssunto (view)
            builder.Entity<LivroAutorAssunto>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView("vw_LivroAutorAssunto"); // Nome da view no banco de dados

                entity.Property(e => e.LivroId).HasColumnName("LivroId");
                entity.Property(e => e.Titulo).HasColumnName("Titulo");
                entity.Property(e => e.Editora).HasColumnName("Editora");
                entity.Property(e => e.Edicao).HasColumnName("Edicao");
                entity.Property(e => e.AnoPublicacao).HasColumnName("AnoPublicacao");
                entity.Property(e => e.AutorId).HasColumnName("AutorId");
                entity.Property(e => e.AutorNome).HasColumnName("AutorNome");
                entity.Property(e => e.AssuntoId).HasColumnName("AssuntoId");
                entity.Property(e => e.AssuntoDescricao).HasColumnName("AssuntoDescricao");
            });
        }
    }
}


