﻿using Data.Context;
using Data.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        internal Store3DBContext context;

        public ProductRepository(Store3DBContext context)
        {
            this.context = context;
        }

        public void Create(Product order)
        {
            context.Add(order);
        }

        public void Delete(Product entity)
        {
            context.Remove(entity);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.Where(p => p.Id == id).Include(x => x.Category).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsWithParamsAsync(GetProductsParameters getProducts)
        {
            return await context.Products.OrderBy(p => p.Price).Skip((getProducts.PageNumber - 1) * getProducts.PageSize).Take(getProducts.PageSize).Include(x => x.Category).ToListAsync();
        }

        public void Update(Product entity)
        {
            context.Products.Update(entity);
        }

        public string SaveImageAsync(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/", "Images/", imageName);

            var imagePathStore = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/", "Images/", imageName);


            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(imagePathStore, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }


            return imageName;
        }
    }
}