using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Repositories.Interface
{
    public interface IOrdersService
    {
        float GetTotalOrderToday();
        float GetTotalOrdersWeek();
        float GetTotalOrderLastThirtyDays();
        int CountOrderLastThirtyDays();
        List<(int Month, int TotalItems, float TotalAmount)> GetMonthlySalesData(int year);
        List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByMonth();
        List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByWeek();
        List<Orders> GetOrders();
        Dictionary<string, float> GetOrderValuesInEachMonth();
        List<(int Month, int TotalOrders, int TotalProducts)> GetSalesDataMonthly(int year);
        List<OrderDetail> GetOrderDetailsByOrderId(Guid orderId);
        Orders GetOrderById(Guid id);
        //Get all orders from a user
        List<Orders> GetOrdersById(Guid id);
    }
}
