using CouponAPI;
using CouponAPI.DAL;
using CouponAPI.DAL.Interfaces;
using CouponAPI.DAL.Repository;
using CouponAPI.Middleware.Extensions;
using CouponAPI.Service.Implementations;
using CouponAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(connectionString));

builder.Services.AddResponseCaching();
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.ReportApiVersions = true;// в заголовке ответа отображаются все доступные версии 
});
builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;// авто подстановка версии в маршрут
});

builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add("Default30",
        new CacheProfile()
        {
            Duration = 30
        });
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "CouponAPI V1",
        Description = "Web API 2022",
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
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "CouponAPI V2 ...",
        Description = "Web API.",
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

builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = false;
    //opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Quarterly;
    opt.SetExternalDbConnString = connectionString;
    opt.SqlDriverOption = WatchDogSqlDriverEnum.PostgreSql;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "CouponAPI V1");
        option.SwaggerEndpoint("/swagger/v2/swagger.json", "CouponAPI V2");
    });
}

app.UseErrorHandlerMiddleware();

app.UseHttpsRedirection();

app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = builder.Configuration.GetConnectionString("LoginWatchDog");
    opt.WatchPagePassword = builder.Configuration.GetConnectionString("PasswordWatchDog");
});

app.UseAuthorization();

app.MapControllers();

app.UseWatchDogExceptionLogger();

app.Run();
