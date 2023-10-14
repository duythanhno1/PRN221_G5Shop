using BAL.Services.Interface;
using DAL.Entities;
using DAL.Infacstucture;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class CartService : ICartService
    {
        private ICartRepository CartRepository;
        private IUnitOfWork UnitOfWork;

        public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            this.CartRepository = cartRepository;
            this.UnitOfWork = unitOfWork;
        }

        public Cart? GetCartByProductAndUser(Product product, User user)
        {
            return CartRepository.GetCartByProductAndUser(product, user).Result;
        }

        public void AddItem(Cart cart)
        {
            cart.CreatedDate= DateTime.Now;
            cart.UpdatedDate= DateTime.Now;
            CartRepository.AddItem(cart);
            UnitOfWork.Commit();
        }

        public void DeleteItem(Cart cart)
        {
            CartRepository.DeleteItem(cart);
            UnitOfWork.Commit();
        }

        public List<Cart> GetCartList(User user)
        {
            return CartRepository.GetCartList(user).Result;
        }

        public void UpdateQuantity(Cart cart, int quantity)
        {
            cart.UpdatedDate = DateTime.Now;
            cart.Quantity = quantity;
            CartRepository.UpdateItem(cart);
            UnitOfWork.Commit();
        }

        public Cart GetCartById(string id)
        {
            return CartRepository.GetCartItem(new Guid(id)).Result;
        }

        public void RemoveCartList(User user)
        {
            CartRepository.RemoveCartList(user);
            UnitOfWork.Commit();
        }
    }
}
