using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
   
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

      
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
            

        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto) 
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrders()
        {
            var orders = await _orderService.ListAllOrdersAsync();
        
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
            
        }


        [HttpGet]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethods")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult> DeleteOrderByIdAsync(int id)
        {
            //var order = await _orderService.GetOrderByIdAsync(id, User.RetrieveEmailFromPrincipal());
 
            //if (order == null) return NotFound(new ApiResponse(404));
 
            await _orderService.DeleteOrderByIdAsync(id);
 
            return NoContent();
        }

     



    }
}