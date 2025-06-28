using Microsoft.AspNetCore.Mvc;
using POSInventory.DTOs;
using POSInventory.Services;
using System.Threading.Tasks;

namespace POSInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;
        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpPost]
        public async Task<ActionResult<BillingResponseDto>> CreateBill([FromBody] BillingRequestDto request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
                return BadRequest("No items provided.");
            var result = await _billingService.CreateBillAsync(request);
            return Ok(result);
        }
    }
}
