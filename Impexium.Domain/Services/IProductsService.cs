using Impexium.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impexium.Domain.Services
{
    public interface IProductsService
    {
        IAsyncEnumerable<Product> GetAllProductsAsync();
        Task<Product> GetProductAsync(int id);

        Task<int> CreateProductAsync(Product product);
        Task UpdateProductAsync(int id, Product book);
    }
}
