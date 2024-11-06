using Common.DTOs.ParkingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ParkingService;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }


        [HttpPost("add")]
        public IActionResult Add([FromBody] ParkingForAddDto parkingForAdd)
        {
            _parkingService.Add(parkingForAdd);
            return Ok();
        }

        [HttpPut("close")]
        public IActionResult Close([FromBody] string plate)
        {
            ParkingForViewDto? parking = _parkingService.Close(plate);
            if (parking is null)
            {
                return NotFound();
            }

            return Ok(parking);
        }

        [HttpGet("reports")]
        public IActionResult Get()
        {
            return Ok(_parkingService.GetReports());
        }
    }
}
