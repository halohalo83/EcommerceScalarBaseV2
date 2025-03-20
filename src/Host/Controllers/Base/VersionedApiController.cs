using Microsoft.AspNetCore.Mvc;

namespace EcommerceScalarBase.Controllers.Base;

[Route("api/v{version:apiVersion}/[controller]")]
public class VersionedApiController : BaseApiController;