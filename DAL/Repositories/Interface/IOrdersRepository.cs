using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Repositories.Interface
{
    public interface IOrdersRepository
    {
        List<Orders> GetTotalOrderToday();
        List<Orders> GetTotalOrderWeek();
        List<Orders> GetTotalOrderLastThirtyDays();
        List<(int Month, int TotalItems, float TotalAmount)> GetMonthlySalesData(int year);
        List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByMonth();
        List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByWeek();
        List<Orders> GetOrders();
        Dictionary<string, float> GetOrderValuesInEachMonth();
        List<(int Month, int TotalOrders, int TotalProducts)> GetSalesDataMonthly(int year);
        List<OrderDetail> GetOrderDetailsByOrderId(Guid orderId);
        Orders GetOrderById(Guid id);
        Orders SaveOrder(Orders orders);
        //Get all orders from a user
        List<Orders> GetOrdersById(Guid id);
    }
}
