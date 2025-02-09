using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // ApiExplorer service injection into Dependency Injection container.
builder.Services.AddSwaggerGen(); // SwaggerGen service injection into Dependency Injection container.

builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))); /* DbContext class injected into Dependency Injection container with connection string.
                                                                                                The application will manage all the instances of this DbContext class whenever we 
                                                                                                call it within controllers or repositories. */

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>(); // We inject the IRegionRepository into the container, with the implmentation of SQLRegionRepository.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
