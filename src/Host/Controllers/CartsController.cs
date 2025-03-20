using Application.Catalog.Carts.Commands;
using Domain.Common.Constants;
using EcommerceScalarBase.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceScalarBase.Controllers
{
    public class CartsController : BaseNoAuthController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateCartCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result, MessageCommon.CreateSuccess);
        }
    }
}
