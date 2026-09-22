using LocadoraCarros.Data.AutoMapper;
using LocadoraCarros.Model.Context;
using LocadoraCarros.Repository;
using LocadoraCarros.Repository.Interface;
using LocadoraCarros.Services;
using LocadoraCarros.Services.Interface;
using LocadoraCarrosBackEnd.Middleware;
using LocadoraLocacaos.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace LocadoraCarros
{
    //https://localhost:7096/weatherforecast
    public class Program
    {
        public static void Main(string[] args)
        {
            //add

            var builder = WebApplication.CreateBuilder(args);


            var connection = builder.Configuration.GetConnectionString("MySQLConnection");
            builder.Services.AddDbContext<MySQLContext>(options =>
                options.UseMySql(connection,
                new MySqlServerVersion(new Version(8, 0, 44)))
            );
            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(LocadoraCarrosAutomapper));
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IClienteService, ClienteService>();
            builder.Services.AddScoped<ICarroRepository, CarroRepository>();
            builder.Services.AddScoped<ICarroService, CarroService>();
            builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
            builder.Services.AddScoped<ILocacaoService, LocacaoService>();

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    );
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "LocadoraCarros v1"
                    )
                );
            }

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
