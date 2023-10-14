using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Repositories.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetById(string id);
        User? GetUserByEmail(string email);
        Task<User> GetByEmail(string email);
    }
}
