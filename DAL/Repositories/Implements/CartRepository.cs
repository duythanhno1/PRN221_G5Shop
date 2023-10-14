using DAL.Entities;
using DAL.Infacstucture;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class CartRepository : FRMDbContextBase<Cart>, ICartRepository
    {
        private readonly FRMDbContext _dbContext;
        public CartRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public async Task<Cart> GetCartItem(Guid guid)
        {
            return await _dbSet.Where(cart => cart.ID.Equals(guid)).Include(cart => cart.Product).FirstAsync();
        }

        public async Task<Cart?> GetCartByProductAndUser(Product product, User user)
        {
            return await _dbSet.FirstOrDefaultAsync((c) => c.Product.Equals(product) && c.User.Equals(user));
        }

        public void AddItem(Cart cart)
        {
            _dbSet.Add(cart);
        }

        public void DeleteItem(Cart cart)
        {
            _dbSet.Remove(cart);
        }

        public async Task<List<Cart>> GetCartList(User user)
        {
            return await _dbSet.Include(c=>c.User).Where(c => c.User.Email.Equals(user.Email)).Include(c => c.Product).ToListAsync();
        }

        public void UpdateItem(Cart cart)
        {
            _dbSet.Update(cart);
        }

        public void RemoveCartList(User user)
        {
            var list = _dbSet.Where(c => c.User.Equals(user)).ToList();
            _dbSet.RemoveRange(list);
        }
    }
}
