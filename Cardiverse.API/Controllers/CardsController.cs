using Cardiverse.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cardiverse.API.Controllers;

public class CardsController : BaseEntityController
{
    private static readonly List<Card> _cards = new List<Card>();
    [HttpGet]
    public ActionResult<IEnumerable<Card>> Get()
    {
        return Ok(_cards);
    }
    [HttpPost]
    public IActionResult Post(Card card)
    {
        _cards.Add(card);
        return CreatedAtAction(nameof(Get), card, new { id = card.Id });
    }
}
