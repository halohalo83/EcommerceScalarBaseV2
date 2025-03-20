using Application.Services;
using Application.Services.Catalog;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IPaginationService, PaginationService>();
            services.AddTransient<IProductService, ProductService>();
            return services;
        }
    }
}