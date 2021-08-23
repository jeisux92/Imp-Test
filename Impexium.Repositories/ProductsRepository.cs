using Impexium.DataAccess;
using Impexium.Domain.Entities;
using Impexium.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impexium.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ImpexiumContext _impexiumContext;

        public ProductsRepository(ImpexiumContext impexiumContext)
        {
            _impexiumContext = impexiumContext;
        }
        public async Task DeleteProductAsync(int id)
        {
            var product = _impexiumContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            _impexiumContext.Remove(product);
            await _impexiumContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<Product> GetAllProductsAsync() => _impexiumContext.Products.AsAsyncEnumerable();

        public Task<Product> GetProductAsync(int id) => _impexiumContext.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateProductAsync(Product product)
        {
            _impexiumContext.Products.Update(product);
            await _impexiumContext.SaveChangesAsync();
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            await _impexiumContext.Products.AddAsync(product);
            return await _impexiumContext.SaveChangesAsync();
        }
    }
}
