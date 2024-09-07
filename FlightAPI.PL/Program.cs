using FlightAPI.PL;
using AutoMapper;
using FlightAPI.PL.MappingProfile;
using FlightAPI.PL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Cấu hình AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Đăng ký IFlightService vào Dependency Injection container
builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
