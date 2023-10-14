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
    }
}
