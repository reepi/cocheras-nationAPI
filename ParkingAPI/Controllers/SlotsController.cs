using Common.DTOs.SlotDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ParkingService;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SlotsController : ControllerBase
    {
        ISlotsService _slotsService;
        public SlotsController(ISlotsService slotsService)
        {
            _slotsService = slotsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_slotsService.Get());
        }

        [HttpPost]
        public IActionResult Add([FromBody] string description)
        {
            if (description is null)
            {
                return BadRequest();
            }
            return Ok(_slotsService.Add(description));
        }

        [HttpPut("{id}")]
        public IActionResult Modify([FromRoute] int id, [FromBody] SlotForModificationDto slotForModification)
        {
            return Ok(_slotsService.Modify(id, slotForModification));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_slotsService.Delete(id))
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
