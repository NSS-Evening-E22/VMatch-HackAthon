using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Volunteer_Match_Backend;
using Volunteer_Match_Backend.Models;
using Volunteer_Match_BE.DTO;
using Volunteer_Match_BE.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7120")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
object value = builder.Services.AddSwaggerGen();


// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<VolunteerMatchDbContext>(builder.Configuration["VolunteerMatchDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
// ...

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Add this line to enable Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API"); // Adjust this according to your Swagger configuration
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Get All Players
app.MapGet("/api/players", (VolunteerMatchDbContext db) =>
{
    var players = db.Players.ToList();
    return Results.Ok(players);
});
//Get Single Player
app.MapGet("/api/players/{id}", (int playerId, VolunteerMatchDbContext db) =>
{
    var player = db.Players.FirstOrDefault(p => p.Id == playerId);

    if (player == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(player);
});

//Create Player
app.MapPost("/api/player", (VolunteerMatchDbContext db, CreatePlayerDTO playerDTO) =>
{
    var player = new Player
    {
        VolunteerId = playerDTO.VolunteerId,
        TeamId = playerDTO.TeamId,
        FirstName = playerDTO.FirstName,
        LastName = playerDTO.LastName,
        Position = playerDTO.Position,
        IsCaptain = playerDTO.IsCaptain
    };

    
    db.Players.Add(player);
    db.SaveChanges();

    return Results.Created($"/players/{player.Id}", player);
});

//Delete PLayer
app.MapDelete("/api/players/{id}", (int playerId, VolunteerMatchDbContext db) =>
{
    var player = db.Players.FirstOrDefault(p => p.Id ==playerId);

    if (player == null)
    {
        return Results.NotFound();
    }

    db.Remove(player);
    db.SaveChanges();

    return Results.NoContent();
});

//Update player
app.MapPut("/api/players/{id}", (int playerId, UpdatePlayerDTO updatePlayerDTO, VolunteerMatchDbContext db) =>
{
    var player = db.Players.FirstOrDefault(p => p.Id == playerId);

    if (player == null)
    {
        return Results.NotFound();
    }

    player.VolunteerId = updatePlayerDTO.VolunteerId;
    player.TeamId = updatePlayerDTO.TeamId;
    player.FirstName = updatePlayerDTO.FirstName;
    player.LastName = updatePlayerDTO.LastName;
    player.Position = updatePlayerDTO.Position;
    player.IsCaptain = updatePlayerDTO.IsCaptain;

    db.SaveChanges();

    return Results.NoContent();
});


// Alexis Endpoints ^^
// Thomas Endpoints ->

app.Run();
