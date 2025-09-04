using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs;
using Order.Application.Services;

namespace Order.API.Controllers
{
    [Route("api/orders/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok("Order service checked");
        }

        [HttpPost]
        public async Task<DataResponse<OrderDTO>> Add([FromBody] OrderDTO data)
        {
            return await _orderService.AddAsync(data);
        }


        [HttpPut]
        public async Task<DataResponse<OrderDTO>> Update([FromBody] OrderDTO data)
        {
            return await _orderService.UpdateAsync(data);
        }

        [HttpDelete("{recID}")]
        public async Task<DataResponse<bool>> Delete(Guid recID)
        {
            return await _orderService.DeleteAsync(recID);
        }
    }
}
