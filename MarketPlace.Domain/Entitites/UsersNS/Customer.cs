using MarketPlace.Domain.Entitites.OrderNS;

namespace MarketPlace.Domain.Entitites.UsersNS
{
    public class Customer : ApplicationUser
    {
        public string? Appeal { get; set; }
        public DateOnly? BirthDay { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
