using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites.OrderNS
{
    public class OrderUserInformation : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string InvoiceAddress { get; set; } = default!;
    }
}
