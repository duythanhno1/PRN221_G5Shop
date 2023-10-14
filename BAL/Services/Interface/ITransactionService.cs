using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interface
{
    public interface ITransactionService
    {
        void Save(User user, float Total, List<Cart> carts);
    }
}
