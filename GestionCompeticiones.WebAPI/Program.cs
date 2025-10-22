
using GestionCompeticiones.Abstractions;
using GestionCompeticiones.Application;
using GestionCompeticiones.DataAccess;
using GestionCompeticiones.Repository;
using GestionCompeticiones.Services;
using Microsoft.EntityFrameworkCore;

namespace GestionCompeticiones.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

      
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DbDataAccess>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly("GestionCompeticiones.WebAPI")
                );
                options.UseLazyLoadingProxies();
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

          
            builder.Services.AddScoped(typeof(IStringServices), typeof(StringServices));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
            builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}