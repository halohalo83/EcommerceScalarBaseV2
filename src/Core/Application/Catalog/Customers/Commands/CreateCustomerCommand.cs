using Application.Common.Interfaces;
using Domain.Entities.Customers;
using MediatR;

namespace Application.Catalog.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<long>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class CreateCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, long>
    {
        private readonly IRepository<Customer> _customerRepository = unitOfWork.GetRepository<Customer>();
        public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = Customer.Create(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

            await _customerRepository.AddAsync(entity, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return entity.Id;
        }
    }
}
