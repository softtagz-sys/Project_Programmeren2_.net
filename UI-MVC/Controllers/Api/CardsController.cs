using Microsoft.AspNetCore.Mvc;
using MTGM.BL;
using MTGM.UI.MVC.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MTGM.BL.Domain;

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
        
        [HttpPut("{id:int}")]
        public Task<IActionResult> UpdateCardManaCost(int id, [FromBody] Card card)
        {
            if (User.Identity is { IsAuthenticated: false })
            {
                return Task.FromResult<IActionResult>(Unauthorized());
            }

            var existingCard = _manager.GetCard(id);
            if (existingCard == null)
            {
                return Task.FromResult<IActionResult>(NotFound());
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existingCard.UserId != userId)
            {
                return Task.FromResult<IActionResult>(Forbid());
            }

            existingCard.ManaCost = card.ManaCost;
            _manager.UpdateCard(existingCard);

            return Task.FromResult<IActionResult>(Ok(existingCard));
        }
    }
}