using Microsoft.AspNetCore.Mvc;
using MTGM.BL;
using MTGM.BL.Domain;
using MTGM.UI.MVC.Models.Dto;

namespace MTGM.UI.MVC.Controllers.Api;

[Route("/api/[controller]")]
[ApiController]
public class DeckEntriesController : ControllerBase
{
    private readonly IManager _manager;

    public DeckEntriesController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("{id}")]
    public ActionResult<DeckEntryDto> GetOneDeckEntry(long id)
    {
        var deckEntry = _manager.getDeckEntry(id);
        if (deckEntry == null)
        {
            return NotFound();
        }
        return Ok(new DeckEntryDto
        {
            Card = deckEntry.Card.Name,
            Deck = deckEntry.Deck.Name,
            Quantity = deckEntry.Quantity,
            AddedOn = deckEntry.AddedOn
        });
    }

    [HttpGet("{deckEntryId}/deckentries")]
    public ActionResult<IEnumerable<DeckEntryDto>> GetCardsOfDeckEntries(long deckEntryId)
    {
        var deckEntries = _manager.GetDeckEntriesOfDeck(deckEntryId).ToList();
        
        if (!deckEntries.Any())
        {
            return NoContent();
        }
        
        return Ok(deckEntries.Select(deckEntry => new DeckEntryDto
        {
            Card = deckEntry.Card.Name,
            Deck = deckEntry.Deck.Name,
            Quantity = deckEntry.Quantity,
            AddedOn = deckEntry.AddedOn
        }));
    }
    
    [HttpPost]
    public ActionResult<DeckEntryDto> AddNewDeckEntry(NewDeckEntryDto deckEntryDto)
    {
        try
        {
            var createdDeckEntry = _manager.AddDeckEntry(deckEntryDto.CardId,
                deckEntryDto.DeckId, deckEntryDto.Quantity, deckEntryDto.AddedOn);
            return CreatedAtAction("GetOneDeckEntry",
                new { id = createdDeckEntry.DeckEntryId },
                new DeckEntryDto
                {
                    Card = createdDeckEntry.Card.Name,
                    Deck = createdDeckEntry.Deck.Name,
                    Quantity = createdDeckEntry.Quantity,
                    AddedOn = createdDeckEntry.AddedOn
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            return StatusCode(500, ex.Message);
        }
    }
}