using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using WebAPIAlmacen.Filters;
using WebAPIAlmacen.Middlewares;
using WebAPIAlmacen.Models;
using WebAPIAlmacen.Services;

var builder = WebApplication.CreateBuilder(args);

// Capturamos del app.settings la cadena de conexión a la base de datos
// Configuration.GetConnectionString va directamente a la propiedad ConnectionStrings y de ahí tomamos el valor de DefaultConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Nuestros servicios resolverán dependencias de otras clases
// Registramos en el sistema de inyección de dependencias de la aplicación el ApplicationDbContext
builder.Services.AddDbContext<MiAlmacenContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);
// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(FiltroDeExcepcion));

}).AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        // builder.WithOrigins("https://www.apirequest.io").AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<TransientService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddSingleton<SingletonService>();

builder.Services.AddScoped<TestService>();

builder.Services.AddTransient<OperacionesService>();
// El ContextAccesor lo registramos porque en algún servicio vamos a necesitarlo para obtener información de la petición, por ejemplo la ip 
// desde donde se hace la petición
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IGestorArchivos, GestorArchivosLocal>();
//builder.Services.AddHostedService<TareaProgramadaService>();
builder.Services.AddDataProtection();
builder.Services.AddTransient<HashService>();
builder.Services.AddTransient<CalculosService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(builder.Configuration["ClaveJWT"]))
               });


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

});

// Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();
app.UseMiddleware<GestionYRegistroMiddleware>();

app.UseStaticFiles();
app.MapControllers();

app.Run();
