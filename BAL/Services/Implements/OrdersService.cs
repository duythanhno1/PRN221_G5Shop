using System;
using System.Linq;
using System.Text;
using DAL.Infacstucture;
using System.Threading.Tasks;
using DAL.Repositories.Interface;
using System.Collections.Generic;
using DAL.Entities;
using DAL;

namespace BAL.Services.Implements
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;
        private IUnitOfWork _unitOfWork;

        public OrdersService(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public float GetTotalOrderToday()
        {
            var orders = _ordersRepository.GetTotalOrderToday();
            float total = 0;

            foreach (var item in orders)
            {
                total += item.Total;
            }

            return total;
        }

        public float GetTotalOrdersWeek()
        {
            var orders = _ordersRepository.GetTotalOrderWeek();

            float total = 0;

            foreach (var item in orders)
            {
                total += item.Total;
            }

            return total;
        }

        public float GetTotalOrderLastThirtyDays()
        {
            var orders = _ordersRepository.GetTotalOrderLastThirtyDays();

            float total = 0;

            foreach (var item in orders)
            {
                total += item.Total;
            }

            return total;
        }

        public int CountOrderLastThirtyDays()
        {
            var orders = _ordersRepository.GetTotalOrderLastThirtyDays();



            return orders.Count;
        }

        public List<(int Month, int TotalItems, float TotalAmount)> GetMonthlySalesData(int year)
        {
            var list = _ordersRepository.GetMonthlySalesData(year);
            return list;
        }

        public List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByMonth()
        {
            var list = _ordersRepository.GetTopSellingProductsByMonth();
            return list;
        }
        public List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByWeek()
        {
            var list = _ordersRepository.GetTopSellingProductsByWeek();
            return list;
        }

        public List<Orders> GetOrders()
        {
            var list = _ordersRepository.GetOrders();
            return list;
        }

        public Dictionary<string, float> GetOrderValuesInEachMonth()
        {
            var list = _ordersRepository.GetOrderValuesInEachMonth();

            return list;
        }
        public List<(int Month, int TotalOrders, int TotalProducts)> GetSalesDataMonthly(int year)
        {
            var list = _ordersRepository.GetSalesDataMonthly(year);
            return list;
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(Guid orderId)
        {
            return _ordersRepository.GetOrderDetailsByOrderId(orderId);
        }

        public Orders GetOrderById(Guid id)
        {
            return _ordersRepository.GetOrderById(id);
        }

        //Get all orders from a user
        public List<Orders> GetOrdersById(Guid id)
        {
            return _ordersRepository.GetOrdersById(id);
        }
    }
}
