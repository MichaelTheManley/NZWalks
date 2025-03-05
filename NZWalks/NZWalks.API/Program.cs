using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); // HttpContextAccessor service injection into Dependency Injection container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // ApiExplorer service injection into Dependency Injection container.
builder.Services.AddSwaggerGen(); // SwaggerGen service injection into Dependency Injection container.

builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))); /* DbContext class injected into Dependency Injection container with connection string.
                                                                                                The application will manage all the instances of this DbContext class whenever we 
                                                                                                call it within controllers or repositories. */

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>(); // We inject the IRegionRepository into the container, with the implmentation of SQLRegionRepository.
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>(); // We inject the IWalkRepository into the container, with the implmentation of SQLWalkRepository.
builder.Services.AddScoped<IImageRepository, SQLImageRepository>(); // We inject the IImageRepository into the container, with the implmentation of SQLImageRepository.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// We add the authentication service.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Reads it from the appsettings.json file
        ValidAudience = builder.Configuration["Jwt:Audience"], // Reads it from the appsettings.json file
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // This must be before UseAuthorization
app.UseAuthorization(); // Before this can happen, we need to authenticate!

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
    // Essentially, everytime we go to https://localhost:7055/Images, it will redirect use to the FileProvider path, which is the correct path to the images folder.
});

app.MapControllers();

app.Run();
