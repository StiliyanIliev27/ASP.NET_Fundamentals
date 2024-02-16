using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Contracts.Interfaces;
using ShoppingListApp.Data;
using ShoppingListApp.Models;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingListApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
     
        //Dependency injection
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]   
        public async Task<IActionResult> Index()
        {
            var model = await productService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new ProductViewModel();

            return View(model);
        }
        
        [HttpPost]  
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.AddProductAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await productService.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model, int id)
        {
            if (!ModelState.IsValid || model.Id != id)
            {
                return View(model);
            }

            await productService.UpdateProductAsync(model);
          
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteProductAsync(id);        

            return RedirectToAction(nameof(Index));
        }
    }
}
