using DAL.Infacstucture;
using DAL.Repositories.Interface;
using System.Collections.Generic;
using DAL.Entities;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace BAL.Services.Implements
{
    public class TransactionService : ITransactionService
    {
        private IOrdersRepository OrdersRepository;
        private IOrderDetailRepository OrderDetailRepository;
        private ITransactionRepository TransactionRepository;
        private IUnitOfWork _unitOfWork;

        private readonly byte[] salt = Encoding.ASCII.GetBytes("PRN221");

        public TransactionService(ITransactionRepository transactionRepository, IOrderDetailRepository orderDetailRepository, IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            TransactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            OrderDetailRepository = orderDetailRepository;
            OrdersRepository = ordersRepository;
        }

        public void Save(User user, float Total, List<Cart> carts)
        {
            //Save order
            var Orders = new Orders
            {
                Total = Total,
                CreatedDate = DateTime.Now,
                isDeleted = false,
                User = user
            };
            Orders = OrdersRepository.SaveOrder(Orders);
            //Save orderDetail
            var OrderDetail = carts.ConvertAll((cart) =>
            {
                var orderDetail = new OrderDetail();
                orderDetail.Order = Orders;
                orderDetail.Product = cart.Product;
                orderDetail.Quantity = cart.Quantity;
                orderDetail.Price = cart.Product.Price;
                orderDetail.Amount = cart.Quantity * cart.Product.Price;
                orderDetail.isDeleted = false;
                return orderDetail;
            });
            OrderDetailRepository.SaveList(OrderDetail);
            //Save Transaction
            var prevTrans = TransactionRepository.GetLatestTransaction().Result;
            var trans = new Transaction
            {
                Amount = Total,
                HashValue = Convert.ToBase64String(GenerateSaltedHash(Encoding.ASCII.GetBytes((Orders.CreatedDate.ToString())), salt)),
                PreviousBalance = prevTrans == null ? 0 : (decimal)prevTrans.Amount + prevTrans.PreviousBalance,
                CreatedDate = DateTime.UtcNow,
                Status = true,
                isDeleted = false,
                Order = Orders
            };
            if (prevTrans == null)
            {
                //The first trans go here
                trans.PreviousHash = Convert.ToBase64String(GenerateSaltedHash(Encoding.ASCII.GetBytes("HashStartHere"), salt));
            } else
            {
                //Calculated again the hash of prev hash. If hash ok then save the prev hash to this prevHash
                bool isPrevTransGood = CompareByteArrays(GenerateSaltedHash(Encoding.ASCII.GetBytes((prevTrans.Order.CreatedDate.ToString())), salt), Convert.FromBase64String(prevTrans.HashValue));
                if (isPrevTransGood)
                {
                    trans.PreviousHash = prevTrans.HashValue;
                } else
                {
                    throw new Exception("Error. Invaliid transaction detected");
                }
            }
            TransactionRepository.Save(trans);
            _unitOfWork.Commit();
        }
        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
