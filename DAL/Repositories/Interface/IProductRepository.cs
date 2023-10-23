using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(long id);
        Task<List<Product>> GetListProductAsyncs();
        Task<Product> GetProductAsyncs();
        Task DeleteProductAsync(long id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
