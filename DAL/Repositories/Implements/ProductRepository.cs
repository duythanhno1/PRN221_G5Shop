using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class ProductRepository : FRMDbContextBase<Product>, IProductRepository
    {
        private readonly FRMDbContext _dbContext;

        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public Task<Product?> GetProductAsync(long id)
        {
            return _dbSet.FirstOrDefaultAsync(p => p.ID.Equals(id));
        }

        public async Task<Product> GetProductAsyncs()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetListProductAsyncs()
        {
            return await _dbSet.ToListAsync();
        }
        
        public async Task UpdateProductAsync(Product product)
        {
            _dbSet.Update(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateProductAsync(Product product)
        {
            _dbSet.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(long id)
        {
            var product = await _dbSet.FirstOrDefaultAsync(p => p.ID.Equals(id));
            if (product != null)
            {
                _dbSet.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
