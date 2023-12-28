using Microsoft.AspNetCore.Mvc;
using MTGM.BL;
using MTGM.UI.MVC.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MTGM.UI.MVC.Controllers.Api
{
    [Route("/api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly IManager _manager;

        public CardsController(IManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CardDto>> GetAllCards()
        {
            try
            {
                var cards = _manager.GetAllCards().ToList();
                if (!cards.Any())
                {
                    return NoContent();
                }

                return Ok(cards.Select(c => new CardDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    DeckEntries = c.DeckEntries,
                    SetEntries = c.SetEntries,
                    CardAbility = c.CardAbility,
                    CardColour = c.CardColour,
                    ManaCost = c.ManaCost,
                    Price = c.Price,
                    Description = c.Description,
                    IsFoil = c.IsFoil
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}