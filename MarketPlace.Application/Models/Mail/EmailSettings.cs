using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Models.Mail
{
    public class EmailSettings
    {
        public string ApiKey { get; set; } = default!;
        public string FromAddress { get; set; } = default!;
        public string FromName { get; set; } = default!;

    }
}
