using MagicTheGatheringManagement.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MTGM.BL;
using MTGM.BL.Domain;
using UI_MVC.Models;

namespace MTGM.UI.MVC.Controllers;

public class CardController : Controller
{
    private readonly IManager _manager;

    public CardController(IManager manager)
    {
        _manager = manager;
    }
    
    public IActionResult Index()
    {
        return View(_manager.GetAllCards());
    }
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.CardColours = Enum.GetValues(typeof(CardColour));
        ViewBag.CardAbilities = Enum.GetValues(typeof(CardAbility));
        ViewBag.CardTypes = Enum.GetValues(typeof(CardType));
        return View();
    }
    [HttpPost]
    public IActionResult Add(NewCardViewModel card)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CardColours = Enum.GetValues(typeof(CardColour));
            ViewBag.CardAbilities = Enum.GetValues(typeof(CardAbility));
            ViewBag.CardTypes = Enum.GetValues(typeof(CardType));
            return View();
        }

        _manager.AddCard(card.Name, card.Type, card.CardAbility, card.CardColour, card.ManaCost, card.Price, card.Description, card.IsFoil);
        ModelState.Clear();
        return RedirectToAction("Index", "Card");
    }
    public IActionResult Details(int id)
    {
        return View(_manager.getCardWithSetsAndDecks(id));
    }
}