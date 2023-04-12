using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entitites
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
