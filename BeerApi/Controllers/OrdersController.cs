using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrdersController : ControllerBase 
    {
        
        private readonly IOrderService _svc;
        private readonly BeersDbContext _ctx;
        private readonly IOrderServiceApp _svcApp;
        public OrdersController(IOrderService svc, BeersDbContext ctx,         
        IOrderServiceApp svcApp) {
            _svc = svc;
            _ctx = ctx;
            _svcApp = svcApp;
        }

        [HttpPost()]
        public IActionResult NewOrder(NewOrderRequest data) 
        {
            if (!data.Items.Any()) 
            {
                return BadRequest();
            }
            var order = _svcApp.GetOrderFromRequest(data);
            _svc.ProcessOrder(order);
            return Ok(order);
            
        }
    }
}