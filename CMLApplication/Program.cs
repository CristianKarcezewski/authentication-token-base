using CMLApplication.Application.Implementation;
using CMLApplication.Application.Interfaces;
using CMLApplication.Models;
using CMLApplication.Repository.Implementation;
using CMLApplication.Repository.Interfaces;
using CMLApplication.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lendo configurações
builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));

// Configurando database
builder.Services.AddDbContext<RepositoryDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Configurando HttpClient
builder.Services.AddHttpClient();

// Adicionando escopos de requisições externas
builder.Services.AddScoped<IActivityDirectoryRequests, ActivityDirectoryRequests>();

// Adicionando escopos de repositórios
builder.Services.AddScoped<IColaboradoresRepository, ColaboradoresRepository>();
builder.Services.AddScoped<IGrupoColaboradorPermissaoRepository, GrupoColaboradorPermissaoRepository>();

// Adicionando escopos de serviços
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Configurando validadores de requisições
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return new BadRequestObjectResult(new { Errors = errors });
    };
});

// Configurando autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configurando o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Certifique-se de adicionar a autenticação aqui
app.UseAuthorization();
app.MapControllers();
app.Run();