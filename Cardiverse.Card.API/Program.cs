using Cardiverse.Card.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

var _cards = new List<Card>();

app.MapGet("/api/Cards", () =>
{
    return Results.Ok(_cards);
});

app.MapGet("/api/Cards/{id}", (int id) =>
{
    var card = _cards.SingleOrDefault(x => x.Id == id);
    return card switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(card),
    };
});

app.MapPost("/api/Cards", (Card card) =>
{
    _cards.Add(card);
    return Results.Created($"/api/Cards/{card.Id}", card);
});

app.MapPut("/api/Cards/{id}", (int id, Card card) =>
{
    if (id != card.Id)
    {
        return Results.BadRequest();
    }
    var currentCard = _cards.SingleOrDefault(x => x.Id == id);
    if (currentCard != null)
        _cards.Remove(currentCard);
    _cards.Add(card);
    return Results.NoContent();
});

app.Run();
