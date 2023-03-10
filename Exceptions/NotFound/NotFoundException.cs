using Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.NotFound
{
    public class NotFoundException : Exception
    {
        public NotFoundException(long id, string className) : base($"The {className} with id {id} doesn't exist in the database.")
        {
        }
    }
}
