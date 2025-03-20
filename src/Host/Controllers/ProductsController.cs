using Application.Catalog.Products.Queries;
using Domain.Common.Constants;
using EcommerceScalarBase.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceScalarBase.Controllers
{
    public class ProductsController : BaseNoAuthController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetAllProductQuery());
            return Ok(result, MessageCommon.GetDataSuccess);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(GetProductByConditionQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result, MessageCommon.GetDataSuccess);
        }
    }
}
