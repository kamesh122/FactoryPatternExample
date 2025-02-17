using FactoryPatternExample.DataAccess;
using FactoryPatternExample.Model;
using FactoryPatternExample.Service.Interface;

namespace FactoryPatternExample.Service.Handlers
{
    public class ProductUpdateHandler : IUpdateHandler<Product>
    {
        private readonly AppDbContext _context;

        public ProductUpdateHandler(AppDbContext context) => _context = context;

        public async Task<bool> UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null) return false;

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
