using Microsoft.AspNetCore.Mvc;
using MTGM.BL;

namespace MTGM.UI.MVC.Controllers;

public class DeckController : Controller
{
    private readonly IManager _manager;

    public DeckController(IManager manager)
    {
        _manager = manager;
    }
    
    public IActionResult Details(int id)
    {
        return View(_manager.GetDeck(id));
    }
    
}