using Application.Common.Interfaces;
using Domain.Entities.Carts;
using Domain.Entities.Products;
using MediatR;

namespace Application.Catalog.Carts.Commands
{
    public class CreateCartCommand : IRequest<long>
    {
        public long ProductId { get; set; }
        public long CustomerId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateCartCommandHandler(IUnitOfWork unitOfWork, IReadRepository<Product> productReadRepository) : IRequestHandler<CreateCartCommand, long>
    {
        private readonly IRepository<Cart> cartRepository = unitOfWork.GetRepository<Cart>();
        public async Task<long> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync();

            var product = await productReadRepository.GetByIdAsync(request.ProductId);

            var entity = Cart.Create(request.ProductId, request.CustomerId, request.Quantity, product);

            await cartRepository.AddAsync(entity, cancellationToken);

            await cartRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitAsync();

            return entity.Id;
        }
    }
}
