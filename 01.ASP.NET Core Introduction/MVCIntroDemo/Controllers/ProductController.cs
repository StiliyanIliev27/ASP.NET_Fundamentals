using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCIntroDemo.Models.Product;
using System.Text;
using System.Text.Json;

namespace MVCIntroDemo.Controllers
{
    using static MVCIntroDemo.Seeding.ProductsData;
    public class ProductController : Controller
    {
        [ActionName("My-Products")]
        public IActionResult All()
        {
            return View(Products);
        }

        [ActionName("My-Products")]
        [HttpGet]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var foundProducts = Products
                    .Where(p => p.Name
                        .ToLower()
                        .Contains(keyword.ToLower()));

                return View(foundProducts);
            }
            return View(Products);
        }

        [Route("/Product/Details/{id?}")]
        public IActionResult ById(int id)
        {
            ProductViewModel? product = Products
                .FirstOrDefault(x => x.Id == id);
            
            if (product == null)
            {
                return RedirectToAction("My-Products");
            }
            
            return View(product);       
        }
        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            return Json(Products,options);   
        }
        public IActionResult AllAsText()
        {
            string text = string.Empty;
            foreach(var product in Products)
            {
                text += $"Product {product.Id}: {product.Name} - {product.Price} lv.";
                text += "\r\n";
            }
            return Content(text);
        }
        public IActionResult AllAsTextFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var product in Products)
            {
                sb.AppendLine($"Product {product.Id}: {product.Name} - {product.Price:f2} lv.");
            }

            Response.Headers.Add(HeaderNames.ContentDisposition, 
                @"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()),"text/plain");
        }
    }
}
