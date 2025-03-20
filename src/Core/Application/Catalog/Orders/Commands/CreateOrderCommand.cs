using Application.Catalog.Orders.Dtos;
using Application.Catalog.Products.Dtos;
using Application.Common.Caching;
using Application.Common.Interfaces;
using Application.Services.Catalog;
using Domain.Common.Constants;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using MediatR;
using System.Collections.Concurrent;

namespace Application.Catalog.Orders.Commands
{
    public class CreateOrderCommand : IRequest<long>
    {
        public long? CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    public class CreateOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IRedisCacheService redisCacheService,
        IProductService productService) : IRequestHandler<CreateOrderCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRepository<Order> _orderRepository = unitOfWork.GetRepository<Order>();
        private readonly IRepository<Product> _productRepository = unitOfWork.GetRepository<Product>();
        private readonly IRedisCacheService _redisCacheService = redisCacheService;
        private readonly IProductService _productService = productService;
        private static readonly ConcurrentQueue<CreateOrderCommand> OrderQueue = new();
        private static bool _isProcessingOrder = false;

        public async Task<long> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OrderQueue.Enqueue(request);
                await ProcessQueueAsync(cancellationToken);
                return 1;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task ProcessQueueAsync(CancellationToken cancellationToken)
        {
            if (_isProcessingOrder)
                return;

            _isProcessingOrder = true;

            while (OrderQueue.TryDequeue(out var order) && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await HandleOrderAsync(order, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            _isProcessingOrder = false;
        }

        private async Task HandleOrderAsync(CreateOrderCommand order, CancellationToken cancellationToken)
        {
            var allProducts = await _productService.GetAllProducts();

            foreach (var item in order.OrderItems)
            {
                var productKey = $"product:{item.ProductId}";
                var stock = await _redisCacheService.GetCacheValueAsync<int?>(productKey);

                if (!stock.HasValue || stock.Value < item.Quantity)
                {
                    await FallbackToDatabaseCheck(order, allProducts, cancellationToken);
                    return;
                }
            }

            var keys = order.OrderItems.Select(item => (StackExchange.Redis.RedisKey)$"product:{item.ProductId}").ToArray();
            var args = order.OrderItems.Select(item => (StackExchange.Redis.RedisValue)item.Quantity).ToArray();

            string script = @"
                local totalItems = #KEYS

                for i = 1, totalItems do
                    local productKey = KEYS[i]
                    local quantity = tonumber(ARGV[i])

                    local stock = tonumber(redis.call('GET', productKey))

                    if stock == nil or stock < quantity then
                        return 0 -- Stock not available
                    end
                end

                for i = 1, totalItems do
                    local productKey = KEYS[i]
                    local quantity = tonumber(ARGV[i])

                    redis.call('DECRBY', productKey, quantity)
                end

                return 1 -- Success
            ";

            var redisResult = await _redisCacheService.ScriptEvaluateAsync(script, keys, args);
            long result = redisResult.IsNull ? 0 : long.Parse(redisResult.ToString());

            if (result == 1)
            {
                await ProcessOrder(order, cancellationToken);
            }
            else
            {
                await FallbackToDatabaseCheck(order, allProducts, cancellationToken);
            }
        }

        private async Task<long> ProcessOrder(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var allProducts = (await _productRepository.ListAsync(cancellationToken)).ToDictionary(x => x.Id, x => x);

            var order = Order.Create(request.CustomerId);

            var orderItemEntities = new List<OrderItem>();

            foreach (var item in request.OrderItems)
            {
                allProducts.TryGetValue(item.ProductId, out var product);
                if (product != null)
                {
                    orderItemEntities.Add(OrderItem.Create(order.Id, item.ProductId, product.Price, item.Quantity));

                    product.UpdateStock(item.Quantity);
                    await _redisCacheService.SetCacheValueAsync($"product:{item.ProductId}", product.Stock);

                    // Update AllProducts cache in Redis if it exists
                    var allProductCaches = await _redisCacheService.GetCacheValueAsync<IDictionary<long, ProductDto>>(RedisCacheConstants.AllProducts);
                    if (allProductCaches != null)
                    {
                        if (allProductCaches.ContainsKey(item.ProductId))
                        {
                            allProductCaches[item.ProductId].Stock = product.Stock;
                            await _redisCacheService.SetCacheValueAsync(RedisCacheConstants.AllProducts, allProductCaches);
                        }
                    }
                }
            }
            await _productRepository.UpdateRangeAsync(allProducts.Values, cancellationToken);

            order.AddOrderItems(orderItemEntities);

            order.GetTotalPrice();

            await _orderRepository.AddAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            return order.Id;
        }

        private async Task FallbackToDatabaseCheck(CreateOrderCommand request, IDictionary<long, ProductDto> allProducts, CancellationToken cancellationToken)
        {
            foreach (var item in request.OrderItems)
            {
                allProducts.TryGetValue(item.ProductId, out var product);
                if (product == null || product.Stock < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for product {item.ProductId}. Available: {product?.Stock}, Requested: {item.Quantity}");
                }
            }

            await ProcessOrder(request, cancellationToken);
        }
    }
}