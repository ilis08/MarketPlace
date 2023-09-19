using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts;

public interface IProductImage
{
    string SaveImage(IFormFile imageFile);
    string UpdateImage(IFormFile imageFile, string oldImagePath);
}
