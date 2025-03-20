using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Infrastructure.Common.Extensions;

public class ControllerExtensions
{
    public class ToKebabParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value) => value != null
            ? Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower() // to kebab
            : null;
    }
}