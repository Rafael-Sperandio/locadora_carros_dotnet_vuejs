using LocadoraCarros.Data.AutoMapper;
using LocadoraCarros.Model.Context;
using LocadoraCarros.Repository;
using LocadoraCarros.Repository.Interface;
using LocadoraCarros.Services;
using LocadoraCarros.Services.Interface;
using LocadoraCarrosBackEnd.Middleware;
using LocadoraCarrosBackEnd.Models.Identity;
using LocadoraCarrosBackEnd.Services.Interface;
using LocadoraCarrosBackEnd.Services;
using LocadoraCarrosBackEnd.Utils.Identity;
using LocadoraLocacaos.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace LocadoraCarros
{
    //https://localhost:7096/weatherforecast
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //add

            var builder = WebApplication.CreateBuilder(args);


            // Banco de dados
            var connection = builder.Configuration
                .GetConnectionString("MySQLConnection");

            builder.Services
                .AddDbContext<MySQLContext>(options =>
                options.UseMySql(connection,
                new MySqlServerVersion(new Version(8, 0, 44)))
            );

            // Identity
            builder.Services
                
                .AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;

                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan =
                        TimeSpan.FromMinutes(15);
                })
                .AddEntityFrameworkStores<MySQLContext>()
                .AddDefaultTokenProviders();

            //REVER

            // JWT
            var jwtKey = builder.Configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException(
                    "Chave JWT não configurada."
                );

            var key = Encoding.UTF8.GetBytes(jwtKey);

            // Add services to the container.
            // AutoMapper
            builder.Services.AddAutoMapper(typeof(LocadoraCarrosAutomapper));

            // Repositories
            // Remover ClienteRepository após a migração para Identity.
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            
            builder.Services.AddScoped<ICarroRepository, CarroRepository>();
            builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();


            // Services
            // Remover ClienteService após a migração para Identity.
            builder.Services.AddScoped<IClienteService, ClienteService>();
            
            builder.Services.AddScoped<ICarroService, CarroService>();
            builder.Services.AddScoped<ILocacaoService, LocacaoService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();

            //REVER
            builder.Services.AddScoped<JwtService>();

            // Controllers
            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    );
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });


            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtKey)
                            ),

                            ValidateIssuer = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],

                            ValidateAudience = true,
                            ValidAudience = builder.Configuration["Jwt:Audience"],

                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.FromMinutes(1)
                        };
                });

            builder.Services.AddAuthorization();



            /*
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(key),

                            ClockSkew = TimeSpan.FromMinutes(1)
                        };
                });*/

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("VueFrontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    //"http://localhost:5173"
                    //"http://localhost:8080"           
                });
            });

            var app = builder.Build();


            // Inicialização das Roles
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var roleManager = serviceProvider
                    .GetRequiredService<RoleManager<IdentityRole<long>>>();

                var userManager =serviceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                // Criar Roles
                await IdentitySeed.SeedRolesAsync(roleManager);

                // Criar administrador
                await IdentitySeed.SeedAdminAsync(userManager);
            }

            // Pipeline HTTP
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

            app.UseCors("VueFrontend");

            app.UseAuthentication();//rever

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
