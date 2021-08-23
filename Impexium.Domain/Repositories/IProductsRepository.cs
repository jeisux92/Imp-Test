using Impexium.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impexium.Domain.Repositories
{
    public interface IProductsRepository
    {
        IAsyncEnumerable<Product> GetAllProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<int> CreateProductAsync(Product product);
    }
}
