using Microsoft.AspNetCore.Mvc;
using MTGM.BL;

namespace MTGM.UI.MVC.Controllers;

public class SetController : Controller
{
    private Manager _manager;

    public SetController(Manager manager)
    {
        _manager = manager;
    }
    public IActionResult Details(int id)
    {
        return View(_manager.GetSet(id));
    }
}