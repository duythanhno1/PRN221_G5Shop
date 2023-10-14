using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interface
{
    public interface ICartService
    {
        List<Cart> GetCartList(User user);

        Cart? GetCartByProductAndUser(Product product, User user);

        void AddItem(Cart cart);

        void UpdateQuantity(Cart cart, int quantity);

        void DeleteItem(Cart cart);

        Cart GetCartById(string id);

        void RemoveCartList(User user);

    }
}
