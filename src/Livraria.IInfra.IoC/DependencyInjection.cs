using Livraria.Application.Interfaces;
using Livraria.Application.Mappings;
using Livraria.Application.Services;
using Livraria.Domain.Account;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Livraria.Infra.Data.Identity;
using Livraria.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {

        // Configuração do Entity Framework para usar SQLite
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        //services.AddDbContext<ApplicationDbContext>(options =>
        // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
        //), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
                 options.AccessDeniedPath = "/Account/Login");

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        services.AddScoped<ILivroAssuntoRepository, LivroAssuntoRepository>();
        services.AddScoped<ILivroAutorRepository, LivroAutorRepository>();
        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<ILivroAutorAssuntoRepository, LivroAutorAssuntoRepository>();
        services.AddScoped<ILivroPrecoCanalVendaRepository, LivroPrecoCanalVendaRepository>();
        services.AddScoped<ICanalVendaRepository, CanalVendaRepository>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAssuntoService, AssuntoService>();
        services.AddScoped<IAutorService, AutorService>();
        services.AddScoped<ILivroAssuntoService, LivroAssuntoService>();
        services.AddScoped<ILivroAutorService, LivroAutorService>();
        services.AddScoped<ILivroService, LivroService>();
        services.AddScoped<ILivroAutorAssuntoService, LivroAutorAssuntoService>();
        services.AddScoped<ICanalVendaService, CanalVendaService>();
        services.AddScoped<ILivroPrecoCanalVendaService, LivroPrecoCanalVendaService>();

        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        var myhandlers = AppDomain.CurrentDomain.Load("Livraria.Application");
        services.AddMediatR(myhandlers);

        return services;
    }
}
