using Order.Models.Enums;

namespace Order.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid BillingAddressId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public Dictionary<Guid,int>? Items { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public decimal OrderTotal { get; set; }
        public bool Shipped { get; set; }
        public bool Fulfilled { get; set; }
    }
}
