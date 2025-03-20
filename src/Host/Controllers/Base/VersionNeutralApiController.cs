using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceScalarBase.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiVersionNeutral]
    public class VersionNeutralApiController : BaseApiController;
}
