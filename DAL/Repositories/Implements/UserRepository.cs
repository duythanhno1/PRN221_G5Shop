using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DAL.Repositories.Implements
{
    public class UserRepository : FRMDbContextBase<User>, IUserRepository
    {
        private readonly FRMDbContext _dbContext;
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }
        public async Task<List<User>> GetAll()
        {
            var users = await _dbSet.Include(v=>v.Orders).ToListAsync();
            return users;
        }

        public async Task<User> GetUserbyID(string id)
        {
            var user = await _dbContext.Users
                                        .Include(v=>v.Orders)
                                        .Where(u => u.Id.Equals(id)).FirstAsync();
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        }
        public async Task<User> GetUserbyUserEmail(string userEmail)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == userEmail);
            return user;
        }
    }
}
