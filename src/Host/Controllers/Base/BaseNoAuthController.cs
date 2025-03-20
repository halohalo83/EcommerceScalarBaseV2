﻿using Application.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceScalarBase.Controllers.Base
{
    [AllowAnonymous]
    public class BaseNoAuthController : VersionNeutralApiController
    {
        /// <summary>
        /// Return a success response in ResponseBase and with a message.
        /// </summary>
        protected OkObjectResult Ok(object value, string message) => Ok(new ResponseBase<object>(value, message));

        /// <summary>
        /// Return a success response in ResponseBase and with a message.
        /// </summary>
        protected OkObjectResult Ok<T>(T result, string message) => Ok(new ResponseBase<T>(result, message));
    }
}
