using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Services;
using UrlShortener.Infrastructure.Common;
using UrlShortener.Infrastructure.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var cnnstring = builder.Configuration.GetConnectionString("StoreCnn");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(cnnstring));
builder.Services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IShortLinkService, ShortLinkService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("AuthZPolicy", policyBuilder =>
        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWasm", policy =>
    {
        policy.WithOrigins("https://localhost:7260")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
//builder.Services.AddScoped<IUserRepository, UserRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowWasm");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
