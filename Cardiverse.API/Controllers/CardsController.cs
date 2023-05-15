using Cardiverse.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cardiverse.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly List<Card> _cards;

    public CardsController()
    {
        _httpClient = new HttpClient();
        _cards = new List<Card>();
        InitializeCards().GetAwaiter().GetResult();
    }

    private async Task InitializeCards()
    {
        var tickers = new List<string> { "AAPL", "GOOGL", "MSFT" }; // Sample tickers to initialize

        foreach (var ticker in tickers)
        {
            var url = $"https://api.polygon.io/v1/open-close/{ticker}/2023-01-09?adjusted=true&apiKey=hpCA6tpDFitUoIRFI_hx0qEhaiEBy91j";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tickerData = JsonConvert.DeserializeObject<StockTickerData>(content);

                var card = new Card(tickerData.Symbol, tickerData.Open, tickerData.Close, tickerData.High, tickerData.Low, tickerData.Volume, tickerData.VWAP);

                _cards.Add(card);
            }
        }
    }

    [Authorize]
    // GET /cards
    [HttpGet]
    public IActionResult GetAllCards()
    {
        return Ok(_cards);
    }

    // GET /cards/{cardName}
    [HttpGet("{cardName}")]
    public IActionResult GetCard(string cardName)
    {
        var card = _cards.Find(c => c.Name == cardName);
        if (card != null)
        {
            return Ok(card);
        }
        else
        {
            return NotFound($"Card '{cardName}' not found.");
        }
    }

    // POST /cards
    [HttpPost]
    public IActionResult CreateCard([FromBody] Card card)
    {
        var existingCard = _cards.Find(c => c.Name == card.Name);
        if (existingCard != null)
        {
            return Conflict($"Card '{card.Name}' already exists.");
        }

        _cards.Add(card);
        return CreatedAtAction(nameof(GetCard), new { cardName = card.Name }, card);
    }

    // PUT /cards/{cardName}
    [HttpPut("{cardName}")]
    public IActionResult UpdateCard(string cardName, [FromBody] Card card)
    {
        var existingCard = _cards.Find(c => c.Name == cardName);
        if (existingCard != null)
        {
            existingCard.Name = card.Name;
            existingCard.DawnStrength = card.DawnStrength;
            existingCard.TwilightFury = card.TwilightFury;
            existingCard.ApexDominance = card.ApexDominance;
            existingCard.ResilientResolve = card.ResilientResolve;
            existingCard.MarketInfluence = card.MarketInfluence;
            existingCard.HarmonicMomentum = card.HarmonicMomentum;
            return Ok($"Card '{cardName}' updated successfully.");
        }
        else
        {
            return NotFound($"Card '{cardName}' not found.");
        }
    }

    // DELETE /cards/{cardName}
    [HttpDelete("{cardName}")]
    public IActionResult DeleteCard(string cardName)
    {
        var card = _cards.Find(c => c.Name == cardName);
        if (card != null)
        {
            _cards.Remove(card);
            return Ok($"Card '{cardName}' deleted successfully.");
        }
        else
        {
            return NotFound($"Card '{cardName}' not found.");
        }
    }

    // POST /cards/update-data
    [HttpPost("update-data")]
    public async Task<IActionResult> UpdateCardData()
    {
        var tickers = new List<string> { "AAPL", "GOOGL", "MSFT" }; // Sample tickers to update

        foreach (var ticker in tickers)
        {
            var url = $"https://api.polygon.io/v1/open-close/{ticker}/2023-01-09?adjusted=true&apiKey=hpCA6tpDFitUoIRFI_hx0qEhaiEBy91j";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tickerData = JsonConvert.DeserializeObject<StockTickerData>(content);

                var card = _cards.Find(c => c.Name == tickerData.Symbol);
                if (card != null)
                {
                    // Update the card attributes with the fetched ticker data
                    card.DawnStrength = tickerData.Open;
                    card.TwilightFury = tickerData.Close;
                    card.ApexDominance = tickerData.High;
                    card.ResilientResolve = tickerData.Low;
                    card.MarketInfluence = tickerData.Volume;
                    card.HarmonicMomentum = tickerData.VWAP;
                }
            }
        }

        return Ok("Card data updated successfully.");
    }
}
