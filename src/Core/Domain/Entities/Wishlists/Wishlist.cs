namespace Domain.Entities.Wishlists
{
    public class Wishlist : BaseEntity
    {
        public long CustomerId { get; protected set; }
        public long ProductId { get; protected set; }
    }
}
