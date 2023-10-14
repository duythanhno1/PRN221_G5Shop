using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetUserbyID(string id);
        Task<User?> GetUserByEmail(string email);
        Task<User> GetUserbyUserEmail(string userEmail);
    }
}
