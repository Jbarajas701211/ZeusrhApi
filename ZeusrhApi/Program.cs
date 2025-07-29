using DbContext;
using Implementation.Bussiness;
using Implementation.Repositorys;
using Implementation.Utilitys;
using Interfaces.IBussiness;
using Interfaces.IRepository;
using Interfaces.Utilitys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var cadena = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IAppDbContext>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("La cadena  de conexión DefaultConnection no está configurada");
    return new AppDbContext(connectionString);
});

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IManagementUsuario, ManagementUsuario>();
builder.Services.AddTransient<IUtility,  Utility>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());

builder.Services.AddSwaggerGen(
//    opciones =>
//{
//    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//    });

//    opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[]{}
//        }
//    });
//}
);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
