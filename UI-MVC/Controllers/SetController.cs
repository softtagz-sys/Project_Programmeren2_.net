using Microsoft.AspNetCore.Mvc;
using MTGM.BL;
using System.Linq;

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
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var sets = _manager.GetAllSets().ToList();
        return Ok(sets);
    }
}