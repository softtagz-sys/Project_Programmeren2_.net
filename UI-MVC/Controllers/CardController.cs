using Microsoft.AspNetCore.Mvc;
using MTGM.BL;

namespace MTGM.UI.MVC.Controllers;

public class CardController : Controller
{
    private Manager _manager;

    public CardController(Manager manager)
    {
        _manager = manager;
    }
    
    public IActionResult Index()
    {
        return View(_manager.GetAllCards());
    }
    
    public IActionResult Add()
    {
        return View();
    }
    public IActionResult Details(int id)
    {
        return View(_manager.getCardWithSetsAndDecks(id));
    }
    
    
}