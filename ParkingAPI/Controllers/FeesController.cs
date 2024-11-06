using Common.DTOs.FeeDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.FeesService;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeesController : ControllerBase
    {
        private readonly IFeesService _feesService;

        public FeesController(IFeesService feesService)
        {
            _feesService = feesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_feesService.Get());
        }

        [HttpPut]
        public IActionResult Modify([FromBody] FeeForModificationDto feeForModification)
        {
            return Ok(_feesService.Modify(feeForModification));
        }
    }
}
