using Microsoft.AspNetCore.Mvc;
using MTGM.BL;

namespace MTGM.UI.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private Manager _manager;

        public SetsController(Manager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sets = _manager.GetAllSets().ToList();
            return Ok(sets);
        }
    }
}