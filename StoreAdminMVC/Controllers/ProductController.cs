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
                var responseStream = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductVM>>(responseStream);
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

            var request = new HttpRequestMessage(HttpMethod.Get, "Category/Get/");

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            string jsonString = await response.Content.ReadAsStringAsync();

            List<CategoryVM> categories = JsonConvert.DeserializeObject<List<CategoryVM>>(jsonString);

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
            var client = _clientFactory.CreateClient("myapi");

            HttpResponseMessage responseMessage = await client.GetAsync("Product/GetById/" + id);

            string jsonString = await responseMessage.Content.ReadAsStringAsync();
            var gameVM = JsonConvert.DeserializeObject<ProductVM>(jsonString);

            responseMessage = await client.GetAsync("Category/Get");
            jsonString = await responseMessage.Content.ReadAsStringAsync();

            List<CategoryVM> categories = JsonConvert.DeserializeObject<List<CategoryVM>>(jsonString);
            gameVM.CategoryList = new SelectList(
                categories,
                "Id",
                "Title");

            return View(gameVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductVM productVM)
        {
            try
            {
                var client = _clientFactory.CreateClient();

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
                    form.Add(new StringContent(productVM.Release.ToString()), nameof(productVM.Release));
                    form.Add(new StringContent(productVM.Price.ToString()), nameof(productVM.Price));
                    form.Add(new StringContent(productVM.CategoryId.ToString()), nameof(productVM.CategoryId));

                    var response = await client.PostAsync("api/Product/Save", form);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            ProductVM productVM;

            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                productVM = JsonConvert.DeserializeObject<ProductVM>(responseStream);
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
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/GetById/{id}");

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            string jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ProductVM>(jsonString);

            return View(responseData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/Delete/{id}");

                var client = _clientFactory.CreateClient("myapi");

                var response = await client.SendAsync(request);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
