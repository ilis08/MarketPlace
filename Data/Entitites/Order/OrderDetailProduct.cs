using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Data.Entitites
{
    public class OrderDetailProduct
    {
        [BindNever]
        public int Id { get; set; }

        public int Count { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
