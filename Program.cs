using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

//Add player to a Team 
app.MapPost("/api/teams/{teamId}/players/{playerId}", (int teamId, int playerId, VolunteerMatchDbContext db) =>
{
    var teamToAddPlayer = db.Teams.FirstOrDefault(t => t.Id == teamId);
    var playerToAdd = db.Players.FirstOrDefault(p => p.Id == playerId);

    if (teamToAddPlayer == null || playerToAdd == null)
    {
        return Results.NotFound();
    }

    if (teamToAddPlayer.Players == null)
    {
        teamToAddPlayer.Players = new List<Player>();
    }

    teamToAddPlayer.Players.Add(playerToAdd);
    db.SaveChanges();

    return Results.Created($"/api/teams/{teamToAddPlayer.Id}", teamToAddPlayer);
});



// Alexis Endpoints ^^
// Thomas Endpoints ->

// Get all teams
app.MapGet("/api/teams", (VolunteerMatchDbContext db) =>
{
    List<Team> Teams = db.Teams.ToList();
    if (!Teams.Any())
    {
        return Results.NoContent();
    }
    return Results.Ok(Teams);
});

// Get single team
app.MapGet("/api/teams/{id}", (VolunteerMatchDbContext db, int id) =>
{
    Team team = db.Teams.FirstOrDefault(t => t.Id == id);
    if (team == null)
    {
        return Results.NoContent();
    }
    return Results.Ok(team);
});

// Create new team
app.MapPost("/api/teams", (VolunteerMatchDbContext db, CreateTeamDTO team) =>
{
    Team newTeam = new Team
    {
    VolunteerId = team.VolunteerId,
    Name = team.Name,
    Image = team.Image,
    Sponsor = team.Sponsor,
    GamesWon = 0,
    GamesLost = 0,
    };

    try
    {
        db.Teams.Add(newTeam);
        db.SaveChanges();
        return Results.Created("/api/teams/${id}", newTeam);
    }
    catch (DbUpdateException)
    {
        return Results.NoContent();
    }

});

// Update team details
app.MapPut("/api/teams/{id}", (VolunteerMatchDbContext db, UpdateTeamDTO team, int id) =>
{
    Team teamToUpdate = db.Teams.FirstOrDefault(t => t.Id == id);
    if (teamToUpdate == null)
    {
        return Results.NoContent();
    }
    teamToUpdate.Name = team.Name;
    teamToUpdate.Image = team.Image;
    teamToUpdate.Sponsor = team.Sponsor;

    try
    {
        db.Teams.Update(teamToUpdate);
        db.SaveChanges();
        return Results.Ok(teamToUpdate);
    }
    catch (DbUpdateException)
    {
        return Results.NoContent();
    }
});

// Delete team
app.MapDelete("/api/teams/{id}", (VolunteerMatchDbContext db, int id) =>
{
    Team teamToDelete = db.Teams.FirstOrDefault(t => t.Id == id);
    if (teamToDelete == null)
    {
        return Results.NotFound();
    }
    db.Remove(teamToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

// Get all games
app.MapGet("/api/games", (VolunteerMatchDbContext db) =>
{
    List<Game> games = db.Games.ToList();
    if (!games.Any())
    {
        return Results.NoContent();
    }
    return Results.Ok(games);
});

// Get single game
app.MapGet("/api/games/{id}", (VolunteerMatchDbContext db, int id) =>
{
    Game game = db.Games.FirstOrDefault(g => g.Id == id);
    if (game == null)
    {
        return Results.NoContent();
    }
    return Results.Ok(game);
});

// Create new game
app.MapPost("/api/games", (VolunteerMatchDbContext db, CreateGameDTO game) =>
{
    Game newGame = new Game
    {
        Name = game.Name,
    };

    try
    {
        db.Games.Add(newGame);
        db.SaveChanges();
        return Results.Created("/api/games/${id}", newGame);
    }
    catch (DbUpdateException)
    {
        return Results.NoContent();
    }
});

// Delete game
app.MapDelete("/games/{id}", (VolunteerMatchDbContext db, int id) =>
{
    Game gameToDelete = db.Games.FirstOrDefault(g => g.Id == id);
    if (gameToDelete == null)
    {
        return Results.NotFound();
    }
    db.Games.Remove(gameToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();
