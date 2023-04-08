using AutoMapper;
using CouponAPI.DAL;
using CouponAPI.Service.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WatchDog;
using WatchDog.src.Enums;

namespace CouponAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "CouponAPI V1",
                    Description = "Web API 2023",
                    TermsOfService = new Uri("https://vk.com/id306326375"),
                    Contact = new OpenApiContact
                    {
                        Name = "Горлов Андрей",
                        Email = "avgorlov899@gmail.com",
                        Url = new Uri("https://github.com/Andrej-Gorlov")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Лицензия...",
                        Url = new Uri("https://vk.com/id306326375")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddDbContext<ApplicationDbContext>(opt => {

                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });

            services.AddMediatR(typeof(GetServiceAsync.Handler));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            IMapper mapper = MappingProfiles.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}