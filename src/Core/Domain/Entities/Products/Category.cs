using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Products
{
    public class Category : BaseEntity
    {
        [MaxLength(100)]
        public string? Name { get; protected set; }

        private readonly List<Product> _product = new List<Product>();
        public IReadOnlyCollection<Product> Products => _product.AsReadOnly();
    }
}
