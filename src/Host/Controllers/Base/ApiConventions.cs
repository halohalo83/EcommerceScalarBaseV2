using Application.Common.Responses;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Middleware;

namespace EcommerceScalarBase.Controllers.Base
{
#nullable disable
#pragma warning disable RCS1163, IDE0060

    public static class ApiConventions
    {
        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Search(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get()
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object cancellationtoken)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Post(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Post(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object cancellationToken)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Register(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Create(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object cancellationToken)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Put(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Put(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object cancellationToken)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object cancellationToken)
        {
        }

        [ProducesResponseType(200, Type = typeof(ResponseBase))]
        [ProducesResponseType(400, Type = typeof(ErrorResult))]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Generate(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
        {
        }
    }
}
