using Data.Entitites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Contracts
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, List<IdentityRole<long>> role);
    }
}
