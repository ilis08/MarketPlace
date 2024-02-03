namespace MarketPlace.Domain.Entitites.Users
{
    public class Customer : ApplicationUser
    {
        public string? Appeal { get; set; }
        public DateOnly? BirthDay { get; set; }
        public string? Address { get; set; }
    }
}
