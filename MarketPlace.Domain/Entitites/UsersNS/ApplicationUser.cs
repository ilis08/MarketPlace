using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Domain.Entitites.UsersNS
{
    public abstract class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
