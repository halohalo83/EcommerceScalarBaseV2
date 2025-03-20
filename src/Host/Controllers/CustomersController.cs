
using Application.Catalog.Customers.Commands;
using Application.Catalog.Customers.Queries;
using Domain.Common.Constants;
using EcommerceScalarBase.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceScalarBase.Controllers
{
    public class CustomersController : BaseNoAuthController
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetCustomersQuery());
            return Ok(result, MessageCommon.GetDataSuccess);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result, MessageCommon.CreateSuccess);
        }
    }
}
