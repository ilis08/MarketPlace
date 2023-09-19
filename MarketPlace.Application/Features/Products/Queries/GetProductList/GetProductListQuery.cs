using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Features.Products;

public class GetProductListQuery : IRequest<List<ProductListVm>>
{
}
