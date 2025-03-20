using Application.Catalog.Customers.Dtos;
using Application.Common.Interfaces;
using Domain.Entities.Customers;
using MediatR;

namespace Application.Catalog.Customers.Queries
{
    public class GetCustomersQuery : IRequest<IReadOnlyCollection<CustomerDto>>;

    public class GetCustomersQueryHandler(IReadRepository<Customer> customerRepository) : IRequestHandler<GetCustomersQuery, IReadOnlyCollection<CustomerDto>>
    {
        public async Task<IReadOnlyCollection<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var entities = await customerRepository.ListAsync();

            return entities.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            }).ToList();
        }
    }
}
