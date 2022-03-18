using Order.Models.Enums;

namespace Order.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid BillingAddressId { get; set; }
        public BillingAddress? BillingAddress { get; set; }
        public Guid ShippingAddressId { get; set; }
        public ShippingAddress? ShippingAddress { get; set; }
        public Dictionary<Guid,int>? Items { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public bool Shipped { get; set; }
        public bool Fulfilled { get; set; }
    }
}
