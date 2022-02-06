using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public sealed class PriceRangeBadRequestException : Exception
    {
        public PriceRangeBadRequestException() : base("Min price cannot be greater than the Max price.")
        {
        }
    }
}
