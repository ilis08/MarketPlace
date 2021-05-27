using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entitites
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
    }
}
