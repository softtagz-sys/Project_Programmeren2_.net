using Microsoft.AspNetCore.Mvc;
using MTGM.BL;
using MTGM.UI.MVC.Models.Dto;

namespace MTGM.UI.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly IManager _manager;

        public SetsController(IManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SetDto>> GetAllSets()
        {
            var sets = _manager.GetAllSets().ToList();
            if (!sets.Any())
            {
                return NoContent(); // 204
            }
            
            var setDtos = sets.Select(set => new SetDto
            {
                Id = set.Id,
                Name = set.Name,
                Code = set.Code,
                ReleaseDate = set.ReleaseDate
            });
            return Ok(setDtos);
        }
        
        [HttpGet("{id}")]
        public ActionResult<SetDto> GetOneSet(int id)
        {
            var set = _manager.GetSet(id);
            if (set == null)
            {
                return NotFound();
            }
            return Ok(new SetDto
            {
                Id = set.Id,
                Name = set.Name,
                Code = set.Code,
                ReleaseDate = set.ReleaseDate
            });
        }
        
        [HttpPost]
        public IActionResult AddSet([FromBody] NewSetDto newSet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var createdSet = _manager.AddSet(newSet.Name, newSet.Code, newSet.ReleaseDate);
            return CreatedAtAction("GetOneSet", 
                new { id = createdSet.Id },
                new SetDto
                {
                    Id = createdSet.Id,
                    Name = createdSet.Name,
                    Code = createdSet.Code,
                    ReleaseDate = createdSet.ReleaseDate
                });
        }
    }
}