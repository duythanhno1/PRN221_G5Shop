using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class OrdersRepository : FRMDbContextBase<Orders> , IOrdersRepository
    {
        private readonly FRMDbContext _dbContext;
        public OrdersRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public List<Orders> GetTotalOrderToday()
        {
            DateTime today = DateTime.Today;
            
            var orders = _dbContext.Orders.Where(o => o.CreatedDate == today);
            return orders.ToList();
        }

        public List<Orders> GetTotalOrderWeek()
        {
            DateTime today = DateTime.Today;

            var orders = _dbContext.Orders.Where(o => o.CreatedDate >= today.AddDays(-7) && o.CreatedDate <= today);
            return orders.ToList();
        }

        public List<Orders> GetTotalOrderLastThirtyDays()
        {
            DateTime today = DateTime.Today;

            var orders = _dbContext.Orders.Where(o => o.CreatedDate >= today.AddDays(-30) && o.CreatedDate <= today);
            return orders.ToList();
        }

        public List<(int Month, int TotalItems, float TotalAmount)> GetMonthlySalesData(int year)
        {
            var monthlySalesData = _dbContext.Orders
                .Where(o => o.CreatedDate.Year == year && !o.isDeleted)
                .Join(_dbContext.OrderDetail.Where(d => !d.isDeleted),
                    o => o.ID,
                    d => d.Order.ID,
                    (o, d) => new
                    {
                        Month = o.CreatedDate.Month,
                        Quantity = d.Quantity,
                        Amount = d.Amount
                    })
                .GroupBy(d => d.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalItems = g.Sum(d => d.Quantity),
                    TotalAmount = g.Sum(d => d.Amount)
                })
                .OrderBy(d => d.Month)
                .ToList();

            return monthlySalesData.Select(d => (d.Month, d.TotalItems, d.TotalAmount)).ToList();
        }

        public List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByMonth()
        {
            var topProducts = _dbContext.OrderDetail
                .Where(od => !od.isDeleted && !od.Order.isDeleted && !od.Product.isDeleted
                    && od.Order.CreatedDate >= DateTime.Now.AddDays(-30) && od.Order.CreatedDate <= DateTime.Now)
                .GroupBy(od => od.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalAmount = g.Sum(od => od.Amount),
                    TotalQuantity = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(5)
                .ToList();
            return topProducts.Select(d => (d.Product, d.TotalAmount, d.TotalQuantity)).ToList();
        }

        public List<(Product Product, float TotalAmount, int TotalQuantity)> GetTopSellingProductsByWeek()
        {
            var topProducts = _dbContext.OrderDetail
                .Where(od => !od.isDeleted && !od.Order.isDeleted && !od.Product.isDeleted
                    && od.Order.CreatedDate >= DateTime.Now.AddDays(-7) && od.Order.CreatedDate <= DateTime.Now)
                .GroupBy(od => od.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalAmount = g.Sum(od => od.Amount),
                    TotalQuantity = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(5)
                .ToList();
            return topProducts.Select(d => (d.Product, d.TotalAmount, d.TotalQuantity)).ToList();
        }

        public List<Orders> GetOrders()
        {
            return _dbContext.Orders
                .Include(m => m.User).ToList();
        }

        public Dictionary<string, float> GetOrderValuesInEachMonth()
        {
            var now = DateTime.Now;
            var startOfYear = new DateTime(now.Year, 1, 1);
            var orders = _dbContext.Orders
                .Where(o => !o.isDeleted && o.CreatedDate >= startOfYear && o.CreatedDate <= now)
                .ToList();

            var result = new Dictionary<string, float>();
            for (int month = 1; month <= now.Month; month++)
            {
                var monthOrders = orders.Where(o => o.CreatedDate.Month == month);
                var monthValue = monthOrders.Sum(o => o.Total);
                result.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month), monthValue);
            }

            return result;
        }

        public List<(int Month, int TotalOrders, int TotalProducts)> GetSalesDataMonthly(int year)
        {
            var monthlySalesData = new List<(int Month, int TotalOrders, int TotalProducts)>();

            for (int i = 1; i <= 12; i++)
            {
                int month = i;
                int totalOrders = _dbContext.Orders
                    .Where(o => !o.isDeleted
                        && o.CreatedDate.Year == year
                        && o.CreatedDate.Month == month)
                    .Count();
                int totalProducts = _dbContext.OrderDetail
                    .Where(od => !od.isDeleted && !od.Order.isDeleted && !od.Product.isDeleted
                        && od.Order.CreatedDate.Year == year
                        && od.Order.CreatedDate.Month == month)
                    .Sum(od => od.Quantity);

                monthlySalesData.Add((month, totalOrders, totalProducts));
            }

            return monthlySalesData;
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(Guid orderId)
        {

            Orders order = GetOrderById(orderId);

            return _dbContext.OrderDetail
                .Where(od => od.Order == order)
                .Include(od => od.Product)
                .ToList();
        }

        public Orders GetOrderById(Guid id)
        {
            var order = _dbContext.Orders
                        .Include(o => o.User)
                        .FirstOrDefault(o => o.ID == id);
            return order;
        }
        
        public Orders SaveOrder(Orders orders)
        {
            return _dbSet.Add(orders).Entity;
        }
        //Get all orders from a user
        public List<Orders> GetOrdersById(Guid id)
        {
            var orders = _dbContext.Orders.Where(x => x.UserId == id.ToString());
            return orders.ToList();
        }
    }
}
