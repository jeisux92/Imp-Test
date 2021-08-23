using Impexium.Domain.Entities;
using Impexium.Domain.Repositories;
using Impexium.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impexium.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public Task<int> CreateProductAsync(Product product) => _productsRepository.CreateProductAsync(product);

        public IAsyncEnumerable<Product> GetAllProductsAsync() => _productsRepository.GetAllProductsAsync();


        public Task<Product> GetProductAsync(int id) => _productsRepository.GetProductAsync(id);
        public async Task UpdateProductAsync(int id, Product book)
        {
            var bookOnDataBase = await _productsRepository.GetProductAsync(id);
            bookOnDataBase.Description = book.Description;
            bookOnDataBase.Quantity = book.Quantity;
         
            await _productsRepository.UpdateProductAsync(bookOnDataBase);

        }
    }
}
