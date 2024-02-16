using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Contracts.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        Task<ProductViewModel> GetByIdAsync(int id);

        Task AddProductAsync(ProductViewModel productViewModel);

        Task UpdateProductAUsync(ProductViewModel productViewModel);

        Task DeleteProductAsync(int id);
    }
}
