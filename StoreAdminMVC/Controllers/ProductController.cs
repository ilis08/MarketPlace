using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StoreAdminMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace StoreAdminMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<ProductVM> Products { get; private set; }

        public bool GetPullRequestsError { get; private set; }


        public ProductController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ActionResult> Index(string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "Product/Get/");

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Products = JsonConvert.DeserializeObject<List<ProductVM>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                GetPullRequestsError = true;
                Products = Array.Empty<ProductVM>();
            }
            

            return View(Products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductVM productVM = new();

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.GetAsync("Category/Get/");

            List<CategoryVM> categories = JsonConvert.DeserializeObject<List<CategoryVM>>(await response.Content.ReadAsStringAsync());

            productVM.CategoryList = new SelectList(
                categories,
                "Id",
                "Title"
                );

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            try
            {
                var client = _clientFactory.CreateClient("myapi");

                using (var memoryStream = new MemoryStream())
                {
                    await productVM.ImageFile.CopyToAsync(memoryStream);

                    using var form = new MultipartFormDataContent();
                    using var fileContent = new ByteArrayContent(memoryStream.ToArray());

                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                    form.Add(fileContent, nameof(productVM.ImageFile), productVM.ImageFile.FileName);
                    form.Add(new StringContent(productVM.ProductName), nameof(productVM.ProductName));
                    form.Add(new StringContent(productVM.Description), nameof(productVM.Description));
                    form.Add(new StringContent(productVM.Release.ToString()), nameof(productVM.Release));
                    form.Add(new StringContent(productVM.Price.ToString()), nameof(productVM.Price));
                    form.Add(new StringContent(productVM.CategoryId.ToString()), nameof(productVM.CategoryId));

                    var response = await client.PostAsync("Product/Save", form);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ProductVM product = new();

            var client = _clientFactory.CreateClient("myapi");

            HttpResponseMessage response = await client.GetAsync("Product/GetById/" + id);

            product = JsonConvert.DeserializeObject<ProductVM>(await response.Content.ReadAsStringAsync());

            product.CategoryId = product.Category.Id;

            response = await client.GetAsync("Category/Get");

            List<CategoryVM> categories = JsonConvert.DeserializeObject<List<CategoryVM>>(await response.Content.ReadAsStringAsync());

            product.CategoryList = new SelectList(
                categories,
                "Id",
                "Title");

            return View(product);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductVM productVM)
        {
            try
            {
                var client = _clientFactory.CreateClient("myapi");

                if (productVM.ImageFile == null)
                {
                 /*   using (var memoryStream = new MemoryStream())
                    {*/

                        /*  using var fileContent = new ByteArrayContent(memoryStream.ToArray());
                          fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                          form.Add(fileContent);
  */
                        using var form = new MultipartFormDataContent();
                        form.Add(new StringContent(productVM.Id.ToString()), nameof(productVM.Id));
                        form.Add(new StringContent(productVM.ProductName), nameof(productVM.ProductName));
                        form.Add(new StringContent(productVM.Description), nameof(productVM.Description));
                        form.Add(new StringContent(productVM.Release.ToString()), nameof(productVM.Release));
                        form.Add(new StringContent(productVM.Image), nameof(productVM.Image));
                        form.Add(new StringContent(productVM.Price.ToString()), nameof(productVM.Price));
                        form.Add(new StringContent(productVM.CategoryId.ToString()), nameof(productVM.CategoryId));

                        var response = await client.PutAsync("Product/Update", form);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Details", new { Id = productVM.Id });
                        }
                    
   
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await productVM.ImageFile.CopyToAsync(memoryStream);

                        using var form = new MultipartFormDataContent();
                        using var fileContent = new ByteArrayContent(memoryStream.ToArray());
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        form.Add(new StringContent(productVM.Id.ToString()), nameof(productVM.Id));
                        form.Add(fileContent, nameof(productVM.ImageFile), productVM.ImageFile.FileName);
                        form.Add(new StringContent(productVM.ProductName), nameof(productVM.ProductName));
                        form.Add(new StringContent(productVM.Description), nameof(productVM.Description));
                        form.Add(new StringContent(productVM.Image), nameof(productVM.Image));
                        form.Add(new StringContent(productVM.Release.ToString()), nameof(productVM.Release));
                        form.Add(new StringContent(productVM.Price.ToString()), nameof(productVM.Price));
                        form.Add(new StringContent(productVM.CategoryId.ToString()), nameof(productVM.CategoryId));

                        var response = await client.PutAsync("Product/Update", form);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Edit", new { Id = productVM.Id });
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            ProductVM productVM;

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.GetAsync("Product/GetById/" + id);

            if (response.IsSuccessStatusCode)
            {
                productVM = JsonConvert.DeserializeObject<ProductVM>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                GetPullRequestsError = true;
                productVM = null;
            }

            return View(productVM);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("myapi");

            var response = await client.GetAsync($"Product/GetById/{id}");

            var responseData = JsonConvert.DeserializeObject<ProductVM>(await response.Content.ReadAsStringAsync());

            return View(responseData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("myapi");

                var response = await client.DeleteAsync($"Product/Delete/{id}");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
