using Microsoft.AspNetCore.Mvc;
using MTGM.BL;

namespace MTGM.UI.MVC.Controllers.Api;

[Route("/api/[controller]")]
[ApiController]
public class DecksController : ControllerBase
{
    private readonly IManager _manager;
    
    public DecksController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("decks/{deckId}/cards")]
    public IActionResult GetCards(int deckId)
    {
        var cards = _manager.GetCardsFromDeck(deckId);
        return Ok(cards);
    }
}