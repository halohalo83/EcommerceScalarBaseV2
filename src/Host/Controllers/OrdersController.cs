using Application.Catalog.Orders.Commands;
using Domain.Common.Constants;
using EcommerceScalarBase.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceScalarBase.Controllers
{
    public class OrdersController : BaseNoAuthController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result, MessageCommon.CreateSuccess);
        }
    }
}
