using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Contracts.Interfaces;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Contracts
{
    public class ProductService : IProductService
    {
        private readonly ShoppingListDbContext dbContext;
        private int nextProductId = 1;

        public ProductService(ShoppingListDbContext dbContext)
        {
            this.dbContext = dbContext;

            nextProductId = dbContext.Products.Any() ?
                dbContext.Products.Max(p => p.Id) + 1 : 1;
        }
        public async Task AddProductAsync(ProductViewModel model)
        {
            var product = new Product()
            {
                Id = nextProductId++,
                Name = model.Name
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                throw new ArgumentException("Invalid product");
            }

            dbContext.Products.Remove(product);         

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await dbContext.Products
                .AsNoTracking()
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();        
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                throw new ArgumentException("Product not found!");
            }

            var model = new ProductViewModel()
            {
                Id = id,
                Name = product.Name
            };

            return model;
        }

        public async Task UpdateProductAsync(ProductViewModel productViewModel)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == productViewModel.Id);

            if(product == null)
            {
                throw new ArgumentException("Invalid product");
            }

            product.Name = productViewModel.Name;
            
            await dbContext.SaveChangesAsync();
        }
    }
}
