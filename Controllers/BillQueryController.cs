using Microsoft.AspNetCore.Mvc;
using POSInventory.DTOs;
using POSInventory.Services;
using System.Threading.Tasks;

namespace POSInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillQueryController : ControllerBase
    {
        private readonly IBillQueryService _service;
        public BillQueryController(IBillQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<BillListResponseDto>> Get([FromQuery] BillListRequestDto req)
        {
            var result = await _service.GetPagedAsync(req);
            return Ok(result);
        }
    }
}
